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
            Author a = new Author("Rostyk", 17);
            Author a2 = new Author("bodya", 14);
            Author a3 = new Author("Andriy", 37);
            Author a4 = new Author("Rock", 110);
            Department dep2 = new Department("IT");
            Department dep1 = new Department("Foreign");
            Book b1 = new Book("War", 124, 12);
            Book b2 = new Book("ss", 2435, 22);
            Book b3 = new Book("War", 1240, 102);
            Book b4 = new Book("KOSAks", 35, 122);
            Book b5 = new Book("World War I", 1124, 2);
            Book b6 = new Book("Zoom", 5, 22);
            Book b7 = new Book("Plan", 40, 12);
            Book b8 = new Book("lel", 351, 92);
            Book b9 = new Book("War OO", 24, 612);
            Book b10 = new Book("Mandarin", 725, 82);
            Book b12 = new Book("Okayanna", 124, 1);
            Book b13 = new Book("Something", 350, 2);

            Library lib1 = new Library("Ternopil");


            a.AddBook(b1);
            a.AddBook(b2);
            a2.AddBook(b3);
            a2.AddBook(b4);

            a3.AddBook(b5);
            a3.AddBook(b6);
            a3.AddBook(b7);
            a3.AddBook(b8);
            a4.AddBook(b9);
            a4.AddBook(b10);
            a4.AddBook(b10);
            a4.AddBook(b12);
            a4.AddBook(b13);

            a.CountBooks();
            a.ShowNumOfBooks();
            a2.CountBooks();
            a2.ShowNumOfBooks();
            a3.CountBooks();
            a3.ShowNumOfBooks();
            a4.CountBooks();
            a4.ShowNumOfBooks();

            Console.WriteLine(b3.CompareTo(b2));//2-nd book has more pages so returns -1
            Book.ShowComparapble(b1, b3);
            Book.ShowComparapble(b12, b13);
            Book.ShowComparapble(b3, b1);
            Book.ShowComparapble(b5, b5);

            Author.ShowComparapble(a4, a3);
            Author.ShowComparapble(a3, a);
            Author.ShowComparapble(a2, a2);
            Author.ShowComparapble(a2, a4);

            dep1.AddAuthor(a);
            dep1.AddAuthor(a2);
            dep1.CountBooks();

            dep2.AddAuthor(a3);
            dep2.AddAuthor(a4);
            dep2.CountBooks();

            Console.WriteLine(dep1.CompareTo(dep2));


            Department.ShowComparapble(dep1, dep2);
            Department.ShowComparapble(dep2, dep1);
            Department.ShowComparapble(dep1, dep1);


            lib1.AddDepartment(dep1);
            lib1.AddDepartment(dep2); 
            lib1.CountDepartments();
            Console.WriteLine(lib1.ToString());
            lib1.CountBooks();
            lib1.ShowNumOfBooks();

            Console.ReadLine();
        }
    }
}
