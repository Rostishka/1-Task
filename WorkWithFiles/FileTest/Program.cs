using System;

namespace FileDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //FileInstance mongo = new FileInstance(@"E:\KPIZOS\mongo.txt");
            //FileManager manager = new FileManager();
            //manager.InformationAboutFile(mongo);
            //manager.CopyFile(mongo, @"E:\KPIZOS\aaa.txt");
            //manager.DeleteFile(mongo);
            //FileInstance aaa = new FileInstance(@"E:\KPIZOS\aaa.txt");
            //manager.TransferFile(aaa, @"E:\Rostishka\Documents\aaa.txt");
            FileInstance aaa = new FileInstance(@"E:\Rostishka\Documents\aaa.txt");
            //FileManager.RunStream(aaa);
            FileManager.RunFile(aaa);
            //FileManager.RunFileStream(aaa);
            Console.ReadLine();
            Console.ReadKey();
        }

    }
}
