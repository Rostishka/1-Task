using System.Text;

namespace LinqToObject
{
    public class User
    {
        public string LastName { get; set; }

        public string FirstName { get; set; }

        public string Login { get; set; }

        public int Age { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("Last Name: {0}\n", LastName);
            sb.AppendFormat("First Name: {0}\n", FirstName);
            sb.AppendFormat("Login: {0}\n", Login);
            sb.AppendFormat("Age: {0}\n", Age);
            sb.Append("__________________________________\n");
            return sb.ToString();
        }
    }
}
