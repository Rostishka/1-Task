using System;

namespace DAL.Models
{
    public class Student : BaseEntity
    {
        public String Name { get; set; }
        public String Surname { get; set; }
    }
}
