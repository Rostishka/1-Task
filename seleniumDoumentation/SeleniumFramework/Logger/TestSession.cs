using System;
using System.Collections.Generic;

namespace Logger
{
    internal class TestSession
    {
        internal TestSession() { TestCases = new List<TestCase>(); }

        internal TestCase CurrentTestCase; 
        internal List<TestCase> TestCases;

        internal string Name { get; set; }       	
        internal string Description { get; set; }         	
        internal DateTime StartTime { get; set; }
    }
}
