using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace _2Homework
{
    class LibraryXmlManager
    {
        private string _fileName;

        public string FileName
        {
            get { return _fileName; }
        }

        public LibraryXmlManager(string fileName)
        {
            _fileName = fileName;
        }

        public Library LoadEntity()
        {
            XDocument xDoc = XDocument.Load(FileName);
            return new Library(xDoc.Root);
        }

        public void SaveLibrary(Library library)
        {
            XDocument xDoc = new XDocument(library.WriteToXml());

            try
            {
                xDoc.Save(_fileName);
            }
            catch(Exception ex)
            {
                Console.WriteLine("Can't save your file!!!");
                Console.WriteLine(ex.Message);
            }
        }

        public Library LoadLibrary()
        {
            XDocument xDoc = XDocument.Load(_fileName);

            return new Library(xDoc.Root);
        }

        public void UpdateEntity(BaseEntity entity)
        {
            var doc = XDocument.Load(FileName);

            var element = RetrieveXElement(entity, doc.Root);
            if (element != null)//here element == null figure out with it
            {
                foreach (var field in entity.FieldForEditing())
                {
                    element.Attribute(field.Key).Value = field.Value;
                }
            }

            doc.Save(FileName);
        }

        public void DeleteEntity(BaseEntity entity)
        {
            var doc = XDocument.Load(FileName);

            var element = RetrieveXElement(entity, doc.Root);
            element?.Remove();

            doc.Save(FileName);
        }

        public void AddDepartment(Department department, Library library)
        {
            var doc = XDocument.Load(FileName);
            var departmentElement = department.WriteToXml();
            var libraryElement = RetrieveXElement(library, doc.Root);
            libraryElement.Add(departmentElement);

            doc.Save(FileName);
        }

        public void AddBook(Department department, Book book)
        {
            var doc = XDocument.Load(FileName);
            var bookElement = book.WriteToXml();
            var departmentElement = RetrieveXElement(department, doc.Root);
            departmentElement.Add(bookElement);

            doc.Save(FileName);
        }

        private XElement RetrieveXElement(BaseEntity entity, XElement element)
        {
            if (element.Name == entity.GetNodeName())
            {
                if (GetAttributeByName(element, "Id") == entity.Id)
                    return element;
            }

            foreach (var elem in element.Elements())
            {

                if (elem.Name == entity.GetNodeName())
                {
                    if (GetAttributeByName(elem, "Id") == entity.Id)
                        return elem;
                    else
                        continue;
                }
                else
                {
                    return RetrieveXElement(entity, elem);
                }
            }

            return null;
        }

        public static string GetAttributeByName(XElement elem, String attrName)
        {
            return elem.Attribute(attrName).Value.ToString();
        }
    }
}
