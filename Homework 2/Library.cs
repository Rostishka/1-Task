using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Newtonsoft.Json;
using System.IO;

namespace NEWLIBRARY
{   [XmlRoot]
    public class Library : ICountingBooks, IComparable
    {

        [XmlAttribute]
        public string Address { get; set; }

        [XmlElement]
        public string Name { get; set; }

        [XmlAttribute]
        public int Id { get; set; }

        public List<Author> Autors { get; set; }
        public List<Dapartment> Departments { get; set; }

        public Library(string name, string address, int id)
        {
            Id = id;
            Name = name;
            Address = address;
            Autors = new List<Author>();
            Departments = new List<Dapartment>();
        }

        public Library()
        {
            Autors = new List<Author>();
            Departments = new List<Dapartment>();
        }

        public IEnumerable<Dapartment> Where(Func<Dapartment, bool> d)
        {
            foreach(var item in Departments)
            {
                if (d(item))
                {
                    yield return item;
                }
            }
        }

        public int CountBooks()
        {
            int q = 0;
            foreach (var item in Departments)
            {
                q += item.CountBooks();
            }
            return q;
        }

        public int CompareTo(object obj)
        {
            throw new NotImplementedException();
        }

        public string SereilizeJson(string filename)
        {
            try
            {
                using (StreamWriter file = File.CreateText(filename))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    //serializer.ReferenceLoopHandling = ReferenceLoopHandling.Serialize;---->not neccessary if I don't add Authors to books
                    //serializer.PreserveReferencesHandling = PreserveReferencesHandling.Objects;---->not neccessary if I don't add Authors to books
                    serializer.Serialize(file, this);
                }
                return "Serializind data into JSON was successful";
            }
            catch (Exception)
            {
                return "Can't Serialize yor data into JSON";
            }
        }
    }
}
