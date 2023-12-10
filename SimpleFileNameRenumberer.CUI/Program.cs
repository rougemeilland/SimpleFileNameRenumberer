using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SimpleFileNameRenumberer.CUI
{

    internal class Program
    {
        private readonly static string _thisProgramName;

        static Program()
        {
            _thisProgramName = Path.GetFileNameWithoutExtension(typeof(Program).Assembly.Location);
        }

        private static int Main(string[] args)
        {
            var prefix = (string?)null;
            var extensions = new List<string>();
            var onlyImageFile = false;
            var resetMonochromeImageTimestamp = false;
            var targetFiles = new List<FileInfo>();
            for (var index = 0; index < args.Length; ++index)
            {
                var arg = args[index];
                if (arg == "--prefix")
                {
                    if (index + 1 >= args.Length)
                    {
                        PrintErrorMessage("--prefixオプション の値が指定されていません。");
                        return 1;
                    }

                    prefix = args[index + 1];
                    ++index;
                }
                else if (arg == "--extension")
                {
                    if (index + 1 >= args.Length)
                    {
                        PrintErrorMessage("--extensionオプション の値が指定されていません。");
                        return 1;
                    }

                    foreach (var ext in args[index + 1].Split(","))
                    {
                        if (ext.Length <= 0)
                        {
                            PrintErrorMessage("--extension オプションで長さが 0 の拡張子が指定されています。");
                            return 1;
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
                        PrintErrorMessage($"このオペレーティングシステムではサポートされていないオプションが指定されています。: \"{arg}\"");
                        return 1;
                    }

                    onlyImageFile = true;
                }
                else if (arg == "--reset_monochrome_image_timestamp")
                {
                    if (!OperatingSystem.IsWindowsVersionAtLeast(6, 1))
                    {
                        PrintErrorMessage($"このオペレーティングシステムではサポートされていないオプションが指定されています。: \"{arg}\"");
                        return 1;
                    }

                    resetMonochromeImageTimestamp = true;
                }
                else if (arg.StartsWith('-'))
                {
                    PrintErrorMessage($"サポートされていないオプションが指定されています。: \"{arg}\"");
                    return 1;
                }
                else
                {
                    try
                    {
                        if (File.Exists(arg))
                        {
                            targetFiles.Add(new FileInfo(arg));
                        }
                        else if (Directory.Exists(arg))
                        {
                            foreach (var file in new DirectoryInfo(arg).EnumerateFiles("*", SearchOption.AllDirectories))
                                targetFiles.Add(file);
                        }
                        else
                        {
                            PrintErrorMessage($"指定されたパス名が存在しません。: \"{arg}\"");
                            return 1;
                        }
                    }
                    catch (IOException)
                    {
                        PrintErrorMessage($"指定されたパス名のファイルまたはディレクトリにアクセスできません。: \"{arg}\"");
                        return 1;
                    }
                }
            }

            if (resetMonochromeImageTimestamp && !onlyImageFile)
            {
                PrintErrorMessage("--reset_monochrome_image_timestamp オプションは --only_image_file とともに指定する必要があります。");
                return 1;
            }

            if (extensions.Count > 0)
            {
                targetFiles =
                    targetFiles
                    .Where(file => extensions.Contains(file.Extension.ToUpperInvariant()))
                    .ToList();
            }

            try
            {
                if (targetFiles.Count > 0)
                {
                    InitializeProgress(targetFiles.Count * 5);
                    AddProgress(0);

                    var targetDirectoryInfos =
                        targetFiles
                        .Select(file =>
                        {
                            var fileInfo = new
                            {
                                directory = file.Directory ?? throw new Exception($"不正なファイル名です。: \"{file.FullName}\""),
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
                        var monochromeImageFiles = new List<FileInfo>();
                        var nonMonochromeImageFiles = new List<(FileInfo imageFile, DateTime lastWriteTime, DateTime lastAccessTime, DateTime creationTime)>();
                        if (!targetDirectoryInfo.isEnabledImageDirectory)
                        {
                            foreach (var file in targetDirectoryInfo.imageFiles)
                                nonMonochromeImageFiles.Add((file, file.LastWriteTimeUtc, Minimum(file.LastAccessTimeUtc, file.LastWriteTimeUtc), Minimum(file.CreationTimeUtc, file.LastWriteTimeUtc)));
                            AddProgress(targetDirectoryInfo.imageFiles.Count * 4);
                        }
                        else if (targetDirectoryInfo.imageFiles.Count < 2)
                        {
                            PrintWarningMessage($"ディレクトリ配下にファイルが1つしかないため変名を行いません。: \"{targetDirectoryInfo.directory.FullName}\"");
                            foreach (var file in targetDirectoryInfo.imageFiles)
                                nonMonochromeImageFiles.Add((file, file.LastWriteTimeUtc, Minimum(file.LastAccessTimeUtc, file.LastWriteTimeUtc), Minimum(file.CreationTimeUtc, file.LastWriteTimeUtc)));
                            AddProgress(targetDirectoryInfo.imageFiles.Count * 4);
                        }
                        else
                        {
                            var imageFileDirectory = targetDirectoryInfo.directory;
                            if (!ExistDuplicateFileNames(imageFileDirectory, targetDirectoryInfo.imageFiles.Select(imageFile => imageFile.Name)))
                            {
                                AddProgress(targetDirectoryInfo.imageFiles.Count * 5);
                            }
                            else
                            {
                                var fileInfos =
                                    targetDirectoryInfo.imageFiles
                                    .Select(imageFile =>
                                    {
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
                                            AddProgress(1);
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
                                            AddProgress(1);
                                            return value;
                                        }
                                    })
                                    .ToList();
                                if (targetDirectoryInfo.isEnabledImageDirectory)
                                {
                                    var temporaryPrefix = GetTemporaryFilePrefix(imageFileDirectory);
                                    var totalPageCount = fileInfos.Sum(item => item.isWideImage ? 2 : 1);
                                    var numberWidth = totalPageCount.ToString().Length;
                                    var numberFormat = $"D{numberWidth}";
                                    var pageCount = 1;

                                    var fileNameMaps =
                                        fileInfos
                                        .OrderBy(fileInfo => fileInfo.imageFile.Name, StringComparer.OrdinalIgnoreCase)
                                        .Select((fileInfo, index) =>
                                        {
                                            var isFirstPage = pageCount == 1;
                                            var isWideImage = !isFirstPage && fileInfo.isWideImage;
                                            var destinationPageNumber = isWideImage ? $"{pageCount.ToString(numberFormat)}-{(pageCount + 1).ToString(numberFormat)}" : pageCount.ToString(numberFormat);
                                            pageCount += isWideImage ? 2 : 1;
                                            var extension = fileInfo.imageFile.Extension;
                                            var destinationFile = new FileInfo(Path.Combine(imageFileDirectory.FullName, $"{prefix ?? ""}{destinationPageNumber}{extension}"));

                                            if ((pageCount & 1) != 0 && isWideImage)
                                                PrintWarningMessage($"見開きページが偶数ページ番号ではありません。: page=\"{destinationPageNumber}\", file=\"{destinationFile.FullName}\"");

                                            return new
                                            {
                                                sourceFile = fileInfo.imageFile,
                                                temporaryFileName = Path.Combine(imageFileDirectory.FullName, $"{temporaryPrefix}{destinationPageNumber}{extension}"),
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
                                            File.Move(fileNameMap.sourceFile.FullName, fileNameMap.temporaryFileName);
                                        AddProgress(1);
                                    }

                                    foreach (var fileNameMap in fileNameMaps)
                                    {
                                        if (!string.Equals(fileNameMap.sourceFile.FullName, fileNameMap.destinationFile.FullName, StringComparison.OrdinalIgnoreCase))
                                            File.Move(fileNameMap.temporaryFileName, fileNameMap.destinationFile.FullName);
                                        AddProgress(1);
                                    }

                                    foreach (var fileNameMap in fileNameMaps)
                                    {
                                        if (fileNameMap.isMonochromeImage)
                                            monochromeImageFiles.Add(fileNameMap.destinationFile);
                                        else
                                            nonMonochromeImageFiles.Add((fileNameMap.destinationFile, fileNameMap.lastWriteTime, fileNameMap.lastAccessTime, fileNameMap.creationTime));
                                        AddProgress(1);
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
                                        AddProgress(3);
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
                                monochromeImageFile.Refresh();
                                if (minimumLastWriteTime != DateTime.MaxValue)
                                    monochromeImageFile.LastWriteTimeUtc = minimumLastWriteTime;
                                if (minimumLastAccessTime != DateTime.MaxValue)
                                    monochromeImageFile.LastAccessTimeUtc = minimumLastAccessTime;
                                if (minimumCreationTime != DateTime.MaxValue)
                                    monochromeImageFile.CreationTimeUtc = minimumCreationTime;
                                monochromeImageFile.Refresh();
                                AddProgress(1);
                            }

                            foreach (var (file, lastWriteTime, lastAccessTime, creationTime) in nonMonochromeImageFiles)
                            {
                                file.Refresh();
                                file.LastWriteTimeUtc = lastWriteTime;
                                file.LastAccessTimeUtc = lastAccessTime;
                                file.CreationTimeUtc = creationTime;
                                file.Refresh();
                                AddProgress(1);
                            }
                        }
                        else
                        {
                            AddProgress(monochromeImageFiles.Count + nonMonochromeImageFiles.Count);
                        }
                    }

                    Console.WriteLine();
                }
            }
            catch (Exception ex)
            {
                PrintErrorMessage(ex.Message);
                return 1;
            }

            Console.WriteLine();
            Console.WriteLine("完了しました。");
            Console.Beep();
            _ = Console.ReadLine();

            return 0;
        }

        private static bool IsEnableImageDirectory(DirectoryInfo? dir)
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

        private static (bool isWide, bool isMonochrome) CheckImage(FileInfo imageFile)
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
                throw new Exception($"画像ではないファイルが含まれています。: \"{imageFile.FullName}\"", ex);
            }
        }

        private static bool CheckIfWideImage(Bitmap image)
        {
            if (!OperatingSystem.IsWindowsVersionAtLeast(6, 1))
                throw new NotSupportedException();

            return image.Width > image.Height;
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
                    ImageLockMode.ReadOnly, pixelFormat);
            try
            {
                var stride = Math.Abs(bitmapData.Stride);
                var imageBytesLength = stride * bitmapData.Height;
                if (imageBytesLength <= 0)
                {
                    // width あるいは height が 0の場合
                    return true;
                }
                var imageBytes = new byte[imageBytesLength];
                Marshal.Copy(bitmapData.Scan0, imageBytes, 0, imageBytes.Length);
                var columnLength = bitmapData.Width * sizeOfPixel;
                unsafe
                {
                    fixed (byte* imageBytesTop = imageBytes)
                    {
                        var endOfBytes = imageBytesTop + imageBytes.Length;
                        if (stride == sizeOfPixel * image.Width)
                        {
                            // 1 行の右端までピクセルデータがみっしりと詰まっている場合

                            if (sizeOfPixel == sizeof(uint) && (unchecked((int)imageBytesTop) & (sizeof(uint) - 1)) == 0)
                            {
                                // ポインタ imageBytesTop が uint のアラインメント境界上にある場合
                                // (おそらく大半がこのルートに到達する)

                                // imageBytesTop から始まるデータを uint の配列として扱う
                                var ptr = (uint*)imageBytesTop;
                                var samplePixel = *ptr++;
                                while (ptr < endOfBytes)
                                {
                                    if (*ptr++ != samplePixel)
                                        return false;
                                }
                            }
                            else
                            {
                                // ポインタ imageBytesTop が uint のアラインメント境界上にない場合

                                var samplePixel = GetPixel(imageBytesTop);
                                for (var p = imageBytesTop + sizeOfPixel; p < endOfBytes; p += sizeOfPixel)
                                {
                                    if (GetPixel(p) != samplePixel)
                                        return false;
                                }
                            }
                        }
                        else
                        {
                            // 1 行の右端に余白がある場合
                            // (1 ピクセルが 4 バイトであり、かつ .NET の仕様上 stride は 4 の倍数なので、このルートに到達することはほぼない)

                            var samplePixel = GetPixel(imageBytesTop);
                            var row = imageBytesTop;
                            while (row < endOfBytes)
                            {
                                var endOfColumn = row + columnLength;
                                var column = row;
                                while (column < endOfColumn)
                                {
                                    if (GetPixel(column) != samplePixel)
                                        return false;
                                    column += sizeOfPixel;
                                }

                                row += stride;
                            }
                        }

                        return true;
                    }
                }
            }
            finally
            {
                image.UnlockBits(bitmapData);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
            static unsafe uint GetPixel(byte* p)
                => ((uint)p[0] << (8 * 0))
                    | ((uint)p[1] << (8 * 1))
                    | ((uint)p[2] << (8 * 2))
                    | ((uint)p[3] << (8 * 3));
        }

        private static bool ExistDuplicateFileNames(DirectoryInfo? baseDirectory, IEnumerable<string> fileNames)
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

        private static string GetTemporaryFilePrefix(DirectoryInfo baseDirectory)
        {
            string temporaryPrefix;
            for (var index = 0; ; ++index)
            {
                temporaryPrefix = $"._{index}_";
                if (!baseDirectory.EnumerateFiles(temporaryPrefix + "*", SearchOption.TopDirectoryOnly).Any())
                    break;
            }

            return temporaryPrefix;
        }

        private static bool CheckFileNameAmbiguity(string dir, string fileName1, string fileName2)
        {
            var fileName1WithoutExtension = Path.GetFileNameWithoutExtension(fileName1);
            var fileName2WithoutExtension = Path.GetFileNameWithoutExtension(fileName2);
            var extension1 = Path.GetExtension(fileName1);
            var extension2 = Path.GetExtension(fileName2);
            if (string.Equals(fileName1WithoutExtension, fileName2WithoutExtension, StringComparison.OrdinalIgnoreCase) &&
                !string.Equals(extension1, extension2, StringComparison.OrdinalIgnoreCase))
            {
                PrintWarningMessage($"同一ディレクトリ配下に、拡張子のみが異なるファイル名があります。ファイル名の番号付の順序が曖昧であるため、このディレクトリ配下のファイルの名前の変名は行いません。: dir=\"{dir}\", name=\"{fileName1WithoutExtension}\", ext1=\"{extension1}\", ext2=\"{extension2}\"");
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
                    PrintWarningMessage($"同一ディレクトリ配下に順番が曖昧なファイル名が存在するため、このディレクトリ配下のファイルの名前の変名は行いません。: dir=\"{dir}\", name1=\"{fileName1}\", name2=\"{fileName2}\"");
                    return false;
                }
            }

            // この時点で、fileName1 と fileName2 の何れかの長さは length であり、かつ fileName1 と fileName2 は 最初の length 文字までは等しい。

            // fileName1 と fileName2 が等しいことはありえないため、fileName1 が fileName2 の部分文字列か、あるいはその逆である。
            // 2つのファイル名が部分文字列の関係にある場合、順序の曖昧さの原因となるため、NG を返す。

            PrintWarningMessage($"同一ディレクトリ配下に順番が曖昧なファイル名が存在するため、このディレクトリ配下のファイルの名前の変名は行いません。: dir=\"{dir}\", name1=\"{fileName1}\", name2=\"{fileName2}\"");
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

        private static int _totalFileCount = 0;
        private static int _fileCount = 0;
        private static string _currentProgressText = "";

        private static void InitializeProgress(int totalFileCount)
        {
            _totalFileCount = totalFileCount;
            _fileCount = 0;
            _currentProgressText = "";
            Console.WriteLine();
        }

        private static void AddProgress(int deltaFileCount)
        {
            _fileCount += deltaFileCount;
            var progressText = $" 変名中… {(double)_fileCount / _totalFileCount * 100:F2}%";
            if (_currentProgressText != progressText)
            {
                _currentProgressText = progressText;
                Console.Write($"{_currentProgressText}\r");
            }
        }

        private static void PrintWarningMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"{_thisProgramName}:WARNING:{message}");
            Console.ResetColor();
        }

        private static void PrintErrorMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"{_thisProgramName}:ERROR:{message}");
            Console.ResetColor();
        }
    }
}
