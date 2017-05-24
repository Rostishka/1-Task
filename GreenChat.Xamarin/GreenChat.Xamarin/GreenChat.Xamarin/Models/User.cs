using System;

namespace GreenChat.Xamarin.Models
{
    public class User
    {
        public Int32 Id { get; set; }

        public String FullName { get; set; }

        public String Email { get; set; }

        public String Password { get; set; }

        public String PhoneNumber { get; set; }

        enum Sex
        {
            Male,
            Female
        }
    }
}
