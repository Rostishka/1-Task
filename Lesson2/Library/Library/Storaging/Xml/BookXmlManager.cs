using System;
using System.Xml.Linq;

namespace Lazar
{
    class BookXmlManager : BaseXmlManager
    {
        private readonly Department _department;


        public BookXmlManager(string fileName, Department department) : base(fileName)
        {
            _department = department;
        }


        public void AddBook(Book book)
        {
            var doc = XDocument.Load(FileName);
            var bookElement = book.WriteToXElement();
            var departmentElement = RetrieveXElement(_department, doc.Root);
            departmentElement.Add(bookElement);

            doc.Save(FileName);
        }

        public override BaseEntity LoadEntity()
        {
            throw new NotImplementedException();
        }
    }
}
        