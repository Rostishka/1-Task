using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEWLIBRARY
{
    class Program
    {
        static void Main(string[] args)
        {
            var lib1 = new Library()
            {
                Name = "Tern",
                Address = "Zhivova, 37"
            };

            var depart1 = new Dapartment()
            {
                Name = "IT",
                Id = 12,
            };
           

            var book1 = new Book()
            {
                Title = "War and Peace",
                Id = 1,
                Price = 30
            };

            depart1.Books.Add(book1);
            //book1.Autors.
        }
    }
}
