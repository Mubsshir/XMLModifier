using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileReading
{
    public class FileSysEvents
    {
        public static void WatchForDirectoryChanges(string DirectoryPath, string Filter = "*.*")
        {
            FileSystemWatcher watcher = new FileSystemWatcher()
            {
                Path = DirectoryPath,
                Filter = Filter
            };
            watcher.Created += OnChanged;
            watcher.Deleted += OnDeleting;
            watcher.EnableRaisingEvents = true;
        }
        public static void OnChanged(object sender, FileSystemEventArgs e)
        {
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine($"_______________________________________________________\n   Changes Occure In {Path.GetDirectoryName(e.FullPath)}\n");
            Console.WriteLine(e.ChangeType);
            Console.WriteLine(e.Name + "\n\n_______________________________________________________");
            Console.ResetColor();
            C_FileHandler.Fn_ReadDirectoryAndConvertFiles(Path.GetDirectoryName(e.FullPath));
        }
        public static void OnDeleting(object sender, FileSystemEventArgs e)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine($"_______________________________________________________\n   Changes Occure In {Path.GetDirectoryName(e.FullPath)}   \n");
            Console.WriteLine(e.ChangeType);
            Console.WriteLine(e.Name + " \n\n_______________________________________________________");
            Console.ResetColor();
            C_FileHandler.Fn_ReadDirectoryAndConvertFiles(Path.GetDirectoryName(e.FullPath));
        }
    }
}
