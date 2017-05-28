using System;
using System.IO;
using System.Text;

namespace FileDemo
{
    public class FileManager
    { 
        public FileManager(){}

        public void InformationAboutFile(FileInstance fileInstance)
        {
            if (fileInstance.FileInfo.Exists)
            {
                Console.WriteLine("File Name: {0}", fileInstance.FileInfo.Name);
                Console.WriteLine("CreationTime: {0}", fileInstance.FileInfo.CreationTime);
                Console.WriteLine("Size: {0}", fileInstance.FileInfo.Length);
            }
        }

        public void DeleteFile(FileInstance fileInstance)
        {
            if (fileInstance.FileInfo.Exists)
            {
                fileInstance.FileInfo.Delete();
                Console.WriteLine("File deleted succesfully");
            }
        }

        public void TransferFile(FileInstance fileInstance, string newFilePath)
        {
            if (fileInstance.FileInfo.Exists)
            {
                fileInstance.FileInfo.MoveTo(newFilePath);
                Console.WriteLine("File transfered succesfully");
            }
        }
        public void CopyFile(FileInstance fileInstance, string newFilePath)
        {
            if (fileInstance.FileInfo.Exists)
            {
                fileInstance.FileInfo.CopyTo(newFilePath, true);
                Console.WriteLine("File copied succesfully");
            }
        }
        
        public static void RunStream(FileInstance fileInstance)
        {
            try
            {   // Open the text file using a stream reader.
                using (StreamReader sr = new StreamReader(fileInstance.FilePath))
                {
                    // Read the stream to a string, and write the string to the console.
                    String line = sr.ReadToEnd();
                    Console.WriteLine(line);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
        }

        public static void RunFile(FileInstance fileInstance)
        {
            try
            {
                string[] sr = System.IO.File.ReadAllLines(fileInstance.FilePath);
                foreach (string line in sr)
                {
                    Console.WriteLine(line);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
        }

        public static void RunFileStream(FileInstance fileInstance)
        {
            if (File.Exists(fileInstance.FilePath))
            {
                File.Delete(fileInstance.FilePath);
            }

            //Create the file.
            using (FileStream fs = File.Create(fileInstance.FilePath))
            {
                AddText(fs, "This is some text");
                AddText(fs, "This is some more text,");
                AddText(fs, "\r\nand this is on a new line");
                AddText(fs, "\r\n\r\nThe following is a subset of characters:\r\n");

                for (int i = 1; i < 120; i++)
                {
                    AddText(fs, Convert.ToChar(i).ToString());

                }
            }

            //Open the stream and read it back.
            using (FileStream fs = File.OpenRead(fileInstance.FilePath))
            {
                byte[] b = new byte[1024];
                UTF8Encoding temp = new UTF8Encoding(true);
                while ((fs.Read(b, 0, b.Length) > 0))
                {
                    Console.WriteLine(temp.GetString(b));
                }
            }
        }

        private static void AddText(FileStream fs, string value)
        {
            byte[] info = new UTF8Encoding(true).GetBytes(value);
            fs.Write(info, 0, info.Length);
        }
    }
}
