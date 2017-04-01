using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LinqToXml
{
    class Program
    {
        static void Main(string[] args)
        {
            #region 1. read xml file and print
            ReadXml();
            #endregion

            #region 2. Create the same xml document xml
            CreateXml();
            #endregion

            #region 3. Edit xml document xml
            EditXml();
            #endregion

            #region 4. Remove node from xml document
            RemoveNodeFromXml();
            #endregion


            Console.Read();

        }

        private static void ReadXml()
        {
            XDocument xDoc = XDocument.Load("Library.xml"); // завантадення документу

            if (xDoc != null && xDoc.Root.HasElements) // спосіб перевірки
            {
                Console.WriteLine(xDoc.ToString()); // виведення xml документу на консоль
                Console.Read();

                XElement bookLibraryXel = xDoc.Element("BooksLibrary");  // вибірка конкретного елемента
                if (bookLibraryXel != null && bookLibraryXel.HasElements)
                {
                    XElement libraryXel = bookLibraryXel.Element("library");

                    var libraryName = GetAttributeByName("name", libraryXel); // пошук атрибута 'name'
                    var libraryAdress = GetAttributeByName("adress", libraryXel); // пошук атрибута 'adress'

                    if (libraryXel != null && libraryXel.HasElements)
                    {
                        foreach (var departmentXel in libraryXel.Elements("departments"))
                        {
                            var departmentTitle = GetAttributeByName("title", departmentXel); // пошук атрибута 'title'
                            foreach (var bookXel in departmentXel.Elements("books"))
                            {
                                var bookTitle = GetAttributeByName("title", bookXel); // пошук атрибута 'title'
                                var bookAuthor = GetAttributeByName("author", bookXel); // пошук атрибута 'author'
                            }
                        }
                    }

                    IEnumerable<XElement> elements = xDoc.Element("BooksLibrary").Elements("library").Elements();
                    foreach (XElement element in elements)
                    {
                        Console.WriteLine(element.ToString());
                    }
                }
            }
        }
        private static void CreateXml()
        {
            XElement booksLibraryXElement = new XElement("BooksLibrary");

            XElement libraryXElement = new XElement("library");
            libraryXElement.SetAttributeValue("name", "Stefanyka");
            libraryXElement.SetAttributeValue("adress", "Stefanyka, Lviv");

            XElement departmentXElement = new XElement("departments");
            departmentXElement.SetAttributeValue("title", "IT");

            XElement bookXElement = new XElement("departments");
            bookXElement.SetAttributeValue("title", "OOPs Principle and Theory");
            bookXElement.SetAttributeValue("author", "Afzaal Ahmad Zeeshan");

            departmentXElement.Add(bookXElement);
            libraryXElement.Add(departmentXElement);
            booksLibraryXElement.Add(libraryXElement);

            XDocument newXdoc = new XDocument(booksLibraryXElement);
            newXdoc.Save("Library2.xml");

        }

        public static void EditXml()
        {
            XDocument xDoc = XDocument.Load("Library.xml"); // завантадення документу
            if (xDoc != null && xDoc.Root.HasElements) // спосіб перевірки
            {
                XElement bookLibraryXel = xDoc.Element("BooksLibrary");  // вибірка конкретного елемента
                if (bookLibraryXel != null && bookLibraryXel.HasElements)
                {
                    XElement libraryXel = bookLibraryXel.Element("library");
                    var libraryName = GetAttributeByName("name", libraryXel); // пошук атрибута 'name'
                    libraryName.Value += "_modified";
                    xDoc.Save("Library.xml");
                }
            }
        }

        public static void RemoveNodeFromXml()
        {
            XDocument xDoc = XDocument.Load("Library.xml"); // завантадення документу

            if (xDoc != null && xDoc.Root.HasElements) // спосіб перевірки
            {
                Console.WriteLine(xDoc.ToString()); // виведення xml документу на консоль
                Console.Read();

                XElement bookLibraryXel = xDoc.Element("BooksLibrary");  // вибірка конкретного елемента
                if (bookLibraryXel != null && bookLibraryXel.HasElements)
                {
                    XElement libraryXel = bookLibraryXel.Element("library");

                    var libraryName = GetAttributeByName("name", libraryXel); // пошук атрибута 'name'
                    var libraryAdress = GetAttributeByName("adress", libraryXel); // пошук атрибута 'adress'

                    if (libraryXel != null && libraryXel.HasElements)
                    {
                        foreach (var departmentXel in libraryXel.Elements("departments"))
                        {
                            var departmentTitle = GetAttributeByName("title", departmentXel); // пошук атрибута 'title'
                            var firstBook = departmentXel.Elements("books").FirstOrDefault();
                            if (firstBook != null)
                                firstBook.Remove();

                            xDoc.Save("Library.xml");                            
                        }
                    }
                }
            }
        }
        private static XAttribute GetAttributeByName(string name, XElement xElement)
        {
            return xElement.Attributes().FirstOrDefault(x => x.Name == name);
        }
    }
}
