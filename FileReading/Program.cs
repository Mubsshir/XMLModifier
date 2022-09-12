using System;
using System.IO;

namespace FileReading
{
    public class Program
    {
        static void Main(String[] args)
        {
            string path = @"E:\TestFolder\TestDirectory\";
            //Make two sub directories one for converted file and one for backup file
            C_FileHandler.Fn_MakeSubDirectories(path);
            C_FileHandler.Fn_ReadDirectoryAndConvertFiles(path);
            FileSysEvents.WatchForDirectoryChanges(path);
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("Looking for changes in Directory, Don't press any key\n\n__________________________________________________________________");
            Console.ReadKey();
        }
    }
}

