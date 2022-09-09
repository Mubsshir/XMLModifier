using System;
using System.IO;

namespace FileReading
{
    public class Program
    {
        public static void OnChanged(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine($"++++    Changed Occure In {Path.GetDirectoryName(e.FullPath)}   ++++");
            Console.WriteLine(e.ChangeType);
            Console.WriteLine(e.Name);
            FileMover.Fn_ReadDirectoryAndConvertFiles(Path.GetDirectoryName(e.FullPath));
        }
        public static void WatchForDirectoryChanges(string DirectoryPath,string Filter = "*.*")
        {
            FileSystemWatcher watcher = new FileSystemWatcher()
            {
                Path = DirectoryPath,
                Filter =Filter
            };
            watcher.Created += OnChanged;
            watcher.EnableRaisingEvents = true;
        }
        static void Main(String[] args)
        {
            string path = @"D:\TestDirectory";
            //Make two sub directories one for converted file and one for backup file
            FileMover.Fn_MakeSubDirectories(path);
            FileMover.Fn_ReadDirectoryAndConvertFiles(path);
            WatchForDirectoryChanges(path);
            Console.WriteLine("Press Any Key To exit");
            Console.ReadKey();
        }
    }
}

