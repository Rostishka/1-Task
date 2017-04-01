using System;
using System.Xml.Linq;

namespace Lazar
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

            XDocument doc = new XDocument(library.WriteToXElement());

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

            XDocument doc = XDocument.Load(FileName);

            return new Library(doc.Root);
        }

        public void UpdateEntity(BaseLibraryEntity entity)
        {
            var doc = XDocument.Load(FileName);

            var element = RetrieveXElement(entity, doc.Root);
            if (element != null)
            {
                foreach (var field in entity.FieldsForUpdate())
                {
                    element.Attribute(field.Key).Value = field.Value;
                }
            }

            doc.Save(FileName);
        }

        public void DeleteEntity(BaseLibraryEntity entity)
        {
            var doc = XDocument.Load(FileName);

            var element = RetrieveXElement(entity, doc.Root);
            element?.Remove();

            doc.Save(FileName);
        }

        public void AddDepartment(Department department, Library library)
        {
            var doc = XDocument.Load(FileName);
            var departmentElement = department.WriteToXElement();
            var libraryElement = RetrieveXElement(library, doc.Root);
            libraryElement.Add(departmentElement);

            doc.Save(FileName);
        }

        public void AddBook(Department department, Book book)
        {
            var doc = XDocument.Load(FileName);
            var bookElement = book.WriteToXElement();
            var departmentElement = RetrieveXElement(department, doc.Root);
            departmentElement.Add(bookElement);

            doc.Save(FileName);
        }

        private XElement RetrieveXElement(BaseLibraryEntity entity, XElement element)
        {
            if (element.Name == entity.GetNodeName())
            {
                if (GetAttributeByName(element, "id") == entity.ID)
                    return element;
            }

                foreach (var elem in element.Elements())
            {
                
                if (elem.Name == entity.GetNodeName())
                {
                    if (GetAttributeByName(elem, "id") == entity.ID)
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
        