
namespace FileReading
{
    public class FileMover
    {
        public static void Fn_MoveFile(string SourceFilePath, string DestinationFullPath)
        {
            try
            {
                File.Move(SourceFilePath, DestinationFullPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error catched: " + ex.Message);
                string NewFileName = DestinationFullPath;
                int count = 1;
                while (File.Exists(NewFileName))
                {
                    NewFileName = Path.Combine(Path.GetDirectoryName(NewFileName), Path.GetFileNameWithoutExtension(SourceFilePath) + count + Path.GetExtension(SourceFilePath));
                    count++;
                }
                File.Move(SourceFilePath, NewFileName);
            }
        }

        public static void Fn_MakeSubDirectories(string DirectoryPath)
        {
            Console.WriteLine("Creating Folders.......\n");
            if (Directory.Exists(DirectoryPath + @"\Backup") && Directory.Exists(DirectoryPath + @"\Converted"))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Folders Already Exists\n");
                Console.ResetColor();
            }
            else
            {
                Directory.CreateDirectory(DirectoryPath + @"\Backup");
                Directory.CreateDirectory(DirectoryPath + @"\Converted");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Sub-Directories Created\n");
                Console.ResetColor();
            }
        }
        public static void Fn_ReadDirectoryAndConvertFiles(string path)
        {
            List<string> Files = Directory.GetFiles(path, "*.xml", SearchOption.TopDirectoryOnly).ToList();
            string ConvertedFileDir = path + @"\Converted\";
            string BackupFileDir = path + @"\Backup\";

            foreach (string File in Files)
            {
                string fileName = Path.GetFileName(File);
                string newXML = ReadXML.Fn_ReadXML(File);
                Console.WriteLine(newXML);
                using (FileStream file = new FileStream(ConvertedFileDir + fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    StreamWriter streamWriter = new StreamWriter(file);
                    streamWriter.Write(newXML);
                    streamWriter.Close();

                }
                Fn_MoveFile(File, BackupFileDir + fileName);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("New File Created: " + ConvertedFileDir + fileName);
                Console.WriteLine("File: {0}  Moved To {1}: ", fileName, BackupFileDir);
                Console.ResetColor();
            }
        }
    }
}
