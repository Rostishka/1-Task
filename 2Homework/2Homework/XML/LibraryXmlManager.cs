using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace _2Homework
{
    class LibraryXmlManager : BaseXmlManager
    {
        public LibraryXmlManager(string _fileName) : base(_fileName)
        {
        }

        public override BaseEntity LoadEntity()
        {
            XDocument xDoc = XDocument.Load(FileName);
            return new Library(xDoc.Root);
        }
    }
}
