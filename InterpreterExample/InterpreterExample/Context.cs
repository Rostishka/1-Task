using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InterpreterExample
{
    class Context
    {   //
        Dictionary<string, Dictionary<string, string>> addString = new Dictionary<string,Dictionary<string, string>>();
        private string _input;
        private int _output;

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

        // Gets or sets output
        public int Output
        {
            get { return _output; }
            set { _output = value; }
        }
    }
}
