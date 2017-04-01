using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace LinqP2.Library
{
    
    public class Book
    {
        public Book()
        {
            Autors = new List<string>();
        }
        public int Id { get; set; }
        public List<string> Autors { get; private set; }

        [XmlElement(ElementName = "BookTitle")]
        public string Title { get; set; }
        
        public int Count { get; set; }

        [Format("0.00 UAH")]
        public decimal Price { get; set; }
        
        public override string ToString()
        {
            return string.Format("Id:{0} Title:\"{1}\"\tCount:{2}",Id,Title,Count);
        }
        
        private int GetTitleLength()
        {
            if(string.IsNullOrEmpty(Title)) return 0;
            return Title.Length;
        }
    }
}
