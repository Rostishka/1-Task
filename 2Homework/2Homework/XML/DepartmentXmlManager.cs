using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace _2Homework.XML
{
    class DepartmentXmlManager : BaseXmlManager
    {
        private readonly Library _library;

        public DepartmentXmlManager(string _fileName, Library library) : base(_fileName)
        {
           
        }

        public void AddDepartment(Department department)
        {
            var xDoc = XDocument.Load(FileName);
            var departmentElement = department.WriteToXml();
            var libraryElement = RetrieveXmlElement(xDoc.Root, _library);
            libraryElement.Add(departmentElement);

            xDoc.Save(FileName);
        }

        public override BaseEntity LoadEntity()
        {
            throw new NotImplementedException();
        }
    }
}
