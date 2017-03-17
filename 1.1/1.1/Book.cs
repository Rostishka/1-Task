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
            :this("No Name", 0) {
            books++;
        }

        public Book(string name)
            : this(name, 0) {
            books++;
        }

        public Book(string name, int pages)
        {
            this.pages = pages;
            this.name = name;
            books++;
        }

        public override string ToString()
        {
            return name + "book consists of " + pages + "pages";
        }

        public int CompareTo(object obj)
        {
            return pages.CompareTo(obj);
        }
    }
}
