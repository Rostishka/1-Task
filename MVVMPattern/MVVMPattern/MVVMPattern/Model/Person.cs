using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMPattern.Model
{
    public class Person
    {
        public String FirstName{ get; set; }
        public String SecondName { get; set; }
        public Int32 Age { get; set; }
        public DateTime BirthDay { get; set; }
        public Int32 PhoneNumber { get; set; }
        public String Email { get; set; }
        public Int32 Id { get; set; }
    }
}
