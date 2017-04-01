using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Lazar
{
    class Department : BaseEntity, IComparable<Department>, ICountingBooks
    {

        private List<Book> books = new List<Book>();

        public List<Book> GetBooks
        { 
            get { return books; }
        }

        public Department(string name)
        {
            this.Name = name;
        }

        public Department() { }

        public void AddBook(Book book)
        {
            books.Add(book);
        }

        public int CompareTo(Department otherDepartment)
        {
            if (otherDepartment == null) return 1;

            return this.books.Count.CompareTo(otherDepartment.books.Count);
        }

        public override string ToString()
        {
            return "Department: " + this.Name;
        }

        public int BooksCount()
        {
            return books.Count;
        }

        public override XElement WriteToXElement()
        {
            var departmentRoot = new XElement(GetNodeName(),
                                             new XAttribute("id", Id),        
                                             new XAttribute("name", Name)
                                             );
            foreach (var book in this.books)
                departmentRoot.Add(book.WriteToXElement());

            return departmentRoot;
        }

        public sealed override BaseEntity ReadFromXElement(XElement element, Library library)
        {
            this.Id = BaseXmlManager.GetAttributeByName(element, "id");
            this.Name = BaseXmlManager.GetAttributeByName(element, "name");
            foreach (var elem in element.Elements())
            {
                var book = (Book) new Book().ReadFromXElement(elem, library);
                this.AddBook(book);
            }

            return this;
        }

        public override Dictionary<string, string> FieldsForUpdate()
        {
            Dictionary<string, string> fields = new Dictionary<string, string>
            {
                { "name", Name }
            };

            return fields;
        }
    }

}
