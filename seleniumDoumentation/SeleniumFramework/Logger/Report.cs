using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.IO.Compression; 
using System.Net;  
using System.Net.Mail;

namespace Logger
{
    public static class Report
    {
        /// <summary>   
        /// constructor by default       
        /// </summary>      
        static Report()
        {
            TestSession.StartTime = DateTime.Now;
        }

        internal enum Status
        {
            Pass, Fail,
            NoRun, Warning
        }

        /// <summary>an instance of a test execution model</summary>       
        private static readonly TestSession TestSession = new TestSession();

        /// <summary>path to log file on a local drive</summary>   
        private static string logFilePath = string.Empty; 
        /// <summary>indicates if event logging must be provided</summary>
        public static bool doEventLogging = false;


        /// <summary>   
        /// path to a folder on a local drive for saving the report   
        /// </summary>      
        public static string ReportFolderPath;

        /// <summary>   
        /// Initialize variables general for current test session        
        /// </summary>   
        /// <param name="TestSessionName">Name of current test session</param>      
        /// <param name="TestSessionDescription">Description of current test session</param>        
        public static void StartTestSession(string TestSessionName, string TestSessionDescription = null)
        {
            TestSession.Description = TestSessionDescription ?? string.Empty; 
            TestSession.Name = string.IsNullOrEmpty(TestSessionName) ? "Undefined" : TestSessionName;
            ReportFolderPath = Directory.GetCurrentDirectory() + "\\Reports\\" + TestSession.Name + "_" + TestSession.StartTime.ToString(CultureInfo.InvariantCulture).Replace(":", string.Empty).Replace("/", string.Empty).Replace(" ", string.Empty);
            Directory.CreateDirectory(ReportFolderPath);
            logFilePath = ReportFolderPath + "\\log.txt";
            Log("Start test session: " + TestSession.Name + (string.IsNullOrEmpty(TestSession.Description) ? string.Empty : ", " + TestSession.Description + "\r\n"));   
        }

        /// <summary>   
        /// Initialize variables of current test case   
        /// </summary>   
        /// <param name="collection">Name of test collection which the test case belongs to</param>   
        /// <param name="name">Name of test case</param>   
        /// <param name="description">Summary description of the test case</param> 
        /// <param name="testScope">The test scope (description of test case steps in format 'step action, expected result')</param>   
        public static void StartTestCase(string collection, string name, string description, Dictionary<string, string> testScope)
        {
            TestSession.CurrentTestCase = new TestCase
            {
                StartTime = DateTime.Now,
                TestCollectionName = collection,
                Name = string.IsNullOrEmpty(name) ? DateTime.Now.ToString(CultureInfo.InvariantCulture).Replace(":", string.Empty).Replace("/", string.Empty).Replace(" ", string.Empty) : name,
                Description = description ?? string.Empty,
                TestScope = testScope ?? new Dictionary<string, string> { { "No test scope is declared for the test case", string.Empty } }
            };
            if (doEventLogging)
            {
                Log("\r\nStart test case: " + TestSession.CurrentTestCase.Name + ", " + TestSession.CurrentTestCase.Description + ", Test collection: " + TestSession.CurrentTestCase.TestCollectionName); 
                string message = string.Empty;
                message = TestSession.CurrentTestCase.TestScope.Aggregate(message, (current, scope) => current + ("\t" + scope.Key + " - " + scope.Value + "\r\n"));
                Log("Test case scope:\r\n" + message); 
            }
        }

        /// <summary>   
        /// Adds current test step to the list of steps of current test case 
        /// </summary>        
        private static void SaveCurrentTestStep()
        {
            var currentStep = TestSession.CurrentTestCase.CurrentStep;
            if (currentStep == null)
                return; //if there is no current step so there is nothing to save   

            //currently this path is empty because there is no method for saving screenshots yet.   
            currentStep.ScreenshotPath = string.Empty;

            //Status.NoRun is the initial status of a step.   
            //If there is no substeps it means the step haven’t been performed and it’s state is No run.   
            if (!currentStep.Status.Equals(Status.Warning))
                currentStep.Status = currentStep.SubSteps.Count > 0 ? Status.Pass : Status.NoRun;

            //Sets the screenshot of the last substep as the step’s screenshot which  displays status of the application after step is executed   
            if (currentStep.SubSteps.Count > 0)
                currentStep.ScreenshotPath = currentStep.SubSteps.Last().ScreenshotPath;

            //looking through all substeps.   
            //If any of the substeps is marked as Warning the step is marked the same,   
            //if any is marked Fail, the step status is Fail.             
            //Otherwise the step status remains Pass. 
            foreach (var subStep in currentStep.SubSteps)
            {
                if (!subStep.Status.Equals(Status.Pass))
                {
                    if (subStep.Status.Equals(Status.Warning))
                    {
                        currentStep.ActualResult = subStep.ActualResult ?? "Warning";
                        currentStep.Status = Status.Warning;
                        if (!TestSession.CurrentTestCase.Status.Equals(Status.Fail))
                            TestSession.CurrentTestCase.Status = Status.Warning;
                    }
                    else
                    {
                        currentStep.ActualResult = subStep.ActualResult ?? "Error";
                        currentStep.Status = Status.Fail;
                        TestSession.CurrentTestCase.Status = Status.Fail;
                        //If any of the substeps is failed its screenshot will be displayed as the step’s screenshot  
                        currentStep.ScreenshotPath = subStep.ScreenshotPath;
                        break;
                    }
                }
            }
            if (string.IsNullOrEmpty(currentStep.ActualResult))
            {
                currentStep.ActualResult = "As expected";
            }
            //current step is saved to the list of steps of current test case   
            TestSession.CurrentTestCase.TestSteps.Add(currentStep);
        }

        /// <summary>   
        /// Creates new test step and makes it current   
        /// </summary>   
        /// <param name="customDescription">Description of test step. Overrides the value stored in the test case scope</param>      
        public static void RunStep(string customDescription = null)
        {
            SaveCurrentTestStep();
            var testCase = TestSession.CurrentTestCase;
            bool isTestScopeValid = testCase.CurrentStepIndex <= testCase.TestScope.Count - 1;

            testCase.CurrentStep = new TestStep
               {
                   Description = isTestScopeValid ? testCase.TestScope.ElementAt(testCase.CurrentStepIndex).Key : "No description provided for the test step",
                   Tooltip = customDescription ?? string.Empty,
                   ExpectedResult = isTestScopeValid ? testCase.TestScope.ElementAt(testCase.CurrentStepIndex).Value : string.Empty,
                   StartTime = DateTime.Now,
                   Status = Status.NoRun
               };
            testCase.CurrentStepIndex++;
            Log("Execute test step: " + TestSession.CurrentTestCase.CurrentStep.Description + ", " + TestSession.CurrentTestCase.CurrentStep.StartTime);
        }

        /// <summary>   
        /// Adds new passed substep to the list of substeps of current step       
        /// </summary>   
        /// <param name="description">Substep description</param>       
        /// <param name="expectedResult">Expected result</param>   
        /// <param name="screenshotPath">Path to related screenshot file</param>      
        public static void AddInfo(string description, string expectedResult = null, string screenshotPath = null)
        {
            TestSession.CurrentTestCase.CurrentStep.SubSteps.Add(new TestStep
                {
                    Description = description ?? string.Empty,
                    ExpectedResult = expectedResult ?? string.Empty,
                    ScreenshotPath = screenshotPath ?? string.Empty,
                    ActualResult = "As expected",
                    StartTime = DateTime.Now,
                    Status = Status.Pass
                });
            Log("Info: " + DateTime.Now + " - " + description + ", as expected");
        }
        /// <summary>   
        /// Adds new substep with warning status to the list of substeps of current step   
        /// </summary>   
        /// <param name="description">Substep description</param>       
        /// <param name="expectedResult">Expected result</param>   
        /// <param name="actualResult">Actual result</param>   
        /// <param name="screenshotPath">Path to related screenshot file</param>      
        public static void AddWarning(string description, string expectedResult = null, string actualResult = null, string screenshotPath = null)
        {
            TestSession.CurrentTestCase.CurrentStep.SubSteps.Add(new TestStep
                 {
                     Description = description ?? string.Empty,
                     ExpectedResult = expectedResult ?? string.Empty,
                     ScreenshotPath = screenshotPath ?? string.Empty,
                     ActualResult = actualResult ?? string.Empty,
                     StartTime = DateTime.Now,
                     Status = Status.Warning
                 });
            Log("Warning: " + DateTime.Now + " - " + description + (string.IsNullOrEmpty(expectedResult) ? string.Empty : ", expected result: " + expectedResult) + (string.IsNullOrEmpty(actualResult) ? string.Empty : ", actual result: " + actualResult)); 

        }

        /// <summary>   
        /// Adds new failed substep to the list of substeps of current step        
        /// </summary>   
        /// <param name="description">Substep description</param>       
        /// <param name="expectedResult">Expected result</param>   
        /// <param name="actualResult">Actual result</param>   
        /// <param 	name="screenshotPath">Path to related screenshot file</param>      
        public static void AddError(string description, string expectedResult = null, string actualResult = null, string screenshotPath = null)
        {
            TestSession.CurrentTestCase.CurrentStep.SubSteps.Add(new TestStep
                 {
                     Description = description ?? string.Empty,
                     ExpectedResult = expectedResult ?? string.Empty,
                     ScreenshotPath = screenshotPath ?? string.Empty,
                     ActualResult = actualResult ?? string.Empty,
                     StartTime = DateTime.Now,
                     Status = Status.Fail
                 });
            Log("Error: " + DateTime.Now + " - " + description + (string.IsNullOrEmpty(expectedResult) ? string.Empty : ", expected result: " + expectedResult) + (string.IsNullOrEmpty(actualResult) ? string.Empty : ", actual result: " + actualResult));
        }

        /// <summary>   
        /// Saves current test case to the list of test cases of the test session   
        /// </summary>         
        public static void FinishTestCase()
        {
            SaveCurrentTestStep();
            TestSession.CurrentTestCase.Duration = DateTime.Now - TestSession.CurrentTestCase.StartTime;
            TestSession.TestCases.Add(TestSession.CurrentTestCase);
        }

        /// <summary>   
        /// Saves data of certain test step as text in HTML format   
        /// </summary>   
        /// <param name="report">target HTML document to which the formatted data will be appended</param>   
        /// <param name="step">test step for save</param>   
        /// <param name="isSubstep">indicated is the step parent (main) step or a substep</param>   
        /// <param name="stepName">step name</param>   
        /// <returns>String: modified input parameter report with test step data appended</returns> 
        private static string ReportStep(string report, TestStep step, bool isSubstep = false, string stepName = null)
        {
            string color, status;
            string background = isSubstep ? "#F1F1F1" : "#DBFAFC";
            string display = isSubstep ? "display: none; " : string.Empty;
            string onclick = step.SubSteps.Count > 0 ? " onclick='toggle(\"" + step.Description + "\");'" : string.Empty;
            string className = string.IsNullOrEmpty(stepName) ? string.Empty : " class='" + stepName + "'";
            report += "<tr" + className + " style='" + display + "background: " + background + ";'" + onclick + ">" + "\r\n";

            if (step.Status == Status.Fail)
            {
                color = "red";
                status = "Fail";
            }
            else if (step.Status == Status.Warning)
            {
                color = "orange";
                status = "Warning";
            }
            else if (step.Status == Status.Pass)
            {
                color = "green";
                status = "Pass";
            }
            else
            {
                color = "grey";
                status = "No run";
            }

            report += "<td>" + step.StartTime + "</td>" + "\r\n";
            report += "<td style='color: " + color + "'>" + status + "</td>" + "\r\n";
            report += "<td title='" + step.Tooltip + "'>" + step.Description + "</td>" + "\r\n";
            report += "<td>" + step.ExpectedResult + "</td>" + "\r\n";
            report += "<td>" + step.ActualResult + "</td>" + "\r\n";
            report += "<td>" + "\r\n";
            if (!string.IsNullOrEmpty(step.ScreenshotPath))
                report += "<a href='" + step.ScreenshotPath + "'>Screenshot</a>" + "\r\n";
            report += "</td>" + "\r\n" + "</tr>" + "\r\n";

            return step.SubSteps.Aggregate(report, (current, substep) => ReportStep(current, substep, true, step.Description));
        }

        /// <summary>   
        /// Saves data of certain test case as text in HTML format   
        /// </summary>   
        /// <param name="report">target HTML document to which the formatted data will be appended</param>   
        /// <param name="testCase">test case for save</param>   
        /// <returns>String: modified input parameter report with test case data appended</returns>   
        private static string ReportTestCase(string report, TestCase testCase)
        {
            string color, status;
            const string background = "#F1F1F1";

            report += "<tr style='background: " + background + ";'>" + "\r\n";

            if (testCase.Status == Status.Fail)
            {
                color = "red";
                status = "Fail";
            }
            else if (testCase.Status == Status.Warning)
            {
                color = "orange";
                status = "Warning";
            }
            else
            {
                color = "green";
                status = "Pass";
                foreach (var step in testCase.TestSteps)
                {
                    if (step.Status.Equals(Status.NoRun))
                    {
                        color = "orange";
                        status = "Warning";
                        break;
                    }
                }
            }

            report += "<td>" + testCase.StartTime + "</td>" + "\r\n";
            report += "<td style='color: " + color + "'>" + status + "</td>" + "\r\n";
            report += "<td>" + "\r\n";
            report += "<a href='" + testCase.Name + ".html" + "'style='color: " + color + ";'>" + testCase.Name + "</a>" + "\r\n";
            report += "</td>" + "\r\n" + "</tr>" + "\r\n";

            return report;
        }

        /// <summary>   
        /// Saves data of certain test collection as text in HTML format   
        /// </summary>   
        /// <param name="report">target HTML document to which the formatted data will be appended</param>   
        /// <param name="collectionName">test collection name</param>   
        /// <returns>String: modified input parameter report with test collection data appended </returns>       
        private static string ReportTestSuite(string report, string collectionName)
        {
            string color;
            const string background = "#F1F1F1";

            report += "<tr style='background: " + background + ";'>" + "\r\n";

            var collection = from t in TestSession.TestCases where t.TestCollectionName == collectionName select t;

            var list = collection as IList<TestCase> ?? collection.ToList();

            var passed = from l in list where l.Status.Equals(Status.Pass) select l;
            var warning = from l in list where l.Status.Equals(Status.Warning) select l;
            var failed = from l in list where l.Status.Equals(Status.Fail) select l;

            var numberOfFailed = failed.Count();
            if ((double)numberOfFailed / list.Count >= 0.5)
                color = "red";
            else if ((double)numberOfFailed / list.Count >= 0.25)
                color = "orange";
            else if ((double)numberOfFailed / list.Count >= 0.1)
                color = "yellow";
            else
                color = "green";

            report += "<td>" + "\r\n";
            report += "<a href='" + collectionName + ".html" + "' style='color: " + color + ";'>" + collectionName + "</a>" + "\r\n";
            report += "</td>" + "\r\n";
            report += "<td>" + list.Count() + "</td>" + "\r\n";
            report += "<td>" + (passed.Count() + warning.Count()) + "</td>" + "\r\n";
            report += "<td>" + failed.Count() + "</td>" + "\r\n" + "</tr>" + "\r\n";

            return report;
        }

        /// <summary>
        /// Generates final summary report and saves it to local drive       
        /// </summary> 
        public static void SaveReport()
        {
            string reportBody, filePath;

            //distinct list of test collections' names in the report   
            List<string> testCollectionsInReport = TestSession.TestCases.GroupBy(x => x.TestCollectionName).Select(y => y.First().TestCollectionName).ToList();

            StreamWriter sw;

            foreach (var testCollectionName in testCollectionsInReport)
            {
                string name = testCollectionName;
                var testCases = from row in TestSession.TestCases where row.TestCollectionName == name select row;

                //creates document for every test case             
                var enumerable = testCases as IList<TestCase> ?? testCases.ToList();
                foreach (var testCase in enumerable)
                {
                    if ((testCase.TestScope.Count > testCase.TestSteps.Count) && !testCase.Status.Equals(Status.Fail))
                        testCase.Status = Status.Warning;

                    reportBody =
                        "<!DOCTYPE HTML PUBLIC ' -//W3C//DTD HTML 4.01//EN' 'http://www.w3.org/TR/html4/strict.dtd'>" + "\r\n" +
                        "<html>" + "\r\n" +
                        "<head>" + "\r\n" +
                        "<meta charset='utf-8' />" + "\r\n" +
                        "<meta content='text/html; charset=utf-8' />" + "\r\n" +
                        "<title>" + testCase.Name + " test result" + "</title>" + "\r\n" +
                        "<style>td{border: 0; padding: 0 1em; }</style>" + "\r\n" +
                        "</head>" + "\r\n";
                    reportBody +=
                        "<body>" + "\r\n" +
                        "<h2>" + testCase.Name + " test result - " + testCase.StartTime + "</h2>" + "\r\n" +
                        "<div style='border: solid #DBFAFC 2px;" + " padding: 1em; width:35%; border-radius:10px;'>" + "\r\n" +
                        "<h4>" + "Test steps:" + "</h4>" + "\r\n" +
                        "<ul>" + "\r\n";
                    reportBody = testCase.TestScope.Aggregate(reportBody, (current, scope) => current + ("<li>" + scope.Key + " - " + scope.Value + "</li>" + "\r\n"));
                    reportBody += "</ul>" + "\r\n" +
                        "<br/>" + "\r\n" +
                        "<h5>" + "Duration: " + testCase.Duration.ToString(@"hh\:mm\:ss") + "</h5>" + "\r\n" +
                        "</div>" + "\r\n" +
                        "<br/><br/>" + "\r\n" +
                        "<div style='padding: 1em;'>" + "\r\n" +
                        "<table>" + "\r\n" +
                        "<tr style='font-weight: bold; font-size:large; align: center;'>" + "\r\n" +
                        "<td>Time</td>" + "\r\n" +
                        "<td>Status</td>" + "\r\n" +
                        "<td>Description</td>" + "\r\n" +
                        "<td>Expected result</td>" + "\r\n" +
                        "<td>Actual result</td>" + "\r\n" +
                        "<td>Screenshot</td>" + "\r\n" +
                        "</tr>" + "\r\n";
                    reportBody = testCase.TestSteps.Aggregate(reportBody, (current, step) => ReportStep(current, step));
                    reportBody = (from scope in testCase.TestScope let stepDone = testCase.TestSteps.FirstOrDefault(x => x.Description == scope.Key) where stepDone == null select scope).Aggregate(reportBody, (current, scope) => ReportStep(current, new TestStep { Description = scope.Key, Status = Status.NoRun }));
                    reportBody +=
                        "</table>" + "\r\n" +
                        "</div>" + "\r\n" +
                        "<script type='text/javascript'>function toggle(id){var element = document.getElementsByClassName(id);i = element.length;while(i--){element[i].display =	\"none\";if	(element[i]) {var display = element[i].style.display;if (display == \"none\"){element[i].style.display = \"\";} else{element[i].style.display = \"none\";}}}}</script>" + "\r\n" +
                        "<script type='text/javascript'>function addImgAndReplaceA(){ var links = document.getElementsByTagName('a'); for(var i = 0; i < links.length; i++){ var a = document.createElement('a'); var pathToFolder = links[i].getAttribute('href').replace(/.+(?=\\ScreenShots)/, '.\\\\'); a.innerHTML = '<img 	src=\"'+ pathToFolder +'\" width=90 height=60>'; links[i].parentNode.replaceChild(a, links[i]); links[i].addEventListener('mouseover', showImg, 	false); links[i].addEventListener('mouseout', unshowImg, false); links[i].addEventListener('click', openImgInTab, false); } } function openImgInTab(e){ e = e || event; window.open(e.target.src,'_blank'); } function showImg(e){ e = e || event; var div = document.createElement('div'); div.id = 'screenshot'; div.style.cssText='display: block; position: fixed; right: 10%; top:10%; border: solid 3px black;'; div.innerHTML = '<img src=' + e.target.src + ' style=\" max-width:' + document.body.clientWidth * 0.8 + 'px; max-height:' + document.body.clientHeight * 0.8 + 'px;\">'; document.body.appendChild(div); } function unshowImg(){ var element = document.getElementById('screenshot'); element.parentNode.removeChild(element);} window.onload = addImgAndReplaceA; </script>" + "\r\n" +
                        "</body>" + "\r\n";
                    reportBody +=
                        "</html>" + "\r\n";
                    filePath = ReportFolderPath + "\\" + testCase.Name + ".html";
                    sw = File.CreateText(filePath);
                    sw.Write(reportBody);
                    sw.Close();
                }

                //creates document for every test suite   
                List<string> testCaseNames = enumerable.Select(x => x.Name).ToList();
                testCaseNames.Sort();
                var startTime = enumerable.Select(x => x.StartTime).Min();
                reportBody =
                    "<!DOCTYPE HTML PUBLIC ' -//W3C//DTD HTML 4.01//EN' 'http://www.w3.org/TR/html4/strict.dtd'>" + "\r\n" +
                    "<html>" + "\r\n" +
                    "<head>" + "\r\n" +
                    "<meta charset='utf-8' />" + "\r\n" +
                    "<meta content='text/html; charset=utf-8' />" + "\r\n" +
                    "<title>" + testCollectionName + " test result" + "</title>" + "\r\n" +
                    "<style>td{border: 0; padding: 0 1em; }</style>" + "\r\n" +
                    "</head>" + "\r\n";
                reportBody +=
                    "<body>" + "\r\n" +
                    "<h2>" + testCollectionName + " test result - " + startTime + "</h2>" + "\r\n" +
                    "<br/><br/>" + "\r\n" +
                    "<div style='padding: 1em;'>" + "\r\n" +
                    "<table>" + "\r\n" +
                    "<tr style='font-weight: bold; font-size:large; align: center;'>" + "\r\n" +
                    "<td>Time</td>" + "\r\n" +
                    "<td>Status</td>" + "\r\n" +
                    "<td>Test case</td>" + "\r\n" +
                    "</tr>" + "\r\n";
                reportBody = enumerable.Aggregate(reportBody, ReportTestCase);
                reportBody +=
                     "</table>" + "\r\n" +
                     "</div>" + "\r\n" +
                     "<script type='text/javascript'>function toggle(id){var element = document.getElementsByClassName(id);i = element.length;while(i--){element[i].display = \"none\";if (element[i]) {var display = element[i].style.display;if (display == \"none\"){element[i].style.display = \"\";} else{element[i].style.display = \"none\";}}}}</script>" + "\r\n" +
                     "</body>" + "\r\n" +
                     "</html>" + "\r\n";
                filePath = ReportFolderPath + "\\" + testCollectionName + ".html";
                sw = File.CreateText(filePath);
                sw.Write(reportBody);
                sw.Close();
            }

            reportBody = "<!DOCTYPE HTML PUBLIC ' -//W3C//DTD HTML 4.01//EN' 'http://www.w3.org/TR/html4/strict.dtd'>" + "\r\n" +
                "<html>" + "\r\n" +
                "<head>" + "\r\n" +
                "<meta charset='utf-8' />" + "\r\n" +
                "<meta content='text/html; charset=utf-8' />" + "\r\n" +
                "<title>" + TestSession.Name + " test result" + "</title>" + "\r\n" +
                "<style>td{border: 0; padding: 0 1em; }</style>" + "\r\n" +
                "<script type='text/javascript' src='https://www.google.com/jsapi'></script>" + "\r\n" +
                "<script type='text/javascript'>google.load('visualization', '1.0', {'packages':['corechart']});google.setOnLoadCallback(drawChart);function drawChart() {var data = new google.visualization.DataTable();data.addColumn('string', 	'Topping');data.addColumn('number', 'Slices');data.addRows([['', 0],['Failed', " + TestSession.TestCases.Count(x => x.Status == Status.Fail) + "],['Warnings', " + TestSession.TestCases.Count(x => x.Status == Status.Warning) + "],['Passed', " + TestSession.TestCases.Count(x => x.Status == Status.Pass) + "]]);var options = {'title':'Logger 1.0 test result - 5/5/2015 8:13:04 PM', 'width':400, 'height':300};var chart = new google.visualization.PieChart(document.getElementById('chart_div'));chart.draw(data, options);}</script>" + "\r\n" +
                "</head>" + "\r\n";
            reportBody +=
                "<body>" + "\r\n" +
                "<h2>" + TestSession.Name + " test result - " + TestSession.StartTime + "</h2>" + "\r\n" +
                "<br/><br/>" + "\r\n" +
                "<div id='chart_div'></div>" + "\r\n" +
                "<div style='padding: 1em;'>" + "\r\n" +
                "<table>" + "\r\n" +
                "<tr style='font-weight: bold; font-size:large; align: center;'>" + "\r\n" +
                "<td>Test collection</td>" + "\r\n" +
                "<td>Tests executed</td>" + "\r\n" +
                "<td>Tests passed</td>" + "\r\n" +
                "<td>Tests failed</td>" + "\r\n" +
                "</tr>" + "\r\n";
            reportBody = testCollectionsInReport.Aggregate(reportBody, ReportTestSuite);
            reportBody +=
                "</table>" + "\r\n" +
                "</div>" + "\r\n" +
                "<script type='text/javascript'>function toggle(id){var element = document.getElementsByClassName(id);i = element.length;while(i--){element[i].display = \"none\";if (element[i]) {var display = element[i].style.display;if (display == \"none\"){element[i].style.display = \"\";} else{element[i].style.display = \"none\";}}}}</script>";
            reportBody +=
                "</body>" + "\r\n" +
                "</html>" + "\r\n";
            filePath = ReportFolderPath + "\\index.html";
            sw = File.CreateText(filePath);
            sw.Write(reportBody);
            sw.Close();
        }

        /// <summary>   
        /// Compresses summary test report into zip archive   
        /// </summary>   
        /// <returns>String: path to created archive</returns>    
        private static string ZipReport()
        {
            string zipFilePath = ReportFolderPath + ".zip";
            try
            {
                ZipFile.CreateFromDirectory(ReportFolderPath, zipFilePath);
            }
            catch (Exception ex)
            {
                Report.AddWarning("An error has occurred during archivation of summary report. Error message: " + ex.Message);
                zipFilePath = string.Empty;
            }
            return zipFilePath;
        }

        //emailSubject - subject of the email   
        //emalTo - list of recipients’ addresses whom the letter must be sent to   
        //emailBody - content of the email, in my case it will be index.html file with summary result for all test collections (though you may write here whatever you want, for example “Hi, guys! Here are the tests run results. Happy analyzing! :))  
        //emailAttachmentPaths - optional parameter, list of paths to other attachments if you want to put them into the email   
        /// <summary>   
        /// Sends summary test report to specified e-mail addresses   
        /// </summary>   
        /// <param name="emailSubject">e-mail subject</param>   
        /// <param name="emailTo">List of addresses of recipients</param>   
        /// <param name="emailBody">e-mail body</param>   
        /// <param name="emailAttachmentPaths">List of paths to files to be attached to the e-mail</param>     
        public static void SendReportByEmail(string emailSubject, ICollection<string> emailTo, string emailBody, IEnumerable<string> emailAttachmentPaths = null)
        {
            //this is an address which the letter will be sent from. The address must be real and actual           
            const string senderEmail = "seleniumcsharp@gmail.com";
            const string senderPassword = "bucksRwelcome";
            if (emailTo == null || emailTo.Count == 0)
            {
                Report.AddWarning("Sending test result report by email", "email was sent to specified addresses", "Email address is not specified");
                return;
            }
            try
            {
                //referring to smtp client of the 	email provider (I use Google). You can know the host and port names 	from help documentatio n or support service. 	The sender's address 	and password are sent to smtp client here for setting 	the connection              
                var smtp = new SmtpClient("smtp.gmail.com", 587)
                             {
                                 Credentials = new NetworkCredential(senderEmail, senderPassword),
                                 EnableSsl = true
                             };
                //an instance of the email is created              
                var email = new MailMessage
                 {
                     From = new MailAddress(senderEmail),
                     Subject = emailSubject,
                     Body = emailBody,
                     IsBodyHtml = true,
                     SubjectEncoding = System.Text.Encoding.UTF8,
                     BodyEncoding = System.Text.Encoding.UTF8,
                 };
                //adding recipients’ addresses               
                foreach (var address in emailTo)

                    email.To.Add(new MailAddress(address));
                //adding attachments              
                if (emailAttachmentPaths != null)
                    foreach (var path in emailAttachmentPaths)
                        email.Attachments.Add(new Attachment(path));
                email.Attachments.Add(new Attachment(ZipReport()));
                smtp.Send(email);
            }
            catch (Exception ex)
            {
                Report.AddWarning("Sending test result report by email", "Email is sent to specified addresses", "An error has occurred while sending the email. Error message: " + ex.Message);
            }
        }

        /// <summary>   
        /// Writes string message to text file   
        /// </summary>   
        /// <param name="logMessage">message</param>   
        /// <see cref="https://msdn.microsoft.com/enus/library/3zc0w663(v =vs.110).aspx"/>         
        public static void Log(string logMessage)
        {
            if (doEventLogging)
                using (StreamWriter w = File.AppendText(logFilePath))
                {
                    w.WriteLine("{0}", logMessage);
                }
        }
    }
}
