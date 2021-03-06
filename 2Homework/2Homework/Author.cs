﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace _2Homework
{
    public class Author : IComparable
    { 
        public string Name { get; set; }
        public int Id { get; set; }
        public int Age { get; set; }

        private HashSet<string> BooksTitles = new HashSet<string>();

        public Author(string name, int age, int id)
        {
            Name = name;
            Id = id;
            Age = age;
        }
        
        public void AddBook(string bookName)
        {
            BooksTitles.Add(bookName);
        }

        public int CountBooks()
        {
            int q = 0;
            foreach (var item in BooksTitles)
            {
                q ++;
            }
            return q;
        }

        public int CompareTo(object obj)
        {
            if (obj != null)
            {
                Author otherAuthor = obj as Author;
                if (otherAuthor != null)
                {
                    return CountBooks().CompareTo(otherAuthor.CountBooks());
                }
                else
                    throw new ArgumentException("Object doesn't contain any books");
            }
            else return 0;
        }
        public static void ShowComparapble(Author a1, Author a2)
        {
            Console.WriteLine("Department {0} contains more books than Department {1} : {2}", a1.Name, a2.Name, a1.CompareTo(a2));
        }
    }
}
