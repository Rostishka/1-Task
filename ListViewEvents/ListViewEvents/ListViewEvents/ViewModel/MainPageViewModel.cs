using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ListViewEvents.Model;

namespace ListViewEvents.ViewModel
{
    class MainPageViewModel
    {
        public String FullName { get; set; }
        public String CompanyName { get; set; }
        public String Language { get; set; }

        public List<Person> Employees
        {
            get
            {
                return new List<Person>()
                {
                    new Person() {FullName = "RostyslavDiakiv", CompanyName = "Eleks", Language = "C#/.Net"},
                    new Person() {FullName = "Bohdsan Diakiv", CompanyName = "Bang", Language = "Java"},
                    new Person() {FullName = "Vovodymyr Tiutka", CompanyName = "School", Language = "C++"},
                    new Person() {FullName = "Soltys Vlad", CompanyName = "SoftServe", Language = "Ruby"},
                    new Person() {FullName = "Petro Povroznik", CompanyName = "Crowdin", Language = "C"},
                    new Person() {FullName = "Pksana Dyakiv", CompanyName = "Magnetic One", Language = "JavaScript"},
                    new Person() {FullName = "Roan Dilay", CompanyName = "Magnis", Language = "HTML/CSS"},
                };
            }
        }
    }
}
