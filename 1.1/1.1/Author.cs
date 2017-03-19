using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1._1
{
    class Author : Department, IComparable, ICountingBooks
    {
        private int _age;

        public int Age
        { 
            get { return _age; }
            set { _age = value; }
        }

        public Author()
         :this("No Name", 0) { }

        public Author(string name)
          : this(name, 0) { }
        
        public Author(string name, int age) : base(name)
        {
            Age = age;
        }

        public override string ToString()
        {
            return "Author's name: " + Name;
        }
        public new void CountBooks()
        {
            _books = booooks.Count();
        }

        public override void ShowNumOfBooks()
        {
            Console.WriteLine("Author has wtritten {0}", _books);
        }
        public int CompareTo(object obj)
        {
            if (obj != null)
            {
                Author otherAuthor = obj as Author;
                if (otherAuthor != null)
                {
                    return _books.CompareTo(otherAuthor._books);
                }
                else
                    throw new ArgumentException("Object didn't write any books");
            }
            else return 0;
        }

        public static void ShowComparapble(Author a1, Author a2)
        {
            Console.WriteLine("Author {0} has wtitten more books than Author {1} : {2}", a1.Name, a2.Name, a1.CompareTo(a2));
        }
    }
}
