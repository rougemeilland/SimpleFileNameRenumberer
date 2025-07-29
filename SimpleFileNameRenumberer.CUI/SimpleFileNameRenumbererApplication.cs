using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Palmtree;
using Palmtree.Application;
using Palmtree.IO;
using Palmtree.IO.Console;
using Palmtree.Linq;

namespace SimpleFileNameRenumberer.CUI
{
    public class SimpleFileNameRenumbererApplication
        : BatchApplication
    {
        private static readonly FilePath? _ffmpegCommandPath;
        private readonly string _title;
        private readonly Encoding? _encoding;
        private int _totalFileCount;
        private int _fileCount;

        static SimpleFileNameRenumbererApplication()
        {
            try
            {
                var path = ProcessUtility.WhereIs("ffmpeg");
                _ffmpegCommandPath = path is not null ? new FilePath(path) : null;
            }
            catch (Exception)
            {
                _ffmpegCommandPath = null;
            }
        }

        public SimpleFileNameRenumbererApplication(string title, Encoding? encoding)
        {
            _title = title;
            _encoding = encoding;
            _totalFileCount = 0;
            _fileCount = 0;
        }

        protected override string ConsoleWindowTitle => _title;
        protected override Encoding? InputOutputEncoding => _encoding;

        protected override ResultCode Main(string[] args)
        {
            var prefix = (string?)null;
            var extensions = new List<string>();
            var onlyImageFile = false;
            var resetMonochromeImageTimestamp = false;
            var targetPaths = new List<string>();
            for (var index = 0; index < args.Length; ++index)
            {
                var arg = args[index];
                if (arg == "--prefix")
                {
                    if (index + 1 >= args.Length)
                    {
                        ReportErrorMessage("--prefixオプション の値が指定されていません。");
                        return ResultCode.Failed;
                    }

                    prefix = args[index + 1];
                    ++index;
                }
                else if (arg == "--extension")
                {
                    if (index + 1 >= args.Length)
                    {
                        ReportErrorMessage("--extensionオプション の値が指定されていません。");
                        return ResultCode.Failed;
                    }

                    foreach (var ext in args[index + 1].Split(","))
                    {
                        if (ext.Length <= 0)
                        {
                            ReportErrorMessage("--extension オプションで長さが 0 の拡張子が指定されています。");
                        }

                        var upperExt = $".{ext.ToUpperInvariant()}";
                        if (!extensions.Contains(upperExt))
                            extensions.Add(upperExt);
                    }

                    ++index;
                }
                else if (arg == "--only_image_file")
                {
                    if (!OperatingSystem.IsWindowsVersionAtLeast(6, 1))
                    {
                        ReportErrorMessage($"このオペレーティングシステムではサポートされていないオプションが指定されています。: \"{arg}\"");
                    }

                    onlyImageFile = true;
                }
                else if (arg == "--reset_monochrome_image_timestamp")
                {
                    if (!OperatingSystem.IsWindowsVersionAtLeast(6, 1))
                    {
                        ReportErrorMessage($"このオペレーティングシステムではサポートされていないオプションが指定されています。: \"{arg}\"");
                        return ResultCode.Failed;
                    }

                    resetMonochromeImageTimestamp = true;
                }
                else if (arg.StartsWith('-'))
                {
                    ReportErrorMessage($"サポートされていないオプションが指定されています。: \"{arg}\"");
                    return ResultCode.Failed;
                }
                else
                {
                    targetPaths.Add(arg);
                }
            }

            if (resetMonochromeImageTimestamp && !onlyImageFile)
            {
                ReportErrorMessage("--reset_monochrome_image_timestamp オプションは --only_image_file とともに指定する必要があります。");
                return ResultCode.Failed;
            }

            ReportProgress("ファイルを検索しています…");

            var targetFilesContainsNonImageFile =
                targetPaths
                .EnumerateFilesFromArgument();
            if (extensions.Count > 0)
            {
                targetFilesContainsNonImageFile =
                    targetFilesContainsNonImageFile
                    .Where(file => extensions.Contains(file.Extension.ToUpperInvariant()));
            }

            var targetFiles =
                targetFilesContainsNonImageFile
                .ToList();

            try
            {
                if (targetFiles.Count > 0)
                {
                    _totalFileCount = targetFiles.Count * 5;
                    AddProgress(0);

                    var targetDirectoryInfos =
                        targetFiles
                        .Select(file =>
                        {
                            var fileInfo = new
                            {
                                directory = file.Directory,
                                file,
                            };

                            return fileInfo;
                        })
                        .GroupBy(item => item.directory.FullName, StringComparer.OrdinalIgnoreCase)
                        .Select(g => new
                        {
                            g.First().directory,
                            isEnabledImageDirectory = IsEnableImageDirectory(g.First().directory),
                            imageFiles = g.Select(item => item.file).ToList(),
                        })
                        .ToList();

                    foreach (var targetDirectoryInfo in targetDirectoryInfos)
                    {
                        if (IsPressedBreak)
                            throw new OperationCanceledException();

                        try
                        {
                            var monochromeImageFiles = new List<FilePath>();
                            var nonMonochromeImageFiles = new List<(FilePath imageFile, DateTime lastWriteTime, DateTime lastAccessTime, DateTime creationTime)>();
                            if (!targetDirectoryInfo.isEnabledImageDirectory)
                            {
                                foreach (var file in targetDirectoryInfo.imageFiles)
                                {
                                    if (IsPressedBreak)
                                        throw new OperationCanceledException();
                                    nonMonochromeImageFiles.Add((file, file.LastWriteTimeUtc, Minimum(file.LastAccessTimeUtc, file.LastWriteTimeUtc), Minimum(file.CreationTimeUtc, file.LastWriteTimeUtc)));
                                }

                                AddProgress(targetDirectoryInfo.imageFiles.Count * 4, targetDirectoryInfo.directory.FullName);
                            }
                            else if (targetDirectoryInfo.imageFiles.Count < 2)
                            {
                                ReportWarningMessage($"ディレクトリ配下にファイルが1つしかないため変名を行いません。: \"{targetDirectoryInfo.directory.FullName}\"");
                                foreach (var file in targetDirectoryInfo.imageFiles)
                                {
                                    if (IsPressedBreak)
                                        throw new OperationCanceledException();
                                    nonMonochromeImageFiles.Add((file, file.LastWriteTimeUtc, Minimum(file.LastAccessTimeUtc, file.LastWriteTimeUtc), Minimum(file.CreationTimeUtc, file.LastWriteTimeUtc)));
                                }

                                AddProgress(targetDirectoryInfo.imageFiles.Count * 4, targetDirectoryInfo.directory.FullName);
                            }
                            else
                            {
                                var imageFileDirectory = targetDirectoryInfo.directory;
                                if (!ExistDuplicateFileNames(imageFileDirectory, targetDirectoryInfo.imageFiles.Select(imageFile => imageFile.Name)))
                                {
                                    AddProgress(targetDirectoryInfo.imageFiles.Count * 5, targetDirectoryInfo.directory.FullName);
                                }
                                else
                                {
                                    var fileInfos =
                                        targetDirectoryInfo.imageFiles
                                        .Select(imageFile =>
                                        {
                                            if (IsPressedBreak)
                                                throw new OperationCanceledException();
                                            if (onlyImageFile)
                                            {
                                                var (isWideImage, isMonochromeImage) = CheckImage(imageFile);
                                                var value = new
                                                {
                                                    imageFile,
                                                    lastWriteTime = imageFile.LastWriteTimeUtc,
                                                    lastAccessTime = Minimum(imageFile.LastAccessTimeUtc, imageFile.LastWriteTimeUtc),
                                                    creationTime = Minimum(imageFile.CreationTimeUtc, imageFile.LastWriteTimeUtc),
                                                    isWideImage,
                                                    isMonochromeImage,
                                                };
                                                AddProgress(1, targetDirectoryInfo.directory.FullName);
                                                return value;
                                            }
                                            else
                                            {
                                                var value = new
                                                {
                                                    imageFile,
                                                    lastWriteTime = imageFile.LastWriteTimeUtc,
                                                    lastAccessTime = Minimum(imageFile.LastAccessTimeUtc, imageFile.LastWriteTimeUtc),
                                                    creationTime = Minimum(imageFile.CreationTimeUtc, imageFile.LastWriteTimeUtc),
                                                    isWideImage = false,
                                                    isMonochromeImage = false,
                                                };
                                                AddProgress(1, targetDirectoryInfo.directory.FullName);
                                                return value;
                                            }
                                        })
                                        .ToList();
                                    if (targetDirectoryInfo.isEnabledImageDirectory)
                                    {
                                        var temporaryPrefix = GetTemporaryFilePrefix(imageFileDirectory);
                                        var totalPageCount = fileInfos.Sum(item => item.isWideImage ? 2 : 1);
                                        var numberWidth = totalPageCount.ToString(CultureInfo.InvariantCulture.NumberFormat).Length;
                                        var numberFormat = $"D{numberWidth}";
                                        var pageCount = 1;

                                        var fileNameMaps =
                                            fileInfos
                                            .OrderBy(fileInfo => fileInfo.imageFile.Name, StringComparer.OrdinalIgnoreCase)
                                            .Select((fileInfo, index) =>
                                            {
                                                if (IsPressedBreak)
                                                    throw new OperationCanceledException();
                                                var isFirstPage = pageCount == 1;
                                                var isWideImage = !isFirstPage && fileInfo.isWideImage;
                                                var destinationPageNumber = isWideImage ? $"{pageCount.ToString(numberFormat, CultureInfo.InvariantCulture.NumberFormat)}-{(pageCount + 1).ToString(numberFormat, CultureInfo.InvariantCulture.NumberFormat)}" : pageCount.ToString(numberFormat, CultureInfo.InvariantCulture.NumberFormat);
                                                pageCount += isWideImage ? 2 : 1;
                                                var extension = fileInfo.imageFile.Extension;
                                                var destinationFile = imageFileDirectory.GetFile($"{prefix ?? ""}{destinationPageNumber}{extension}");

                                                if ((pageCount & 1) != 0 && isWideImage)
                                                    ReportWarningMessage($"見開きページが偶数ページ番号ではありません。: page=\"{destinationPageNumber}\", file=\"{destinationFile.FullName}\"");

                                                return new
                                                {
                                                    sourceFile = fileInfo.imageFile,
                                                    temporaryFileName = imageFileDirectory.GetFile($"{temporaryPrefix}{destinationPageNumber}{extension}"),
                                                    destinationFile,
                                                    lastWriteTime = fileInfo.imageFile.LastWriteTimeUtc,
                                                    lastAccessTime = Minimum(fileInfo.imageFile.LastAccessTimeUtc, fileInfo.imageFile.LastWriteTimeUtc),
                                                    creationTime = Minimum(fileInfo.imageFile.CreationTimeUtc, fileInfo.imageFile.LastWriteTimeUtc),
                                                    isWideImage = false,
                                                    fileInfo.isMonochromeImage,
                                                };
                                            })
                                            .ToList();

                                        foreach (var fileNameMap in fileNameMaps)
                                        {
                                            if (!string.Equals(fileNameMap.sourceFile.FullName, fileNameMap.destinationFile.FullName, StringComparison.OrdinalIgnoreCase))
                                                fileNameMap.sourceFile.MoveTo(fileNameMap.temporaryFileName);
                                            AddProgress(1, targetDirectoryInfo.directory.FullName);
                                        }

                                        foreach (var fileNameMap in fileNameMaps)
                                        {
                                            if (!string.Equals(fileNameMap.sourceFile.FullName, fileNameMap.destinationFile.FullName, StringComparison.OrdinalIgnoreCase))
                                                fileNameMap.temporaryFileName.MoveTo(fileNameMap.destinationFile);
                                            AddProgress(1, targetDirectoryInfo.directory.FullName);
                                        }

                                        foreach (var fileNameMap in fileNameMaps)
                                        {
                                            if (fileNameMap.isMonochromeImage)
                                                monochromeImageFiles.Add(fileNameMap.destinationFile);
                                            else
                                                nonMonochromeImageFiles.Add((fileNameMap.destinationFile, fileNameMap.lastWriteTime, fileNameMap.lastAccessTime, fileNameMap.creationTime));
                                            AddProgress(1, targetDirectoryInfo.directory.FullName);
                                        }
                                    }
                                    else
                                    {
                                        foreach (var fileInfo in fileInfos)
                                        {
                                            if (fileInfo.isMonochromeImage)
                                                monochromeImageFiles.Add(fileInfo.imageFile);
                                            else
                                                nonMonochromeImageFiles.Add((fileInfo.imageFile, fileInfo.lastWriteTime, fileInfo.lastAccessTime, fileInfo.creationTime));
                                            AddProgress(3, targetDirectoryInfo.directory.FullName);
                                        }
                                    }
                                }
                            }

                            if (resetMonochromeImageTimestamp)
                            {
                                var minimumLastWriteTime = DateTime.MaxValue;
                                var minimumLastAccessTime = DateTime.MaxValue;
                                var minimumCreationTime = DateTime.MaxValue;
                                foreach (var (file, lastWriteTime, lastAccessTime, creationTime) in nonMonochromeImageFiles)
                                {
                                    minimumLastWriteTime = Minimum(minimumLastWriteTime, lastWriteTime);
                                    minimumLastAccessTime = Minimum(minimumLastAccessTime, lastAccessTime);
                                    minimumCreationTime = Minimum(minimumCreationTime, lastWriteTime);
                                }

                                foreach (var monochromeImageFile in monochromeImageFiles)
                                {
                                    if (minimumLastWriteTime != DateTime.MaxValue)
                                        monochromeImageFile.LastWriteTimeUtc = minimumLastWriteTime;
                                    if (minimumLastAccessTime != DateTime.MaxValue)
                                        monochromeImageFile.LastAccessTimeUtc = minimumLastAccessTime;
                                    if (minimumCreationTime != DateTime.MaxValue)
                                        monochromeImageFile.CreationTimeUtc = minimumCreationTime;
                                    AddProgress(1, targetDirectoryInfo.directory.FullName);
                                }

                                foreach (var (file, lastWriteTime, lastAccessTime, creationTime) in nonMonochromeImageFiles)
                                {
                                    file.LastWriteTimeUtc = lastWriteTime;
                                    file.LastAccessTimeUtc = lastAccessTime;
                                    file.CreationTimeUtc = creationTime;
                                    AddProgress(1, targetDirectoryInfo.directory.FullName);
                                }
                            }
                            else
                            {
                                AddProgress(monochromeImageFiles.Count + nonMonochromeImageFiles.Count, targetDirectoryInfo.directory.FullName);
                            }
                        }
                        catch (Exception ex)
                        {
                            ReportException(ex);
                        }
                    }
                }
            }
            catch (OperationCanceledException)
            {
                return ResultCode.Cancelled;
            }
            catch (Exception ex)
            {
                ReportException(ex);
                return ResultCode.Failed;
            }

            AddProgress(0);
            return ResultCode.Success;
        }

        protected override void Finish(ResultCode result, bool isLaunchedByConsoleApplicationLauncher)
        {
            if (result == ResultCode.Success)
                TinyConsole.WriteLine("終了しました。");
            else if (result == ResultCode.Cancelled)
                TinyConsole.WriteLine("中断しました。");

            if (isLaunchedByConsoleApplicationLauncher)
            {
                TinyConsole.Beep();
                TinyConsole.WriteLine("ENTER キーを押すをウィンドウが閉じます。");
                _ = TinyConsole.ReadLine();
            }
        }

        private static bool IsEnableImageDirectory(DirectoryPath? dir)
        {
            while (dir is not null)
            {
                if (dir.Name.StartsWith('.'))
                {
                    return false;
                }

                dir = dir.Parent;
            }

            return true;
        }

        private static (bool isWide, bool isMonochrome) CheckImage(FilePath imageFile)
        {
            try
            {
                if (string.Equals(imageFile.Extension, ".avif", StringComparison.OrdinalIgnoreCase))
                    return CheckImageForAvif(imageFile);
                else
                    return CheckImageForGenericImage(imageFile);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"画像の読み込みに失敗しました。: \"{imageFile.FullName}\"", ex);
            }
        }

        private static (bool isWide, bool isMonochrome) CheckImageForGenericImage(FilePath imageFile)
        {
            if (!OperatingSystem.IsWindowsVersionAtLeast(6, 1))
                throw new NotSupportedException();

            try
            {
                using var image = new Bitmap(imageFile.FullName);
                var isWide = CheckIfWideImage(image);
                var isMonochrome = CheckIfMonochromeImage(image);
                return (isWide, isMonochrome);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"画像ではないファイルが含まれています。: \"{imageFile.FullName}\"", ex);
            }
        }

        private static (bool isWide, bool isMonochrome) CheckImageForAvif(FilePath imageFile)
        {
            var baseDirectory = imageFile.Directory;
            var destinationImageFile = (FilePath?)null;
            try
            {
                for (var count = 0; ; ++count)
                {
                    var destinationFileName = $".temp-{count}.bmp";
                    try
                    {
                        destinationImageFile = baseDirectory.GetFile(destinationFileName);
                        using var s = destinationImageFile.CreateNew();
                        break;
                    }
                    catch (Exception)
                    {
                    }
                }

                Validation.Assert(destinationImageFile is not null);

                if (_ffmpegCommandPath is null)
                    throw new ApplicationException("AVIF ファイルをサポートするためには ffmpeg がインストールされている必要があります。");

                var startInfo = new ProcessStartInfo
                {
                    FileName = _ffmpegCommandPath.FullName,
                    Arguments = $"-hide_banner -y -i {imageFile.Name.EncodeCommandLineArgument()} -map 0:v:0 -frames:v:0 1 -update 1 {destinationImageFile.Name.EncodeCommandLineArgument()}",
                    WorkingDirectory = baseDirectory.FullName,
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    RedirectStandardInput = true,
                    StandardInputEncoding = Encoding.UTF8,
                    RedirectStandardOutput = true,
                    StandardOutputEncoding = Encoding.UTF8,
                    RedirectStandardError = true,
                    StandardErrorEncoding = Encoding.UTF8,
                };

                var process =
                    Process.Start(startInfo)
                    ?? throw new ApplicationException("ffmpeg の実行に失敗しました。");

                _ = Task.Run(() => CopyTextStream(TextReader.Null, process.StandardInput));
                var standardOutputHandler = Task.Run(() => CopyTextStream(process.StandardOutput, TextWriter.Null));
                var standardErrorHandler = Task.Run(() => CopyTextStream(process.StandardError, TextWriter.Null));
                Task.WaitAll(standardOutputHandler, standardErrorHandler);
                process.WaitForExit();
                if (process.ExitCode != 0)
                    throw new ApplicationException($"ffmpeg が異常終了しました。: exitCode={process.ExitCode}, command=\"{startInfo.FileName} {startInfo.Arguments}\"");

                return CheckImageForGenericImage(destinationImageFile);
            }
            finally
            {
                destinationImageFile?.SafetyDelete();
            }

            static void CopyTextStream(TextReader reader, TextWriter writer)
            {
                var buffer = new char[1024];
                while (true)
                {
                    var length = reader.Read(buffer, 0, buffer.Length);
                    if (length <= 0)
                        break;
                    writer.Write(buffer, 0, length);
                }
            }
        }

        private static bool CheckIfWideImage(Bitmap image)
        {
            if (!OperatingSystem.IsWindowsVersionAtLeast(6, 1))
                throw new NotSupportedException();

            return image.Width * 6 > image.Height * 5;
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        private static bool CheckIfMonochromeImage(Bitmap image)
        {
            if (!OperatingSystem.IsWindowsVersionAtLeast(6, 1))
                throw new NotSupportedException();

            // PixelFormat.Format32bppArgb の場合は 1 ピクセルは 4 バイト
            const PixelFormat pixelFormat = PixelFormat.Format32bppArgb;
            const int sizeOfPixel = 4;

            var bitmapData =
                image.LockBits(
                    new Rectangle(0, 0, image.Width, image.Height),
                    ImageLockMode.ReadOnly,
                    pixelFormat);
            try
            {
                var stride = Math.Abs(bitmapData.Stride);
                var imageBytesLength = stride * bitmapData.Height;
                if (imageBytesLength <= 0)
                {
                    // width あるいは height が 0の場合
                    return true;
                }

                Validation.Assert(imageBytesLength == image.Width * image.Height * 4);
                Validation.Assert(sizeof(uint) == 4);

                // stride はスキャンラインのバイト数を 4 の倍数に切り上げたものであるが、sizeOfPixel は 4 であるので、stride == sizeOfPixel * image.Width が成立するはず。
                // つまり、スキャンラインの末尾に余分な空白バイト列は存在しない。
                Validation.Assert(stride == sizeOfPixel * image.Width);

                unsafe
                {
                    //if((UIntPtr)bitmapData.Scan0 % sizeof(uint) == 0)
                    //    return BitmapOperation.IsMonochromeImageAligned((uint*)bitmapData.Scan0, (uint)imageBytesLength / sizeof(uint));
                    //else
                        return BitmapOperation.IsMonochromeImage((byte*)bitmapData.Scan0, (uint)imageBytesLength / sizeof(uint));
                }
            }
            finally
            {
                image.UnlockBits(bitmapData);
            }
        }

        private bool ExistDuplicateFileNames(DirectoryPath? baseDirectory, IEnumerable<string> fileNames)
        {
            var fileNamesArray = fileNames.ToArray();
            for (var index1 = 0; index1 < fileNamesArray.Length - 1; ++index1)
            {
                var fileName1 = fileNamesArray[index1];
                for (var index2 = index1 + 1; index2 < fileNamesArray.Length; ++index2)
                {
                    var fileName2 = fileNamesArray[index2];
                    if (!CheckFileNameAmbiguity(baseDirectory is null ? "" : baseDirectory.FullName, fileName1, fileName2))
                        return false;
                }
            }

            return true;
        }

        private static string GetTemporaryFilePrefix(DirectoryPath baseDirectory)
        {
            string temporaryPrefix;
            for (var index = 0; ; ++index)
            {
                temporaryPrefix = $"._{index}_";
                if (baseDirectory.EnumerateFiles(false).None(file => file.Name.StartsWith(temporaryPrefix, StringComparison.OrdinalIgnoreCase)))
                    break;
            }

            return temporaryPrefix;
        }

        private bool CheckFileNameAmbiguity(string dir, string fileName1, string fileName2)
        {
            var fileName1WithoutExtension = Path.GetFileNameWithoutExtension(fileName1);
            var fileName2WithoutExtension = Path.GetFileNameWithoutExtension(fileName2);
            var extension1 = Path.GetExtension(fileName1);
            var extension2 = Path.GetExtension(fileName2);
            if (string.Equals(fileName1WithoutExtension, fileName2WithoutExtension, StringComparison.OrdinalIgnoreCase) &&
                !string.Equals(extension1, extension2, StringComparison.OrdinalIgnoreCase))
            {
                ReportWarningMessage($"同一ディレクトリ配下に、拡張子のみが異なるファイル名があります。ファイル名の番号付の順序が曖昧であるため、このディレクトリ配下のファイルの名前の変名は行いません。: dir=\"{dir}\", name=\"{fileName1WithoutExtension}\", ext1=\"{extension1}\", ext2=\"{extension2}\"");
                return false;
            }

            var length = fileName1.Length;
            if (length > fileName2.Length)
                length = fileName2.Length;
            for (var index = 0; index < length; ++index)
            {
                var c1 = fileName1[index];
                var c2 = fileName2[index];
                if (c1 == c2)
                {
                    // ここまで同じ文字列である場合

                    // 続行する
                }
                else if (c1 >= '0' && c1 <= '9' && c2 >= '0' && c2 <= '9')
                {
                    // 異なる文字であり、数字どうしの比較である

                    // 中断して OK を返す
                    return true;
                }
                else if (c1 >= 'a' && c1 <= 'z' && c2 >= 'a' && c2 <= 'z')
                {
                    // 異なる文字であり、英子文字どうしの比較である

                    // 中断して OK を返す
                    return true;
                }
                else if (c1 >= 'A' && c1 <= 'Z' && c2 >= 'A' && c2 <= 'Z')
                {
                    // 異なる文字であり、英大文字どうしの比較である

                    // 中断して OK を返す
                    return true;
                }
                else
                {
                    // 異なる文字であり、それ以外の文字どうしの比較である

                    // 環境によって順序が異なることがあるため、NG を返す
                    ReportWarningMessage($"同一ディレクトリ配下に順番が曖昧なファイル名が存在するため、このディレクトリ配下のファイルの名前の変名は行いません。: dir=\"{dir}\", name1=\"{fileName1}\", name2=\"{fileName2}\"");
                    return false;
                }
            }

            // この時点で、fileName1 と fileName2 の何れかの長さは length であり、かつ fileName1 と fileName2 は 最初の length 文字までは等しい。

            // fileName1 と fileName2 が等しいことはありえないため、fileName1 が fileName2 の部分文字列か、あるいはその逆である。
            // 2つのファイル名が部分文字列の関係にある場合、順序の曖昧さの原因となるため、NG を返す。

            ReportWarningMessage($"同一ディレクトリ配下に順番が曖昧なファイル名が存在するため、このディレクトリ配下のファイルの名前の変名は行いません。: dir=\"{dir}\", name1=\"{fileName1}\", name2=\"{fileName2}\"");
            return false;
        }

        private static DateTime Minimum(DateTime dateTime1, DateTime dateTime2)
        {
            if (dateTime1 != DateTime.MaxValue && dateTime2 != DateTime.MaxValue && dateTime1.Kind != dateTime2.Kind)
                throw new Exception();

            return
                dateTime1 == DateTime.MaxValue
                ? dateTime2
                : dateTime2 == DateTime.MaxValue
                ? dateTime1
                : dateTime1.CompareTo(dateTime2) < 0
                ? dateTime1
                : dateTime2;
        }

        private void AddProgress(int deltaFileCount)
        {
            _fileCount += deltaFileCount;

            ReportProgress((double)_fileCount / _totalFileCount, "", (percentage, content) => $"変名中… {percentage}");
        }

        private void AddProgress(int deltaFileCount, string path)
        {
            _fileCount += deltaFileCount;

            ReportProgress((double)_fileCount / _totalFileCount, path, (percentage, content) => $"変名中… {percentage} (\"{content}\")");
        }
    }
}
