using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace NEWLIBRARY
{
    public class Book {
    //{
    //    private int _pages;
    //    public Book() { }

    //    public Book(string name, int id, int pages, List<Author> authors)
    //    {
    //        _name = name;
    //        _id = id;
    //        _pages = pages;

    //        authors = new List<Author>();
    //    }

        public Book()
        {
            Autors = new List<Author>();
        }
        public int Id { get; set; }
        public List<Author> Autors { get; private set; }

        [XmlElement(ElementName = "BookTitle")]
        public string Title { get; set; }

        public int Count { get; set; }

        //[Format("0.00 UAH")]
        public decimal Price { get; set; }

        public override string ToString()
        {
            return string.Format("Id:{0} Title:\"{1}\"\tCount:{2}", Id, Title, Count);
        }

        private int GetTitleLength()
        {
            if (string.IsNullOrEmpty(Title)) return 0;
            return Title.Length;
        }
    }
}
