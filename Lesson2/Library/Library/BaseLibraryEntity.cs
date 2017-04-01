using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Lazar
{
    abstract class BaseLibraryEntity
    {
        private static int currentId = 0;
        private string id = (++currentId).ToString();

        public string ID
        {
            get { return id; }
            set { id = value; }
        }
        public string Name { get; set; }

        public string GetNodeName()
        {
            return GetType().Name;
        }

        public abstract BaseLibraryEntity ReadFromXElement(XElement element, Library library);
        public abstract XElement WriteToXElement();
        public abstract Dictionary<string, string> FieldsForUpdate();

    }
}
        