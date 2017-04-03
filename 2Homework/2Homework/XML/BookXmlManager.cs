using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace _2Homework
{
    class BookXmlManager : BaseXmlManager
    {
        private readonly Department _department;

        public BookXmlManager(string _fileName, Department department) : base(_fileName)
        {
            _department = department;
        }

        public void AddBook(Book book)
        {
            var xDoc = XDocument.Load(FileName);
            var bookElement = book.WriteToXml();
            var departmentElement = RetrieveXmlElement(xDoc.Root, _department);
            departmentElement.Add(bookElement);

            xDoc.Save(FileName);
        }

        public override BaseEntity LoadEntity()
        {
            throw new NotImplementedException();
        }
    }
}
