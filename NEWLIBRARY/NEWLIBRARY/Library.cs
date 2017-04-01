using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEWLIBRARY
{
    public class Library
    {
        public Library()
        {
            Departments = new List<Dapartment>();
        }

        public string Name { get; set; }
        public string Address { get; set; }

        public List<Dapartment> Departments { get; set; }









        //public List<Author> authors { get; set; }
        //public List<Book> books { get; set; }
        //public int _books;
        //private string _name;

        //public string Name
        //{
        //    get { return _name; }
        //    set { _name = value; }
        //}

        //public Library()
        // {
        //     Departments = new List<Dapartment>();
        // }

        // public void AddBook(Book bookName)
        // {
        //     books.Add(bookName);
        // }
    }
}
