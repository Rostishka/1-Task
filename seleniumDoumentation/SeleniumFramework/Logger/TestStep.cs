using System;
using System.Collections.Generic;

namespace Logger
{
    internal class TestStep
    {
        internal TestStep() { SubSteps = new List<TestStep>(); }

        internal List<TestStep> SubSteps;

        internal Report.Status Status { get; set; }         
        internal string Description { get; set; }      
        internal string Tooltip { get; set; }        
        internal string ExpectedResult { get; set; }         
        internal string ActualResult { get; set; }        
        internal string ScreenshotPath { get; set; }        
        internal DateTime StartTime { get; set; }          
    }
}
