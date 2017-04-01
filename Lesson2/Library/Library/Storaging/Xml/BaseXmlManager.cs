using System;
using System.Xml.Linq;

namespace Lazar
{
    abstract class BaseXmlManager : BaseManager<BaseEntity>
    {
        protected BaseXmlManager(string fileName) : base(fileName)
        {
        }

        public static String GetAttributeByName(XElement elem, String attrName)
        {
            return elem.Attribute(attrName).Value.ToString();
        }

        public override void SaveEntity(BaseEntity entity)
        {

            XDocument doc = new XDocument(entity.WriteToXElement());

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

        public abstract override BaseEntity LoadEntity();

        public override void UpdateEntity(BaseEntity entity)
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

        public override void DeleteEntity(BaseEntity entity)
        {
            var doc = XDocument.Load(FileName);

            var element = RetrieveXElement(entity, doc.Root);
            element?.Remove();

            doc.Save(FileName);
        }

        protected XElement RetrieveXElement(BaseEntity entity, XElement element)
        {
            if (element.Name == entity.GetNodeName())
            {
                if (GetAttributeByName(element, "id") == entity.Id)
                    return element;
            }

                foreach (var elem in element.Elements())
            {
                
                if (elem.Name == entity.GetNodeName())
                {
                    if (GetAttributeByName(elem, "id") == entity.Id)
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
        