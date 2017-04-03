using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2Homework
{
    class Program
    {
        static void Main(string[] args)
        {
            var lib1 = new Library("Ternopilska Library", "Zhivova 34", 1);
            //=====================================================================================
            var depart1 = new Department("IT", 11);
            var depart2 = new Department("FatherLand Literature", 12);
            var depart3 = new Department("Foreign  Literature", 13);
            //=====================================================================================
            Author a1 = new Author("Эндрю Троелсен", 49, 1);
            Author a2 = new Author("Джозеф Албахари", 39, 2);
            Author a3 = new Author("Тарас Шевченко", 37, 3);
            Author a4 = new Author("Iван Франко", 110, 4);
            Author a5 = new Author("Джек Лоднон", 1886, 5);
            //=====================================================================================
            var book = new Book("Ward and Peace", 122, 0, 2, lib1, a1);
            var book1 = new Book("Язык программирования C# 5.0 и платформа .NET 4.5", 1311, 1, 3, lib1, a1);
            Book b2 = new Book("Язык программирования C# 6.0 и платформа .NET 4.6", 1440, 2, 7, lib1, a1);
            Book b3 = new Book("C# 6.0. Справочник. Полное описание языка", 1040, 3, 12, lib1, a2);
            Book b4 = new Book("C# 5.0. Справочник. Полное описание языка", 1008, 4, 8, lib1, a2);
            Book b5 = new Book("Катерина", 233, 5, 11, lib1, a3);
            Book b6 = new Book("Гайдамаки", 430, 6, 12, lib1, a3);
            Book b7 = new Book("Утоплена", 201, 7, 56, lib1, a3);
            Book b8 = new Book("Тарасова ніч", 78, 9, 44, lib1, a3);
            Book b9 = new Book("Кобзар", 420, 9, 7, lib1, a3);
            Book b10 = new Book("Істар", 75, 10, 65, lib1, a3);
            Book b11 = new Book("Смерть Каїна", 24, 11, 123, lib1, a4);
            Book b12 = new Book("Вічний Револціонер", 1, 12, 13, lib1, a4);
            Book b13 = new Book("Лис Микита", 50, 13, 2, lib1, a4);
            Book b14 = new Book("Мойсей", 540, 14, 1, lib1, a4);
            Book b15 = new Book("Страшний Суд", 200, 15, 0, lib1, a4);
            Book b16 = new Book("Жага до життя", 75, 16, 22, lib1, a5);
            Book b17 = new Book("Зов предков", 243, 17, 54, lib1, a5);
            Book b18 = new Book("Біле ікло", 603, 18, 98, lib1, a5);
            Book b19 = new Book("Залізна пята", 530, 19, 23, lib1, a5);
            Book b20 = new Book("Байки Езопа", 40, 20, 11, lib1, a5);
            Book b21 = new Book("Лев та миша", 21, 21, 3, lib1, a5);

            //Can't add author to book because of cicle
            //b10.Autors.Add(a3);
            //=====================================================================================
         
            //book1.Autors.Add(a1);
            //=====================================================================================
            depart1.Books.AddRange(new[] { book, book1, b2, b3, b4 });

            depart2.Books.AddRange(new[] { b5, b6, b7, b8, b9, b10, b11, b12, b13, b14, b15 });

            depart3.Books.AddRange(new[] { b16, b17, b18, b20, b21 });
            //=====================================================================================
            Console.WriteLine(a1.CountBooks());
            Console.WriteLine(a2.CountBooks());
            Console.WriteLine(depart1.CountBooks());

            Console.WriteLine(a3.CountBooks());
            Console.WriteLine(a4.CountBooks());
            Console.WriteLine(depart2.CountBooks());

            Console.WriteLine(a5.CountBooks());
            Console.WriteLine(depart3.CountBooks());
            //=====================================================================================
            lib1.AddDepartment(depart1);
            lib1.AddDepartment(depart2);
            lib1.AddDepartment(depart3);

            Console.WriteLine(lib1.CountBooks());

            lib1.AddAuthor(b10.Name);
            //=====================================================================================
            Department.ShowComparapble(depart1, depart2);
            Console.WriteLine(book1.CompareTo(b2.Price));
            Author.ShowComparapble(a1, a2);
            //=====================================================================================
            Console.WriteLine(new string('-', 100));
            b20.AddAuthorName(a3);
            Console.WriteLine(a3.CountBooks());

            
            XmlManager manager = new XmlManager();
          
            manager.DeleteEntity(depart1);
            manager.SaveLibrary(lib1);

            Console.ReadKey();
        }
    }
}
