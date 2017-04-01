using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Json_and_xml
{
    class Program
    {
        static void Main(string[] args)
        {

            string xml = File.ReadAllText("LIbraryDataMy.xml");
            var catalog1 = xml.ParseXML<catalog>();

            //string json = File.ReadAllText(@"D:\file.json");
            //var catalog2 = json.ParseJSON<JSONRoot>();
            Console.WriteLine("Something");
        }
         
/// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
        public partial class Library
        {

            private string nameField;

            private string addressField;

            private LibraryDapartment[] departmentsField;

            private object autorsField;

            private string idField;

            /// <remarks/>
            public string Name
            {
                get
                {
                    return this.nameField;
                }
                set
                {
                    this.nameField = value;
                }
            }

            /// <remarks/>
            public string Address
            {
                get
                {
                    return this.addressField;
                }
                set
                {
                    this.addressField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlArrayItemAttribute("Dapartment", IsNullable = false)]
            public LibraryDapartment[] Departments
            {
                get
                {
                    return this.departmentsField;
                }
                set
                {
                    this.departmentsField = value;
                }
            }

            /// <remarks/>
            public object Autors
            {
                get
                {
                    return this.autorsField;
                }
                set
                {
                    this.autorsField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Id
            {
                get
                {
                    return this.idField;
                }
                set
                {
                    this.idField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class LibraryDapartment
        {

            private string nameField;

            private LibraryDapartmentAuthor[] autorsField;

            private object booksField;

            private byte idField;

            /// <remarks/>
            public string Name
            {
                get
                {
                    return this.nameField;
                }
                set
                {
                    this.nameField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlArrayItemAttribute("Author", IsNullable = false)]
            public LibraryDapartmentAuthor[] Autors
            {
                get
                {
                    return this.autorsField;
                }
                set
                {
                    this.autorsField = value;
                }
            }

            /// <remarks/>
            public object Books
            {
                get
                {
                    return this.booksField;
                }
                set
                {
                    this.booksField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte Id
            {
                get
                {
                    return this.idField;
                }
                set
                {
                    this.idField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class LibraryDapartmentAuthor
        {

            private string nameField;

            private LibraryDapartmentAuthorBook[] booksField;

            private byte idField;

            /// <remarks/>
            public string Name
            {
                get
                {
                    return this.nameField;
                }
                set
                {
                    this.nameField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlArrayItemAttribute("Book", IsNullable = false)]
            public LibraryDapartmentAuthorBook[] Books
            {
                get
                {
                    return this.booksField;
                }
                set
                {
                    this.booksField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte Id
            {
                get
                {
                    return this.idField;
                }
                set
                {
                    this.idField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class LibraryDapartmentAuthorBook
        {

            private string bookTitleField;

            private object autorsField;

            private byte quontatyField;

            private ushort priceField;

            private byte idField;

            /// <remarks/>
            public string BookTitle
            {
                get
                {
                    return this.bookTitleField;
                }
                set
                {
                    this.bookTitleField = value;
                }
            }

            /// <remarks/>
            public object Autors
            {
                get
                {
                    return this.autorsField;
                }
                set
                {
                    this.autorsField = value;
                }
            }

            /// <remarks/>
            public byte Quontaty
            {
                get
                {
                    return this.quontatyField;
                }
                set
                {
                    this.quontatyField = value;
                }
            }

            /// <remarks/>
            public ushort Price
            {
                get
                {
                    return this.priceField;
                }
                set
                {
                    this.priceField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte Id
            {
                get
                {
                    return this.idField;
                }
                set
                {
                    this.idField = value;
                }
            }
        }


    }
}
