using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Homework4
{
    class Context
    {
        //Dictionary<string, Dictionary<string, Dictionary<string, string>>> variables;
        //public Context()
        //{
        //    variables = new Dictionary<string, Dictionary<string, Dictionary<string, string>>>();
        //}

        private string _input;

        // Constructor
        public Context(string input)
        {
            _input = input;
        }       
        // Gets or sets input
        public string Input
        {
            get { return _input; }
            set { _input = value; }
        }
    }
}
