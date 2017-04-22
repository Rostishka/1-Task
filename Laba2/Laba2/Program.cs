using System;

namespace Laba2
{
    class Program
    {
        static void Main(string[] args)
        {
            LineManager manager = new LineManager(@"D:\qw\laba.txt");
            manager.DeleteLine(manager.CountLines());

            LineManager manager2 = new LineManager(@"D:\qw\Text1.txt", @"D:\qw\Text2.txt");
            manager2.GetString(8, 8, 1);
            manager2.DeleteText(8, 8, 1);
            manager2.InsertText(16, 2);

            Console.ReadKey();
        }
    }
}
