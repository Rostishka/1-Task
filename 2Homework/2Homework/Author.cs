using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace _2Homework
{
    public class Author
    { 
        public string Name { get; set; }
        public int Id { get; set; }
        public int Age { get; set; }

        private HashSet<string> BooksTitles = new HashSet<string>();

        public Author(string name, int age, int id/*, string bookTitle*/)
        {
            Name = name;
            Id = id;
            Age = age;
            //BooksTitles.Add(bookTitle);
        }
        
        public void AddBook(string bookTitle)
        {
            BooksTitles.Add(bookTitle);
        }

        public int CountBooks()
        {
            int q = 0;
            foreach (var item in BooksTitles)
            {
                q ++;
            }
            return q;
        }

        public int CompareTo(object obj)
        {
            if (obj != null)
            {
                Author otherAuthor = obj as Author;
                if (otherAuthor != null)
                {
                    return CountBooks().CompareTo(otherAuthor.CountBooks());
                }
                else
                    throw new ArgumentException("Object doesn't contain any books");
            }
            else return 0;
        }
        public static void ShowComparapble(Author a1, Author a2)
        {
            Console.WriteLine("Department {0} contains more books than Department {1} : {2}", a1.Name, a2.Name, a1.CompareTo(a2));
        }
    }
}
