using System;
using System.Xml.Linq;
using System.Linq;
using System.Collections.Generic;

namespace Lazar
{
    class Book : BaseEntity, IComparable<Book>
    {
        public Author Author { get; set; }
        public int Year { get; set; }
        public int Pages { get; set; }

        public Book(Library library, string name, Author author, int year, int pages)
        {
            this.Name = name;
            this.Author = author;
            this.Year = year;
            this.Pages = pages;

            author.AddBook(this.Name);
            library.Authors.Add(author);
        }

        public Book() { }

        public int CompareTo(Book otherBook)
        {
            if (otherBook == null) return 1;

            return this.Pages.CompareTo(otherBook.Pages);
        }

        public override string ToString()
        {
            return "Book: " + this.Name;
        }

        public override XElement WriteToXElement()
        {
            return new XElement(GetNodeName(),
                               new XAttribute("id", Id),
                               new XAttribute("name", Name),
                               new XAttribute("author", Author.Name),
                               new XAttribute("year", Year),
                               new XAttribute("pages", Pages)                               
                               );
        }

        public override BaseEntity ReadFromXElement(XElement element, Library library)
        {
            this.Id = BaseXmlManager.GetAttributeByName(element, "id");
            this.Name = BaseXmlManager.GetAttributeByName(element, "name");

            try
            {
                this.Year = Int32.Parse(BaseXmlManager.GetAttributeByName(element, "year"));
                this.Pages = Int32.Parse(BaseXmlManager.GetAttributeByName(element, "pages"));
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception occured while loading book " + element.Value.ToString());
                Console.WriteLine(e.ToString());
            }

            // In order to save 'Author' correctly we need to use 'library' field of this book.
            // So we need to find Author in the HashSet of authors of the library, or create a new one
            // and save it in this set
            var authorName = BaseXmlManager.GetAttributeByName(element, "author");

            var author = from auth in library.Authors
                         where auth.Name == authorName
                         select auth;
            
            if (!author.Any())
            {
                this.Author = new Author(authorName);
                library.Authors.Add(this.Author);
            }
            else
            {
                var retrievedAuthor = author.First();
                this.Author = retrievedAuthor;
                retrievedAuthor.AddBook(this.Name);
            }         

            return this;
        }

        public override Dictionary<string, string> FieldsForUpdate()
        {
            Dictionary<string, string> fields = new Dictionary<string, string>
            {
                { "name", Name },
                { "author", Author.Name },
                { "year", Year.ToString() },
                { "pages", Pages.ToString() }
            };

            return fields;
        }
    }

}
