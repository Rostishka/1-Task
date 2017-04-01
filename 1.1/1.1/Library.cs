using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace _1._1
{
     class Library : ICountingBooks
    {
        public List<Department> departments = new List<Department>();
        public List<Book> booooks = new List<Book>();
        public int _books;
        private string _name;
        public int _libBooks = 0;
        public int _numOfDepartments;

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
            return Name + " Library has " + _numOfDepartments + " departments";
        }

        public void CountDepartments()
        {
            _numOfDepartments = departments.Count();
        }

        public void ShowNumOfDeps()
        {
            Console.WriteLine("LIBRARY contains " + _numOfDepartments + " Departments");
        }

        public virtual void ShowNumOfBooks()
        {
            Console.WriteLine("Library contains " + _books + " books");
        }

        public void CountBooks()
        {
            foreach (Department b in departments)
            {
                _books += b._books;
            }
        }
    }
}
