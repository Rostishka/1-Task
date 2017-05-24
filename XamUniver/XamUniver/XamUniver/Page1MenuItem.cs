using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XamUniver
{

    public class Page1MenuItem
    {
        public Page1MenuItem()
        {
            TargetType = typeof(Page1Detail);
        }
        public int Id { get; set; }
        public string Title { get; set; }

        public Type TargetType { get; set; }
    }
}
