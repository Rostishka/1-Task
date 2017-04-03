using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace _2Homework
{
    [XmlRoot]
    public class Library : BaseEntity, ICountingBooks
    {
        public string Address { get; set; }

        public HashSet<string> AuthorsNames = new HashSet<string>();
        public List<Department> Departments = new List<Department>();

        public Library(string name, string address, int id)
        {
            Id = id;
            Name = name;
            Address = address;
        }

        public Library(XElement element)
        {
            ReadFromXElement(element, null);
        }

        public void AddAuthor(string authorName)
        {
            this.AuthorsNames.Add(authorName);
        }

        public void AddDepartment(Department departmentName)
        {
            this.Departments.Add(departmentName);
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

        public override XElement WriteToXml()
        {
            var libraryRoot = new XElement(GetType().Name,
                                new XAttribute("Id", Id),
                                new XAttribute("Name", Name),
                                new XAttribute("Address", Address));

            foreach (var d in Departments)
                libraryRoot.Add(d.WriteToXml());


            //foreach (var item in this.AuthorsNames)
            //{
            //    for (int i = 1; i < AuthorsNames.Count + 1; i++)
            //    {
            //        libraryRoot.Add(new XElement($"Author{i}", item));
            //    }
            //}

            return libraryRoot;
        }

        public override BaseEntity ReadFromXElement(XElement element, Library library)
        {
            throw new NotImplementedException();
        }

        public override Dictionary<string, string> FieldForEditing()
        {
            Dictionary<string, string> field = new Dictionary<string, string>()
            {
                { "Name", Name },
                {"Address", Address },
                {"Id", Id.ToString() }
            };
            return field;
        }
    }
}
