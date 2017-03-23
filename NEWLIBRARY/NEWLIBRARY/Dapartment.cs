using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEWLIBRARY
{
    public class Dapartment
    {
        //private int _age;
        //public Dapartment() { }

        //public Dapartment(string name, int id)
        //{
        //    _name = name;
        //    _id = id;
        //}
        public Dapartment()
        {
            Books = new List<Book>();
        }

        public string Name { get; set; }
        public int Id { get; set; }
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
