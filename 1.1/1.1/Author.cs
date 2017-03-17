using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1._1
{
    class Author : Library, ICountingBooks
    {
        private int _age;

        public int Age
        { 
            get { return _age; }
            set { _age = value; }
        }

        public Author()
            :this("No Name", 0, 0) { }

        public Author(string name)
            : this(name, 0, 0) { }

        public Author(string name, int books,int age) : base()
        {
            this.age = age;
        }

        public override string ToString()
        {
            return "Author's name: " + name + "Has written " + books + "books";
        }
        public void CountBooks()
        {
            throw new NotImplementedException();
        }
    }
}
