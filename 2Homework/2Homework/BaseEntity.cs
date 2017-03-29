using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Linq;

namespace _2Homework
{
    public abstract class BaseEntity
    {
        public string GetNodeName()
        {
            return GetType().Name;
        }

        public abstract XElement WriteToXml();
    }
}
