using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1._1
{
    class Book : Author, IComparable
    {
        private int _pages;

        public int Pages
        {
            get { return _pages; }
            set { _pages = value; }
        }

        public Book()
            : this("No Name", 0, 0) { }

        public Book(string name)
            : this(name, 0, 0) { }

        public Book(string name, int pages, int age) : base(name, age)
        {
            Pages = pages;
        }

        public override string ToString()
        {
            return Name + "book consists of " + Pages + "pages";
        }

        public int CompareTo(object obj)
        {
            if (obj != null)
            { 
            Book otherBook = obj as Book;
            if (otherBook != null)
            {
                return _pages.CompareTo(otherBook._pages);
            }
            else
                throw new ArgumentException("Object doesn't have any pages");
            }
             else return 0;
        }
        
        public static void ShowComparapble(Book b1, Book b2)
        {
            Console.WriteLine("Book {0} has more pages than book {1} : {2}", b1.Name, b2.Name, b1.CompareTo(b2));
        } 
    }
}
