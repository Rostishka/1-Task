using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
namespace _1._1
{
    public class Department : Library, IComparable
    {
        private List<Author> authors = new List<Author>();

        public void AddBook(Book bookName)
        {
            booooks.Add(bookName);
        }

        public void AddAuthor(Author author)
        {
            authors.Add(author);
        }

        public override string ToString()
        {
            return Name + " Department contains " + _books + " books";
        }

        public Department()
            : this("") { }

        public Department(string name) : base(name)
        {
            Name = name;
        }

        public void CountBooks()
        {
            foreach (Author a in authors)
            {
                _books += a._books;
            }
        }

        public int CompareTo(object obj)
        {
            if (obj != null)
            {
                Department otherDepartment = obj as Department;
                if (otherDepartment != null)
                {
                    return _books.CompareTo(otherDepartment._books);
                }
                else
                    throw new ArgumentException("Object doesn't contain any books");
            }
            else return 0;
        }
        public static void ShowComparapble(Department d1, Department d2)
        {
            Console.WriteLine("Department {0} contains more books than Department {1} : {2}", d1.Name, d2.Name, d1.CompareTo(d2));
        }
    }
}
