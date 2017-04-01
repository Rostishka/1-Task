using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqP2.Library
{
    public class FormatAttribute:Attribute
    {
        public FormatAttribute(string format)
        {
            FormatString = format;
        }
        public string FormatString { get; set; }
    }
}
