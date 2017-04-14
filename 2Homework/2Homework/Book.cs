using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace _2Homework
{
    public class Book : BaseEntity, IComparable
    {
        public int Quontaty { get; set; }
        public int Price { get; set; }

        public List<Author> Autors = new List<Author>();
        public HashSet<string> AuthorsNames = new HashSet<string>();

        public Book(string title, int price, int id, int quontaty, Library library, Author author )
        {
            Quontaty = quontaty;
            Name = title;
            Price = price;
            Id = id.ToString();

            author.AddBook(this.Name);
            library.AddAuthor(author.Name);
            AuthorsNames.Add(author.Name);
        }

        public Book()
        {

        }

        public void AddAuthorName(Author auth)
        {
            auth.AddBook(this.Name);
            AuthorsNames.Add(auth.Name);
        }

        public override string ToString()
        {
            return string.Format("Id:{0} Title:\"{1}\"\tCount:{2}", Id, Name, Quontaty);
        }

        private int GetTitleLength()
        {
            if (string.IsNullOrEmpty(Name)) return 0;
            return Name.Length;
        }

        public int CompareTo(object obj)
        {
            return Price.CompareTo(obj);
        }

        public override XElement WriteToXml()
        {
            var bookRoot = new XElement(GetNodeName(),
                        new XAttribute("Id", Id),
                        new XAttribute("Title", Name),
                        new XAttribute("Price", Price),
                        new XAttribute("Amount", Quontaty));

            foreach (var item in AuthorsNames)
            {
                for (int i = 1; i < AuthorsNames.Count + 1; i++)
                {

                    bookRoot.Add(new XElement($"Author{i}", item));
                }
            }

            return bookRoot;
        }

        public override BaseEntity ReadFromXElement(XElement element, Library library)
        {
            Id = BaseXmlManager.GetAttributeByName(element, "Id");
            Name = BaseXmlManager.GetAttributeByName(element, "Title");

            try
            {
                Quontaty = Int32.Parse(BaseXmlManager.GetAttributeByName(element, "Amount"));
                Price = Int32.Parse(BaseXmlManager.GetAttributeByName(element, "Price"));
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception occured while loading book " + element.Value.ToString());
                Console.WriteLine(e.ToString());
            }

            // In order to save 'Author' correctly we need to use 'library' field of this book.
            // So we need to find Author in the HashSet of authors of the library, or create a new one
            // and save it in this set
            //var authorName = BaseXmlManager.GetAttributeByName(element, "Author");
            //foreach (var name in AuthorsNames)
            //{
            //    element.Add(name);    
            //}

            //return this;

            //var author = from auth in library.AuthorsNames
            //             where auth == authorName
            //             select auth;

            //if (!author.Any())
            //{
            //    this.AuthorsNames = new Author(authorName);
            //    library.Authors.Add(this.Author);
            //}
            //else
            //{
            //    var retrievedAuthor = author.First();
            //    this.Author = retrievedAuthor;
            //    retrievedAuthor.AddBook(this.Name);
            //}

            return this;
        }

        public override Dictionary<string, string> FieldForEditing()
        {
            Dictionary<string, string> field = new Dictionary<string, string>()
            {
                {"Name", Name },
                {"Id", Id.ToString() },
                {"Price",  Price.ToString() },
                {"Amount", Quontaty.ToString() }
            };
            return field;
        }
    }
}
