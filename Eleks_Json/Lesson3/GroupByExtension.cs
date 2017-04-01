using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqP2
{
    class GroupByExtension
    {
        public static void Run()
        {
            // Take a snapshot of the file system. 
            string startFolder = @"c:\program files\Microsoft Visual Studio 12.0\Common7";

            // Used in WriteLine to trim output lines. 
            int trimLength = startFolder.Length;

            // Take a snapshot of the file system.
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(startFolder);

            // This method assumes that the application has discovery permissions 
            // for all folders under the specified path.
            IEnumerable<System.IO.FileInfo> fileList = dir.GetFiles("*.*", System.IO.SearchOption.AllDirectories);

            // Create the query. 
            var queryGroupByExt =
                from file in fileList
                group file by file.Extension.ToLower() into fileGroup
                orderby fileGroup.Key
                select fileGroup;

            // Display one group at a time. If the number of  
            // entries is greater than the number of lines 
            // in the console window, then page the output.
            PageOutput(trimLength, queryGroupByExt);
        }

        // This method specifically handles group queries of FileInfo objects with string keys. 
        // It can be modified to work for any long listings of data. Note that explicit typing 
        // must be used in method signatures. The groupbyExtList parameter is a query that produces 
        // groups of FileInfo objects with string keys. 
        private static void PageOutput(int rootLength,
                                        IEnumerable<System.Linq.IGrouping<string, System.IO.FileInfo>> groupByExtList)
        {
            // Flag to break out of paging loop. 
            bool goAgain = true;

            // "3" = 1 line for extension + 1 for "Press any key" + 1 for input cursor.
            int numLines = Console.WindowHeight - 3;

            // Iterate through the outer collection of groups. 
            foreach (var filegroup in groupByExtList)
            {
                // Start a new extension at the top of a page. 
                int currentLine = 0;

                // Output only as many lines of the current group as will fit in the window. 
                do
                {
                    Console.Clear();
                    Console.WriteLine(filegroup.Key == String.Empty ? "[none]" : filegroup.Key);

                    // Get 'numLines' number of items starting at number 'currentLine'. 
                    var resultPage = filegroup.Skip(currentLine).Take(numLines);

                    //Execute the resultPage query 
                    foreach (var f in resultPage)
                    {
                        Console.WriteLine("\t{0}", f.FullName.Substring(rootLength));
                    }

                    // Increment the line counter.
                    currentLine += numLines;

                    // Give the user a chance to escape.
                    Console.WriteLine("Press any key to continue or the 'End' key to break...");
                    ConsoleKey key = Console.ReadKey().Key;
                    if (key == ConsoleKey.End)
                    {
                        goAgain = false;
                        break;
                    }
                } while (currentLine < filegroup.Count());

                if (goAgain == false)
                    break;
            }
        }
    }
}
