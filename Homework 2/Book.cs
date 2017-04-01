using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace NEWLIBRARY
{ 
    public class Book : IComparable
    {

        [XmlElement]
        public string Title { get; set; }

        [XmlAttribute]
        public int Id { get; set; }

        [XmlAttribute]
        public int Quontaty { get; set; }

        [XmlElement]
        public decimal Price { get; set; }

        public List<Author> Autors { get; set; }

        public Book(string title, Decimal price, int id, int quontaty)
        {
            Quontaty = quontaty;
            Title = title;
            Price = price;
            Id = id;
            Autors = new List<Author>();
        }

        public Book()
        {
            Autors = new List<Author>();
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
    }
}
