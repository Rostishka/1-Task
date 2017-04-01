using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Homework3
{
    public class Library : ICountingBooks
    {
            public string Address { get; set; }

            public string Name { get; set; }

            public int Id { get; set; }

            public List<Author> Autors { get; set; }
            public List<Department> Departments { get; set; }

            public Library(string name, string address, int id)
            {

                Id = id;
                Name = name;
                Address = address;
                Autors = new List<Author>();
                Departments = new List<Department>();
            }

            public Library()
            {
                Autors = new List<Author>();
                Departments = new List<Department>();
            }

            public IEnumerable<Department> Where(Func<Department, bool> d)
            {
                foreach (var item in Departments)
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
        }
}
