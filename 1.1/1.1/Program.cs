using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1._1
{
    class Program
    {
        static void Main(string[] args)
        {
            Library lib1 = new Library()
            {
                name = "Tern"
            };

            Book b1 = new Book();
            Book b3 = new Book();
            Book b2 = new Book();

            lib1.AddBook(new Book() {name = "looo" });
           //lib1.GetBooks();

            Console.ReadLine();
        }
    }
}
