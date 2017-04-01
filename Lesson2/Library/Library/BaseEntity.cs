using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Lazar
{
    abstract class BaseEntity
    {
        private static int _currentId;

        public string Id { get; set; } = (++_currentId).ToString();

        public string Name { get; set; }

        public string GetNodeName()
        {
            return GetType().Name;
        }

        public abstract BaseEntity ReadFromXElement(XElement element, Library library);
        public abstract XElement WriteToXElement();
        public abstract Dictionary<string, string> FieldsForUpdate();

    }
}
        