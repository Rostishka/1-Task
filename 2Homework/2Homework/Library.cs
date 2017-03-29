using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace _2Homework
{
    [XmlRoot]
    public class Library : ICountingBooks, IComparable
    {
        public string Address { get; set; }
        public string Name { get; set; }
        public int Id { get; set; }

        public HashSet<string> AuthorsNames = new HashSet<string>();
        public List<Department> Departments = new List<Department>();

        public Library(string name, string address, int id)
        {
            Id = id;
            Name = name;
            Address = address;
        }

        public void AddAuthor(string authorName)
        {
            AuthorsNames.Add(authorName);
        }

        public void AddDepartment(Department departmentName)
        {
            Departments.Add(departmentName);
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
    }
}
