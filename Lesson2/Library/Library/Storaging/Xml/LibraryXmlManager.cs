using System.Xml.Linq;

namespace Lazar
{
    class LibraryXmlManager : BaseXmlManager
    {
        public LibraryXmlManager(string fileName) : base(fileName)
        {
        }

        public override BaseEntity LoadEntity()
        {
            XDocument doc = XDocument.Load(FileName);
            return new Library(doc.Root);
        }

    }
}
        