using System;
using System.Collections.Generic;
using System.Reflection;
using Logger;
using NUnit.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Tests
{
    [TestFixture]
    [TestClass]
    public class TestingLogger : BaseTest
    {
        [Test]
        [TestMethod]
        public void ErrorTest()
        {
            Report.StartTestCase(this.GetType().Name, MethodBase.GetCurrentMethod().Name, "Error test", new Dictionary<string, string> { { "Test with error message", "Test fails" } });
            Report.RunStep();
            Report.AddError("Error message", "Error is logged");
            Report.AddInfo("nUnit verification");
            Report.AddInfo("MSTest verification");
        }

        [Test]
        [TestMethod]
        public void WarningTest()
        {
            Report.StartTestCase(this.GetType().Name, MethodBase.GetCurrentMethod().Name, "Warning test", new Dictionary<string, string>   
             {   
  	                   	{ "Test with warning message", "Test fails" }   
             });
            Report.RunStep();
            Report.AddWarning("Warning message", "Warning is logged");
            Report.AddInfo("nUnit verification");
            Report.AddInfo("MSTest verification");
        }

        [Test]
        [TestMethod]
        public void NoRunStepsTest()
        {
            Report.StartTestCase(this.GetType().Name, MethodBase.GetCurrentMethod().Name, "Test with NoRun steps", new Dictionary<string, string>   
  	                   	{   
  	      	   	             { "Step 1", "Pass" },   
  	                     	 { "Step 2", "No run" },   
  	                     	 { "Step 3", "No run" },   
  	                     	 { "Step 4", "No run" }   
  	                   	});
            Report.RunStep();
            Report.AddInfo("message", "");
            Report.AddInfo("nUnit verification"); Report.AddInfo("MSTest verification");
        }

        [Test]
        [TestMethod]
        public void RedundantStepsTest()
        {
            Report.StartTestCase(this.GetType().Name, MethodBase.GetCurrentMethod().Name, "Redundant steps", new Dictionary<string, string>   
  	                   	{   
  	      	   	             { "Step 1", "Pass" }  
  	                   	});
            Report.RunStep();
            Report.AddInfo("message", "");
            Report.RunStep();
            Report.AddInfo("Redundant #1", "");
            Report.RunStep();
            Report.AddInfo("Redundant #2", "");
            Report.AddInfo("nUnit verification"); 
            Report.AddInfo("MSTest verification");
        }
    }
}
