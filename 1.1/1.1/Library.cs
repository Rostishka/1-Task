using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace _1._1
{
     class Library : ICountingBooks
    {
        public List<Depatrment> departments = new List<Depatrment>();
        public List<Book> booooks = new List<Book>();
        private string _name;
        private int _books;

        public string Name
        {
            get { return _name; }
        }

        public Library()
            : this("") { }

        public Library(string Name)
            : this(Name) { }

        public void AddBook(Book bookName)
        {
            booooks.Add(bookName);
        }
        public void AddDepartment(Depatrment departmentName)
        {
            departments.Add(departmentName);
        }

        public void GetBooks()
        {
            booooks.Count();
        }
        public override string ToString()
        {
            return "Library's name: " + _name + "contains " + _books + "books";
        }

        public void CountBooks()
        {
            Console.WriteLine("Number of books in the whole library is: {0}", books);
        }
    }
}
