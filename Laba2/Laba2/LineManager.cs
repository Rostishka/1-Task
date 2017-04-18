using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laba2
{
    class LineManager
    {
        public string _filePath;
        public int _numOfLines = 0;
        public LineManager(string filePath)
        {
            _filePath = filePath;
        }
        public int CountLines()
        {
            using (var reader = File.OpenText(_filePath))
            {
                while (reader.ReadLine() != null)
                {
                    _numOfLines++;
                }
            }
            return _numOfLines;
        }

        public void DeleteLine(int numOfLines, string fileName)
        {
            string[] fileLines = File.ReadAllLines(fileName);
            fileLines[numOfLines / 2] = String.Empty;
            File.WriteAllLines(fileName, fileLines);
        }
    }
}
