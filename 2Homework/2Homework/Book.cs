using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace _2Homework
{
    public class Book : BaseEntity
    {
        public string Title { get; set; }
        public int Id { get; set; }
        public int Quontaty { get; set; }
        public decimal Price { get; set; }

        public List<Author> Autors = new List<Author>();
        public HashSet<string> AuthorsNames = new HashSet<string>();

        public Book(string title, Decimal price, int id, int quontaty, Library library, Author author )
        {
            Quontaty = quontaty;
            Title = title;
            Price = price;
            Id = id;

            author.AddBook(this.Title);
            library.AddAuthor(author.Name); 
        }

        public void AddAuthorName(Author auth)
        {
            auth.AddBook(this.Title);
            AuthorsNames.Add(auth.Name);
        }

        public override string ToString()
        {
            return string.Format("Id:{0} Title:\"{1}\"\tCount:{2}", Id, Title, Quontaty);
        }

        private int GetTitleLength()
        {
            if (string.IsNullOrEmpty(Title)) return 0;
            return Title.Length;
        }

        public int CompareTo(object obj)
        {
            return Price.CompareTo(obj);
        }

        public override XElement WriteToXml()
        {
            return new XElement(GetNodeName(),
                        new XAttribute("id", Id),
                        new XAttribute("title", Title),
                        new XText(Title),
                        new XAttribute("price", Price),
                        new XAttribute("amount", Quontaty));
        }
    }
}
