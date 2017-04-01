using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace _1._1
{
    public class Library : AbstractTest, ICountingBooks
    {
       
        private int _numOfDepartments;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public Library()
            : this("") { }

        public Library(string name)
        {
            Name = name;
        }

        public void AddDepartment(Department departmentName)
        {
            departments.Add(departmentName);
        }

        public override string ToString()
        {
            return Name + " library has " + _numOfDepartments + " departments and contains " + _books + " books";
        }

        public void CountDepartments()
        {
            _numOfDepartments = departments.Count();
        }

        public void CountBooks()
        {
            foreach (Department d in departments)
            {
                _books += d._books;
            }
        }
    }
}
