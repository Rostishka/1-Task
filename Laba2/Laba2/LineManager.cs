using System;
using System.IO;
namespace Laba2
{
    class LineManager
    {
        public string _filePath1;
        public string _filePath2;
        public int _numOfLines = 0;
        public string _importantText;

        public LineManager(string filePath1)
        {
            _filePath1 = filePath1;
        }

        public LineManager(string filePath1, string filePath2)
        {
            _filePath1 = filePath1;
            _filePath2 = filePath2;
        }

        public int CountLines()
        {
            using (var reader = File.OpenText(_filePath1))
            {
                while (reader.ReadLine() != null)
                {
                    _numOfLines++;
                }
            }
            return _numOfLines;
        }

        public void DeleteLine(int numOfLines)
        {
            string[] fileLines = File.ReadAllLines(_filePath1);
            fileLines[numOfLines / 2] = String.Empty;
            File.WriteAllLines(_filePath1, fileLines);
        }

        public void GetString(int index, int length, int lineNumber)
        {
            string[] fileLines = File.ReadAllLines(_filePath1);

            _importantText = fileLines[lineNumber].Substring(index, length);
        }

        public void DeleteText(int index, int length, int lineNumber)
        {
            string[] fileLines = File.ReadAllLines(_filePath1);

            fileLines[lineNumber] = fileLines[lineNumber].Remove(index, length);

            File.WriteAllLines(_filePath1, fileLines);
        }

        public void InsertText(int index, int lineNumber)
        {
            string[] fileLines = File.ReadAllLines(_filePath2);

            fileLines[lineNumber] = fileLines[lineNumber].Insert(index, _importantText);

            File.WriteAllLines(_filePath2, fileLines);
        }
    }   
}
