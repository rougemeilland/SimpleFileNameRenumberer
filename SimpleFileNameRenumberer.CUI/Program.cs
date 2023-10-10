using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.IO;
using System.Linq;

namespace SimpleFileNameRenumberer.CUI
{

    internal class Program
    {
        private readonly static string _thisProgramName;

        static Program()
            => _thisProgramName = Path.GetFileNameWithoutExtension(typeof(Program).Assembly.Location);

        private static int Main(string[] args)
        {
            var prefix = (string?)null;
            var extensions = new List<string>();
            var onlyImageFile = false;
            var targetFiles = new List<FileInfo>();
            for (var index = 0; index < args.Length; ++index)
            {
                var arg = args[index];
                if (arg == "-prefix")
                {
                    if (index + 1 >= args.Length)
                    {
                        PrintErrorMessage("-prefixオプション の値が指定されていません。");
                        return 1;
                    }

                    prefix = args[index + 1];
                    ++index;
                }
                else if (arg == "-extension")
                {
                    if (index + 1 >= args.Length)
                    {
                        PrintErrorMessage("-extensionオプション の値が指定されていません。");
                        return 1;
                    }

                    foreach (var ext in args[index + 1].Split(","))
                    {
                        if (ext.Length <= 0)
                        {
                            PrintErrorMessage("-extension オプションで長さが 0 の拡張子が指定されています。");
                            return 1;
                        }

                        var upperExt = $".{ext.ToUpperInvariant()}";
                        if (!extensions.Contains(upperExt))
                            extensions.Add(upperExt);
                    }

                    ++index;
                }
                else if (arg == "-only_image_file")
                {
                    onlyImageFile = true;
                }
                else if (arg.StartsWith("-", StringComparison.Ordinal))
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

            if (extensions.Any())
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
                    InitializeProgress(targetFiles.Count * (onlyImageFile ? 2 : 1));
                    AddProgress(0);

                    var targetDirectoryInfos =
                        targetFiles
                        .Select(imageFile =>
                        {
                            var fileInfo = new
                            {
                                dir = imageFile.Directory ?? throw new Exception($"不正なファイル名です。: \"{imageFile.FullName}\""),
                                imageFile,
                                ext = imageFile.Extension.ToUpperInvariant(),
                                isWide = onlyImageFile ? IsWideImage(imageFile) : false,
                            };

                            if (onlyImageFile)
                                AddProgress(1);
                            return fileInfo;
                        })
                        .GroupBy(item => item.dir.FullName)
                        .Select(g => new
                        {
                            g.First().dir,
                            fileInfos = g.Select(item => new { item.imageFile, item.isWide }).ToList(),
                        })
                        .ToList();

                    foreach (var targetDirectoryInfo in targetDirectoryInfos)
                    {
                        try
                        {
                            if (IsEnableImageDirectory(targetDirectoryInfo.dir))
                            {
                                if (targetDirectoryInfo.fileInfos.Count < 2)
                                {
                                    PrintWarningMessage($"ディレクトリ配下にファイルが1つしかないため変名を行いません。: \"{targetDirectoryInfo.dir}\"");
                                }
                                else
                                {
                                    if (ExistDuplicateFileNames(targetDirectoryInfo.dir, targetDirectoryInfo.fileInfos.Select(fileInfo => fileInfo.imageFile.Name)))
                                    {
                                        var temporaryPrefix = GetTemporaryFilePrefix(targetDirectoryInfo.dir);
                                        var totalPageCount = targetDirectoryInfo.fileInfos.Sum(item => item.isWide ? 2 : 1);
                                        var numberWidth = totalPageCount.ToString().Length;
                                        var numberFormat = $"D{numberWidth}";
                                        var pageCount = 1;

                                        var fileNameMaps =
                                            targetDirectoryInfo.fileInfos
                                            .OrderBy(fileInfo => fileInfo.imageFile.Name, StringComparer.OrdinalIgnoreCase)
                                            .Select((fileInfo, index) =>
                                            {
                                                var isFirstPage = pageCount == 1;
                                                var destinationPageNumber = fileInfo.isWide ? $"{pageCount.ToString(numberFormat)}-{(pageCount + 1).ToString(numberFormat)}" : pageCount.ToString(numberFormat);
                                                pageCount += !isFirstPage && fileInfo.isWide ? 2 : 1;
                                                var extension = fileInfo.imageFile.Extension;
                                                var destinationFileName = Path.Combine(targetDirectoryInfo.dir.FullName, $"{prefix ?? ""}{destinationPageNumber}{extension}");

                                                if (!isFirstPage && (pageCount & 1) != 0 && fileInfo.isWide)
                                                    PrintWarningMessage($"見開きページが偶数ページ番号ではありません。: page=\"{destinationPageNumber}\", file=\"{destinationFileName}\"");

                                                return new
                                                {
                                                    sourceFileName = fileInfo.imageFile,
                                                    temporaryFileName = Path.Combine(targetDirectoryInfo.dir.FullName, $"{temporaryPrefix}{destinationPageNumber}{extension}"),
                                                    destinationFileName,
                                                };
                                            })
                                            .ToList();

                                        foreach (var fileNameMap in fileNameMaps)
                                        {
                                            if (!string.Equals(fileNameMap.sourceFileName.FullName, fileNameMap.destinationFileName, StringComparison.OrdinalIgnoreCase))
                                                File.Move(fileNameMap.sourceFileName.FullName, fileNameMap.temporaryFileName);
                                        }

                                        foreach (var fileNameMap in fileNameMaps)
                                        {
                                            if (!string.Equals(fileNameMap.sourceFileName.FullName, fileNameMap.destinationFileName, StringComparison.OrdinalIgnoreCase))
                                                File.Move(fileNameMap.temporaryFileName, fileNameMap.destinationFileName);
                                        }
                                    }
                                }
                            }
                        }
                        finally
                        {
                            AddProgress(targetDirectoryInfo.fileInfos.Count);
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

        [SuppressMessage("Interoperability", "CA1416:プラットフォームの互換性を検証", Justification = "<保留中>")]
        private static bool IsWideImage(FileInfo imageFile)
        {
            try
            {
                using var image = new Bitmap(imageFile.FullName);
                return image.Width > image.Height;
            }
            catch (Exception ex)
            {
                throw new Exception($"画像ではないファイルが含まれています。: \"{imageFile.FullName}\"", ex);
            }
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
