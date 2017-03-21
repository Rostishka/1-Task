using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace _1._1
{
    class Program
    {
        static void Main(string[] args)
        {
            Author a1 = new Author("Эндрю Троелсен", 49);
            Author a2 = new Author("Джозеф Албахари", 39);
            Author a3 = new Author("Тарас Шевченко", 37);
            Author a4 = new Author("Iван Франко", 110);
            Author a5 = new Author("Джек Лоднон", 1886);
            Author a6 = new Author("Езоп", 110);
            Department dep1 = new Department("IT Literature");
            Department dep2 = new Department("Fatherland Literature");
            Department dep3 = new Department("Foreign Literature");
            Book b1 = new Book("Язык программирования C# 5.0 и платформа .NET 4.5", 1311, 2013);
            Book b2 = new Book("Язык программирования C# 6.0 и платформа .NET 4.6", 1440, 2016);
            Book b3 = new Book("C# 6.0. Справочник. Полное описание языка", 1040, 2016);
            Book b4 = new Book("C# 5.0. Справочник. Полное описание языка", 1008, 2013);
            Book b5 = new Book("Катерина", 233, 1837);
            Book b6 = new Book("Гайдамаки", 430, 1839);
            Book b7 = new Book("Утоплена", 201, 1841);
            Book b8 = new Book("Тарасова ніч", 78, 1838);
            Book b9 = new Book("Кобзар", 420, 1840);
            Book b10 = new Book("Істар", 75, 1899);
            Book b11 = new Book("Смерть Каїна", 24, 1881);
            Book b12 = new Book("Вічний Револціонер", 1, 1888);
            Book b13 = new Book("Лис Микита", 50, 1884);
            Book b14 = new Book("Мойсей", 540, 1905);
            Book b15 = new Book("Страшний Суд", 200, 1906);
            Book b16 = new Book("Жага до життя", 75, 1899);
            Book b17 = new Book("Зов предков", 243, 1903);
            Book b18 = new Book("Біле ікло", 603, 1906);
            Book b19 = new Book("Залізна пята", 530, 1904);
            Book b20 = new Book("Байки Езопа", 40, -1002);
            Book b21 = new Book("Лев та миша", 21, -1006);

            Library lib1 = new Library("Ternopil");

            a1.AddBook(b1);
            a1.AddBook(b2);
            a2.AddBook(b3);
            a2.AddBook(b4);
  
            a3.AddBook(b5);
            a3.AddBook(b6);
            a3.AddBook(b7);
            a3.AddBook(b8);
            a3.AddBook(b9);
            a4.AddBook(b10);
            a4.AddBook(b11);
            a4.AddBook(b12);
            a4.AddBook(b13);
            a4.AddBook(b14);
            a4.AddBook(b15);
     
            a5.AddBook(b16);
            a5.AddBook(b17);
            a5.AddBook(b18);
            a5.AddBook(b19);
            a6.AddBook(b20);
            a6.AddBook(b21);

            a1.CountBooks();
            a2.CountBooks();
            a3.CountBooks();
            a4.CountBooks();
            a5.CountBooks();
            a6.CountBooks();

            Console.WriteLine(a1.ToString());
            Console.WriteLine(a2.ToString());
            Console.WriteLine(a3.ToString());
            Console.WriteLine(a4.ToString());
            Console.WriteLine(a5.ToString());
            Console.WriteLine(a6.ToString());
            
            Book.ShowComparapble(b1, b3);
            Book.ShowComparapble(b12, b13);
            Book.ShowComparapble(b3, b1);
            Book.ShowComparapble(b5, b5);

            Author.ShowComparapble(a4, a3);
            Author.ShowComparapble(a3, a1);
            Author.ShowComparapble(a5, a2);
            Author.ShowComparapble(a2, a6);

            dep1.AddAuthor(a1);
            dep1.AddAuthor(a2);
            dep1.CountBooks();

            dep2.AddAuthor(a3);
            dep2.AddAuthor(a4);
            dep2.CountBooks();

            dep3.AddAuthor(a5);
            dep3.AddAuthor(a6);
            dep3.CountBooks();

            Department.ShowComparapble(dep1, dep2);
            Department.ShowComparapble(dep2, dep1);
            Department.ShowComparapble(dep1, dep3);

            Console.WriteLine(dep1.ToString());
            Console.WriteLine(dep2.ToString());
            Console.WriteLine(dep3.ToString());

            lib1.AddDepartment(dep1);
            lib1.AddDepartment(dep2);
            lib1.AddDepartment(dep3);
            lib1.CountDepartments();
            lib1.CountBooks();
            Console.WriteLine(lib1.ToString());

            string xmlString = b10.XmlSerialize();

            Console.WriteLine(xmlString);
            Console.WriteLine();
            string xmlString2 = a2.XmlSerialize();

            Console.WriteLine(xmlString2);

            Console.WriteLine();

            string xmlString3 = dep2.XmlSerialize();

            Console.WriteLine(xmlString3);

            Console.ReadLine();
        }
    }
}
