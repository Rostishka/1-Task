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
        public string Name { get; set; }
        private static int _currentId;

        public string Id { get; set; } = (++_currentId).ToString();

        public string GetNodeName()
        {
            return GetType().Name;
        }

        public abstract XElement WriteToXml();
        public abstract BaseEntity ReadFromXElement(XElement element, Library library);
        public abstract Dictionary<string, string> FieldForEditing();
    }
}
