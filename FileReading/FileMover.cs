namespace FileReading
{
    public class C_FileHandler
    {
        public static void Fn_MakeSubDirectories(string DirectoryPath)
        {
            Console.WriteLine("Creating Sub-directories .......\n");
            if (Directory.Exists(DirectoryPath + @"\Backup") && Directory.Exists(DirectoryPath + @"\Converted"))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Sub-directories already Exists\n");
                Console.ResetColor();
            }
            else
            {
                Directory.CreateDirectory(DirectoryPath + @"\Backup");
                Directory.CreateDirectory(DirectoryPath + @"\Converted");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Sub-Directories {0} and {1} Created\n", DirectoryPath + @"\Backup", DirectoryPath + @"\Converted");
                Console.ResetColor();
            }
        }
        public static void Fn_ReadDirectoryAndConvertFiles(string path)
        {
            List<string> Files = Directory.GetFiles(path, "*.xml", SearchOption.TopDirectoryOnly).ToList();
            string ConvertedFileDir = path + @"\Converted\";
            string BackupFileDir = path + @"\Backup\";
            if (!Directory.Exists(ConvertedFileDir) || !Directory.Exists(BackupFileDir))
            {
                Fn_MakeSubDirectories(path);
            }
            foreach (string file in Files)
            {
                string fileName = Path.GetFileNameWithoutExtension(file);
                string fileExtension = Path.GetExtension(file);
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("___________________________________________\nWorking On-->"+fileName+fileExtension);
                if (File.Exists(ConvertedFileDir+fileName+fileExtension)||File.Exists(BackupFileDir+fileName+ fileExtension))
                {
                    Console.WriteLine("File already exists");
                    string newFileName = fileName;
                    int count = 1;
                    while(File.Exists(ConvertedFileDir + newFileName + fileExtension)|| File.Exists(BackupFileDir + newFileName + fileExtension))
                    {
                        newFileName = fileName + "__" + count;
                        count++;
                    }
                    Console.WriteLine("\nRenamed File-->{0}\n___________________________________________", newFileName + fileExtension);
                    fileName = newFileName;

                }
                string newXML = ReadXML.Fn_ReadXML(file);
                using (FileStream NewFile = new FileStream(ConvertedFileDir + fileName+fileExtension, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    StreamWriter streamWriter = new StreamWriter(NewFile);
                    streamWriter.Write(newXML);
                    streamWriter.Close();
                }
                File.Move(file, BackupFileDir + fileName+fileExtension);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("New File Created: " + ConvertedFileDir + fileName+fileExtension);
                Console.WriteLine("File: {0}  Moved To {1} ", fileName, BackupFileDir);
                Console.ResetColor();
            }
        }
    }
}
