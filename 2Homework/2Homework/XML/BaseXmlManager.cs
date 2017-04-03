using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;


namespace _2Homework
{
    public abstract class BaseXmlManager : BaseManager<BaseEntity>
    {
        public BaseXmlManager(string _fileName) : base(_fileName)
        {
        }

        public static string GetAttributeByName(XElement element, string attribName)
        {
            return element.Attribute(attribName).Value.ToString();
        }

        protected XElement RetrieveXmlElement(XElement element, BaseEntity entity)//Base entity where is GetNodename() Mehtod
        {
            if(element.Name == entity.GetNodeName())
            {
                if (GetAttributeByName(element, "Id") == entity.Id.ToString())
                    return element;
            }

            foreach(XElement elem in element.Elements())
            {
                if (elem.Name == entity.GetNodeName())
                {
                    if (GetAttributeByName(elem, "Id") == entity.Id.ToString())
                        return elem;
                    else continue;
                }
                else
                {
                    return RetrieveXmlElement(elem, entity);
                }
            }
            return null;
        }

        public override void DeleteEntity(BaseEntity entity)
        {
            XDocument xDoc = XDocument.Load(FileName);

            var element = RetrieveXmlElement(xDoc.Root, entity);
            element.Remove();

            xDoc.Save(FileName);
        }

        public override void EditEntity(BaseEntity entity)
        {
            XDocument xDoc = XDocument.Load(FileName);

            var element = RetrieveXmlElement(xDoc.Root, entity);

            if(element != null)
            { 
               foreach(var field in entity.FieldForEditing())
                {
                    element.Attribute(field.Key).Value = field.Value;
                }
            }
            xDoc.Save(FileName);
        }

        public override void SaveEntity(BaseEntity entity)
        {
            XDocument xDoc = new XDocument(entity.WriteToXml());

            try
            {
                xDoc.Save(FileName);
            }
            catch(Exception ex)
            {
                Console.WriteLine("Sorry, Can't save this entity: " + entity);
                Console.WriteLine(ex.Message);
            }
        }

     
        public abstract override BaseEntity LoadEntity();
    }
}
