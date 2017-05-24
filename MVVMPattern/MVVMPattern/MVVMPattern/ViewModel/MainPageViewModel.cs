using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVVMPattern.Model;

namespace MVVMPattern.ViewModel
{
    public class MainPageViewModel
    {
        public String FullName { get; set; }
        public Int32 Age { get; set; }
        public Int32 PhoneNumber { get; set; }
        public String Email { get; set; }

        public MainPageViewModel(Person person)
        {
            FullName = person.FirstName + " " + person.SecondName;
            Age = ClacAge(person.BirthDay);
            PhoneNumber = person.PhoneNumber;
            Email = person.Email;
        }

        public Int32 ClacAge(DateTime dt)
        {
            return 17;
        }

        public static Person GetPerson()
        {
            var person = new Person()
            {
                FirstName = "Rostik",
                SecondName = "Diakiv",
                PhoneNumber = 34226457,
                BirthDay = new DateTime(1999, 6, 9),
                Id = 1,
                Email = "rosdtik0945@#gmail.cxom"
            };
            return person;
        }
    }
}
