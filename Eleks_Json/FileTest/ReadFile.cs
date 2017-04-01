using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileDemo
{
    public class ReadFile
    {
        private const string _sourceFile = "Demo.txt";
        private const string _sourceFile2 = "Demo2.txt";
        public static void RunStream()
        {
            try
            {   // Open the text file using a stream reader.
                using (StreamReader sr = new StreamReader(_sourceFile))
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
        public static void RunFile()
        {
            try
            {
                string[] sr = File.ReadAllLines(_sourceFile);
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
        public static void RunFileStream()
        {
            if (File.Exists(_sourceFile2))
            {
                File.Delete(_sourceFile2);
            }

            //Create the file.
            using (FileStream fs = File.Create(_sourceFile2))
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
            using (FileStream fs = File.OpenRead(_sourceFile2))
            {
                byte[] b = new byte[1024];
                UTF8Encoding temp = new UTF8Encoding(true);
                while (fs.Read(b, 0, b.Length) > 0)
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
