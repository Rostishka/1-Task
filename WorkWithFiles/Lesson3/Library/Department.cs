using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqP2.Library
{
    public class Department
    {
        public Department()
        {
            Books = new List<Book>();
        }
        public string Name { get; set; }
        public List<Book> Books { get; private set; }
        public IEnumerable<Book> Where(Func<Book, bool> f)
        {
            foreach(var item in Books)
            {
                if(f(item))
                {
                    yield return item;
                }
            }
        }
    }
}
