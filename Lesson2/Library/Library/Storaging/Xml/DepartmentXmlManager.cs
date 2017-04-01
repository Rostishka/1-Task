using System;
using System.Xml.Linq;

namespace Lazar
{
    class DepartmentXmlManager : BaseXmlManager
    {
        private readonly Library _library;

        public DepartmentXmlManager(string fileName, Library library) : base(fileName)
        {
            _library = library;
        }

        public void AddDepartment(Department department)
        {
            var doc = XDocument.Load(FileName);
            var departmentElement = department.WriteToXElement();
            var libraryElement = RetrieveXElement(_library, doc.Root);
            libraryElement.Add(departmentElement);

            doc.Save(FileName);
        }

        public override BaseEntity LoadEntity()
        {
            throw new NotImplementedException();
        }
    }
}
        