using System;
using System.Collections.Generic;

namespace Logger
{
    internal class TestCase
    {
        internal TestCase()
        {
            TestScope = new Dictionary<string, string>();
            TestSteps = new List<TestStep>();
            CurrentStepIndex = 0;
        }

        internal TestStep CurrentStep; 
        internal Dictionary<string, string> TestScope;
        internal List<TestStep> TestSteps;
        
        internal Report.Status Status { get; set; }
        internal string TestCollectionName { get; set; }
        internal string Description { get; set; }        
        internal TimeSpan Duration { get; set; }        
        internal string Name { get; set; }         
        internal DateTime StartTime { get; set; }        
        internal int CurrentStepIndex { get; set; }
    }
}
