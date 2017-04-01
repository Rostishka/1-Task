using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Lazar
{
    class Author : IComparable<Author>, ICountingBooks
    {

        public string Name { get; set; }
        private HashSet<string> books = new HashSet<string>();

        public Author(string name)
        {
            this.Name = name;
        }

        public Author() { }

        public void AddBook(string bookName)
        {
            books.Add(bookName);
        }

        public int CompareTo(Author otherAuthor)
        {
            if (otherAuthor == null) return 1; 
                
            return this.books.Count.CompareTo(otherAuthor.books.Count);
        
        }

        public override string ToString()
        {
            return "Author: " + this.Name;
        }

        public int BooksCount()
        {
            return books.Count;
        }

    }

}
