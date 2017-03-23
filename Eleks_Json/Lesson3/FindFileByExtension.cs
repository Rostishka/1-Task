using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqP2
{
    class FindFileByExtension
    {
        public static void Run()
        {
            string startFolder = @"c:\program files\Microsoft Visual Studio 12.0\";

            // Take a snapshot of the file system.
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(startFolder);

            // This method assumes that the application has discovery permissions 
            // for all folders under the specified path.
            IEnumerable<System.IO.FileInfo> fileList = dir.GetFiles("*.*", System.IO.SearchOption.AllDirectories);

            //Create the query
            IEnumerable<System.IO.FileInfo> fileQuery =
                from file in fileList
                where file.Extension == ".txt"
                orderby file.Name
                select file;

            //Execute the query. This might write out a lot of files! 
            foreach (System.IO.FileInfo fi in fileQuery)
            {
                Console.WriteLine(fi.FullName);
            }

            // Create and execute a new query by using the previous     
            // query as a starting point. fileQuery is not  
            // executed again until the call to Last() 
            var newestFile =
                (from file in fileQuery
                 orderby file.CreationTime
                 select new { file.FullName, file.CreationTime })?.Last();

            Console.WriteLine("\r\nThe newest .txt file is {0}. Creation time: {1}",
                newestFile.FullName, newestFile.CreationTime);
        }
    }
}
