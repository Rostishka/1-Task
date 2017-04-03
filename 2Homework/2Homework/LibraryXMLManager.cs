using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace _2Homework
{
    class LibraryXMLManager
    {
        private string fileName;

        public string FileName
        {
            get { return fileName; }
        }

        public LibraryXMLManager(string fileName)
        {
            this.fileName = fileName;
        }

        public static String GetAttributeByName(XElement elem, String attrName)
        {
            return elem.Attribute(attrName).Value.ToString();
        }

        public void SaveLibrary(Library library)
        {

            XDocument doc = new XDocument(library.WriteToXml());

            try
            {
                doc.Save(FileName);
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occured while saving to file " + FileName);
                Console.WriteLine(e.Message);
            }

        }

        public Library LoadLibrary()
        {

            XDocument xDoc = XDocument.Load(FileName);

            return new Library(xDoc.Root);
        }

        public void UpdateEntity(BaseEntity entity)
        {
            var xDoc = XDocument.Load(FileName);

            var element = RetrieveXElement(entity, xDoc.Root);
            if (element != null)
            {
                foreach (var field in entity.FieldForEditing())
                {
                    element.Attribute(field.Key).Value = field.Value;
                }
            }

            xDoc.Save(FileName);
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
                if (GetAttributeByName(element, "Id") == entity.Id.ToString())
                    return element;
            }

            foreach (var elem in element.Elements())
            {

                if (elem.Name == entity.GetNodeName())
                {
                    if (GetAttributeByName(elem, "Id") == entity.Id.ToString())
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
    }
}
