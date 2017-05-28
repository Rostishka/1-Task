using System;
using System.IO;

namespace FileDemo
{
   public class FileInstance
    {
        private String _filePath;
        private FileInfo _fileInfo;

        public String FilePath
        {
            get => _filePath;
            set => _filePath = value;
        }
        
        public FileInfo FileInfo
        {
            get => _fileInfo;
            set => _fileInfo = value;
        }

        public FileInstance(String filePath)
        {
            FilePath = filePath;
            _fileInfo = new FileInfo(_filePath);
        }
    }
}
