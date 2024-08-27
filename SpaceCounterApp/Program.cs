using System.Drawing;
using System.IO;

namespace SpaceCounterApp
{
        internal class Program
        {
            static void Main(string[] args)
            {
                Console.OutputEncoding = System.Text.Encoding.Unicode;
                string filePath = "";
                if (args.Length > 0)
                {
                    filePath = args[0];
                }
                else
                {
                    Console.WriteLine("The path to the directory has not been provided. Please try again.");
                    return;
                }

                CheckDirectoryAndPrintSize(filePath);
                CalculateNumberOfFilesAndSpaceRemoved(filePath);
                ClearDirectory(filePath);
                CheckDirectoryAndPrintSize(filePath);
            }

            public static void CheckDirectoryAndPrintSize(string filePath)
            {
                try
                {
                    if (Directory.Exists(filePath))
                    {
                        long size = 0;
                        DirectoryInfo directoryInfo = new DirectoryInfo(filePath);

                        size = CalculateDirectorySize(directoryInfo);
                        Console.WriteLine($"Total size of the directory: {size} byte");

                    }
                    else
                    {
                        Console.WriteLine("No directory has been found at this file path. Make sure that the file path is correct.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"There was an issue: {ex.Message}");
                }
            }
            public static void CalculateNumberOfFilesAndSpaceRemoved(string filePath)
            {
                int fileCount = System.IO.Directory.GetFiles(filePath, "*.*", SearchOption.AllDirectories).Count();
                Console.WriteLine($"{fileCount} files will be removed from the requested folder");

                DirectoryInfo directoryInfo = new DirectoryInfo(filePath);
                var sizeOfFiles = CalculateDirectorySize(directoryInfo);
                Console.WriteLine($"The space of those files to be removed is {sizeOfFiles}");
            }
            public static void ClearDirectory(string filePath)
            {
                try
                {
                    DirectoryInfo dirInfo = new DirectoryInfo(filePath);
                    dirInfo.Delete(true);
                    Console.WriteLine("The catalog has been deleted.");
                }
                catch(UnauthorizedAccessException ex)
                {
                    Console.WriteLine("You need admin rights to do this", ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            public static long CalculateDirectorySize(DirectoryInfo d)
            {
                long size = 0;

                FileInfo[] fileInfos = d.GetFiles();

                foreach (FileInfo fileInfo in fileInfos)
                {
                    size += fileInfo.Length;
                }

                DirectoryInfo[] dis = d.GetDirectories();
                foreach (DirectoryInfo di in dis)
                {
                    size += CalculateDirectorySize(di);
                }
                return size;
            }
        }
}
