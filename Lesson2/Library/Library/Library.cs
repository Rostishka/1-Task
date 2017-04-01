using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Lazar
{
    class Library : BaseEntity, ICountingBooks
    {

        public string Address { get; set;}
        private List<Department> departments = new List<Department>();
        private HashSet<Author> authors = new HashSet<Author>();

        public HashSet<Author> Authors {
            get { return authors; }
        }

        public List<Department> Departments {
            get { return departments; }
        }

        public Library(string name, string address)
        {
            this.Name = name;
            this.Address = address;
        }

        public Library(XElement element)
        {
            ReadFromXElement(element, null);
        }

        public Library() { }

        public void AddDepartment(Department department)
        {
            departments.Add(department);
        }

        public override string ToString()
        {
            return "Library: " + this.Name;
        }

        public int BooksCount()
        {
            int count = 0;
            foreach (Department dep in departments) {
                count += dep.BooksCount();
            }

            return count;
        }

        public override XElement WriteToXElement()
        {
            var libraryRoot = new XElement(GetNodeName(),
                                            new XAttribute("id", Id),
                                            new XAttribute("name", Name),
                                            new XAttribute("address", Address)
                                            );
            foreach (var dep in this.departments)
                libraryRoot.Add(dep.WriteToXElement());

            return libraryRoot;
        }

        public sealed override BaseEntity ReadFromXElement(XElement element, Library library)
        {
            this.Id = BaseXmlManager.GetAttributeByName(element, "id");
            this.Name = BaseXmlManager.GetAttributeByName(element, "name");
            this.Address = BaseXmlManager.GetAttributeByName(element, "address");
            foreach (var elem in element.Elements())
            {
                var dep = (Department) new Department().ReadFromXElement(elem, this);
                this.AddDepartment(dep);    
            }

            return this;
        }

        public override Dictionary<string, string> FieldsForUpdate()
        {
            Dictionary<string, string> fields = new Dictionary<string, string>
            {
                { "name", Name },
                { "address", Address }
            };

            return fields;
        }
    }
}
