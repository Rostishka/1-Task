using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace _2Homework
{
    public class XmlManager
    {
        protected static string _fileName = "LibraryNEW.xml";

        //public string Name { get; set; }

        private static XAttribute GetAttributeByName(string name, XElement xElement)
        {
            return xElement.Attributes().FirstOrDefault(x => x.Name == name);
        }

        public void SaveBook(Book book)
        {
            XDocument xDoc = new XDocument(book.WriteToXml());

            try
            {
                xDoc.Save(_fileName);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Can't save library");
                Console.WriteLine(ex.Message);
            }
        }

        //public static void Serialize(T obj)
        //{
        //    XmlSerializer serializer = new XmlSerializer(typeof(T));
        //    using (StreamWriter writer = new StreamWriter(_fileName))
        //    {
        //        serializer.Serialize(writer, obj);
        //    }
        //}
        //=====================================================================================

        //public static void ReadXml()
        //{
        //    XDocument xDoc = XDocument.Load(_fileName); // завантадення документу

        //    if (xDoc != null && xDoc.Root.HasElements) // спосіб перевірки
        //    {
        //        Console.WriteLine(xDoc.ToString()); // виведення xml документу на консоль
        //        Console.Read();
        //    }
        //}
        //=====================================================================================

        //public static void Deserialize(T d)
        //{
        //    XmlSerializer serializer = new XmlSerializer(typeof(T));
        //    using (StreamReader reader = new StreamReader(_fileName))
        //    {
        //        serializer.Deserialize(reader);
        //    }
        //}
        //=====================================================================================

        // Can't edit XML file
        //________________HELP_______________
        //public static void EditXml()
        //{
        //    XmlDocument xmlDoc = new XmlDocument();
        //    xmlDoc.Load(_fileName);

        //    foreach (XmlElement element in xmlDoc.SelectNodes("/Library"))
        //    {
        //        foreach (XmlElement element1 in element)
        //        {
        //            if (element.SelectSingleNode("Name").InnerText == "Ternopilska Library")
        //            {
        //                //MessageBox.Show(element1.InnerText);
        //                XmlNode newvalue = xmlDoc.CreateElement("Name");
        //                newvalue.InnerText = "MODIFIED";
        //                element.ReplaceChild(newvalue, element1);

        //                xmlDoc.Save(_fileName);
        //            }
        //        }
        //    }

        //XDocument xDoc = XDocument.Load(_fileName);
        //xDoc.Descendants("Search").Where(x => x.Element("title").Value == tbSearch.Text).Single().Descendants("count").Single().Value = "1";
        //=====================================================================================


        //XDocument xDoc = XDocument.Load(_fileName);
        //var items = from item in xDoc.Descendants("item") where item.Element("Name").Value == name select item;
        //foreach (XElement itemElement in items)
        //{
        //    itemElement.SetElementValue("Name", "Lord of the Rings Figures");
        //}
        //xDoc.Save(_fileName);
        //=====================================================================================


        //XDocument xDoc = XDocument.Load(_fileName);
        //var libName = xDoc.Descendants("Library").SingleOrDefault("Name", null);
        //libName.Element("Name").Value = "SAfgsd";
        //xDoc.Save(_fileName);
        //("Name", null)
        //=====================================================================================


        //XDocument xDoc = XDocument.Load(_fileName);
        //xDoc.Descendants("Name").First().Value = "New Name";
        //xDoc.Save(_fileName);
        // завантадення документу
        //if (xDoc != null && xDoc.Root.HasElements) // спосіб перевірки
        //{
        //    XElement bookLibraryXel = xDoc.Element("Library");  // вибірка конкретного елемента
        //    if (bookLibraryXel != null && bookLibraryXel.HasElements)
        //    {
        //        XElement libraryXel = bookLibraryXel.Element("Department");
        //        var libraryName = GetAttributeByName("Id", libraryXel); // пошук атрибута 'Id'!!!xElement было null.
        //        libraryName.Value += "_modified";
        //        xDoc.Save(_fileName);
        //    }
        //}

    }
}
