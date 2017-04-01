using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Lazar
{
    partial class Program {

        private const string FileName = "library.xml";

        private static void Main(string[] args)
        {

            var library = new Library("Центральна бібліотека Тернополя", "м. Тернопіль, вул.Бандери 23");

            var dep1 = new Department("Українська література");
            var dep2 = new Department("IT");
            var dep3 = new Department("Економіка");

            library.AddDepartment(dep1);
            library.AddDepartment(dep2);
            library.AddDepartment(dep3);

            var auth1 = new Author("Франко Іван");
            var auth2 = new Author("Шевченко Тарас");
            var auth3 = new Author("Карл Маркс");
            var auth4 = new Author("Брюс Еккель");
            var auth5 = new Author("Джошуа Блох");

            dep1.AddBook(new Book(library, "Галицькі образки", auth1, 1885, 123));
            dep1.AddBook(new Book(library, "Галицькі образки", auth1, 1885, 123));
            dep1.AddBook(new Book(library, "Галицькі образки", auth1, 1885, 123));
            dep1.AddBook(new Book(library, "Сім казок", auth1, 1900, 201));
            dep1.AddBook(new Book(library, "Сім казок", auth1, 1900, 201));
            dep1.AddBook(new Book(library, "Кобзар", auth2, 1876, 546));
            dep1.AddBook(new Book(library, "Кобзар", auth2, 1876, 546));
            dep1.AddBook(new Book(library, "Кобзар", auth2, 1876, 546));
            dep1.AddBook(new Book(library, "Кобзар", auth2, 1876, 546));
            dep1.AddBook(new Book(library, "Кобзар", auth2, 1876, 546));
            //-------------------------------------------------
            dep2.AddBook(new Book(library, "Философия Java", auth4, 2000, 780));
            dep2.AddBook(new Book(library, "Философия Java", auth4, 2000, 780));
            dep2.AddBook(new Book(library, "Философия Java", auth4, 2000, 780));
            dep2.AddBook(new Book(library, "Ефективне програмування на Java", auth5, 2004, 546));
            dep2.AddBook(new Book(library, "Ефективне програмування на Java", auth5, 2004, 546));
            //-------------------------------------------------
            dep3.AddBook(new Book(library, "Капітал", auth3, 1876, 546));
            dep3.AddBook(new Book(library, "Капітал", auth3, 1876, 546));

            var someBook1 = new Book(library, "Капітал", auth3, 1876, 546);
            dep3.AddBook(someBook1);

            var someBook2 = new Book(library, "Капітал", auth3, 1876, 546);
            dep3.AddBook(someBook2);

            // hometask 1
            ShowResults(library);

            // hometask 2
            // save and load library 
            XmlStoraging(library);
            // update library, department, book
            dep3.Name = "Econimics";
            var xmlManager = new LibraryXmlManager(FileName);
            xmlManager.UpdateEntity(dep3);

            someBook1.Pages = 14;
            xmlManager.UpdateEntity(someBook1);
            // delete 
            xmlManager.DeleteEntity(someBook2);
            xmlManager.DeleteEntity(dep1);

            // add depatment, book
            var depXmlManager = new DepartmentXmlManager(FileName, library);
            depXmlManager.AddDepartment(dep1);
            var bookXmlManager = new BookXmlManager(FileName, dep1);
            bookXmlManager.AddBook(new Book(library, "Автобіографія", auth2, 1890, 420));

            Console.ReadKey();
        }

        private static void XmlStoraging(Library library)
        {
            var file = new FileStream(FileName, FileMode.Create);
            file.Dispose();

            var xmlManager = new LibraryXmlManager(FileName);

            xmlManager.SaveEntity(library);

            var lib = xmlManager.LoadEntity();
            ShowResults((Library) lib);

        }

        public static void ShowResults(Library library)
        {
            //Показати автора, у якого найбільше випущених книжок. 
            var authors = library.Authors.ToList();
            authors.Sort();
            Console.WriteLine("Показати автора, у якого найбільше випущених книжок - " + authors.Last());

            //Показати відділ, у якого найбільше книжок.
            var departments = library.Departments;
            departments.Sort();
            Console.WriteLine("Показати відділ, у якого найбільше книжок - " + departments.Last());

            //Показати книжку, яка має найменшу кількість сторінок у всій бібліотеці.
            var booksQuery = from department in library.Departments
                             from book in department.GetBooks
                             orderby book.Pages
                             select new { book };

            Console.WriteLine("Показати книжку, яка має найменшу кількість сторінок у всій бібліотеці - " + booksQuery.First());
        }
    }
}
        