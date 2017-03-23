using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEWLIBRARY
{
    public class Author
    {
        //private int _age;
        //public Author() { }

        //public Author(string name, int id, int age)
        //{
        //    _name = name;
        //    _id = id;
        //    _age = age;
        //}

          public Author()
        {
            Books = new List<Book>();
        }

        public string Name { get; set; }
        public int Age { get; set; }
        public List<Book> Books { get; private set; }
        public IEnumerable<Book> Where(Func<Book, bool> f)
        {
            foreach (var item in Books)
            {
                if (f(item))
                {
                    yield return item;
                }
            }
        }

    }
}
