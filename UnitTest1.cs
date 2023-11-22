using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using Xunit;
using Xunit.Abstractions;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium.Edge;
using System.Collections;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using OpenQA.Selenium.Internal;
using System.Diagnostics.Metrics;
using AngleSharp;
using System.Reflection;
using System.Linq;
//using OpenQA.Selenium.ke;

namespace Selenium.NetCore.Test
{
    public static class statclass {
        static TimeSpan interval = new TimeSpan(0, 0, 3);

        public static void WaitAndClick(this IWebDriver driver, IWebElement webelement)
        {
            var fluentWait = new WebDriverWait(driver, interval);
            IWebElement option = fluentWait.Until(webDriver =>
            {
                try
                {
                    if (webelement.Displayed)
                    {
                        return webelement;
                    }
                    
                }
                catch (NoSuchElementException) { return null; }
                catch (StaleElementReferenceException) { return null; }

                return null;

            });

            webelement.Click();
        }

        public static bool ElementExists(this IWebDriver driver, By condition, TimeSpan? timeSpan)
        {
            bool isElementPresent = false;
            if (timeSpan == null)
            {
                // default to 15 seconds if timespan parameter is not passed in
                timeSpan = TimeSpan.FromMilliseconds(5000);
            }
            var driverWait = new WebDriverWait(driver, (TimeSpan)timeSpan);
            driverWait.IgnoreExceptionTypes(typeof(WebDriverTimeoutException));
            isElementPresent = driverWait.Until(x => x.FindElements(condition).Any());
            return isElementPresent;
        }

    }

    public class ChromeTests : IDisposable
    {
        

        public static IWebDriver webDriver;
        private readonly ITestOutputHelper output;
        
        public ChromeTests(ITestOutputHelper output)
        {
            this.output = output;
            var directory = Directory.GetCurrentDirectory();
           
            var pathDrivers = "C:/Program Files/chromedriver - win64";

            ChromeOptions options = new ChromeOptions();
            options.AddExcludedArgument("enable-automation");
            options.AddArgument("disable-infobars");
            options.AddUserProfilePreference("credentials_enable_service", false);
            options.AddUserProfilePreference("profile.password_manager_enabled", false);

            new DriverManager().SetUpDriver(new ChromeConfig()); //
            
            webDriver = new ChromeDriver(options);

            Debug.Print("dirGG" + directory);
        }

        class MultiComparable : IComparable
        {
            private int Value { get; }

            public MultiComparable(int value)
            {
                Value = value;
            }

            public int CompareTo(object obj)
            {
                if (obj is int intObj)
                {
                    return Value.CompareTo(intObj);
                }
                else if (obj is MultiComparable multiObj)
                {
                    return Value.CompareTo(multiObj.Value);
                }

                throw new InvalidOperationException();
            }
        }

        //[Fact]
        //public void OLM_0_1_SetToBlocked()
        //{
        //    webDriver.Navigate().GoToUrl("https://olm.testcaselab.com/projects/OLM/test_runs/74883?sort_dir=asc");
        //    webDriver.Manage().Window.Maximize();

        //    Thread.Sleep(1000);

        //    webDriver.FindElement(By.XPath("/html/body/div[1]/div[3]/div/div/form/div[1]/div/input")).SendKeys("gluckgabor@gmail.com");
        //    webDriver.FindElement(By.Name("user[password]")).SendKeys("Bumblebee12");
        //    webDriver.FindElement(By.XPath("/html/body/div[1]/div[3]/div/div/form/div[4]/input")).Click();

        //    Thread.Sleep(3000);

        //    WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(100));
        //    var jse = (IJavaScriptExecutor)webDriver;


        //    //Scroll to bottom of leftmost div
        //    wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("/html/body/div[1]/div/test-run-page/div/div[2]/div[1]/div[1]/test-case-run-list/div[4]")));

        //    IWebElement elementToScrollTo = webDriver.FindElement(By.XPath("/html/body/div[1]/div/app-angular-root/app-test-run-page/div/div/as-split/as-split-area[1]/app-test-case-run-list/div/div[2]/app-test-cases-list/div/div[2]/div[37]"));

        //    jse.ExecuteScript("arguments[0].scrollIntoView(true)", elementToScrollTo);
        //    Thread.Sleep(3000);

        //    IWebElement elementToScrollTo2 = webDriver.FindElement(By.XPath("/html/body/div[1]/div/app-angular-root/app-test-run-page/div/div/as-split/as-split-area[1]/app-test-case-run-list/div/div[2]/app-test-cases-list/div/div[2]/div[49]"));
        //    jse.ExecuteScript("arguments[0].scrollIntoView(true)", elementToScrollTo2);
        //    Thread.Sleep(4000);

        //    IWebElement elementToScrollTo21 = webDriver.FindElement(By.XPath("/html/body/div[1]/div/app-angular-root/app-test-run-page/div/div/as-split/as-split-area[1]/app-test-case-run-list/div/div[2]/app-test-cases-list/div/div[2]/div[51]"));
        //    jse.ExecuteScript("arguments[0].scrollIntoView(true)", elementToScrollTo21);
        //    Thread.Sleep(4000);

        //    IWebElement elementToScrollTo22 = webDriver.FindElement(By.XPath("/html/body/div[1]/div/app-angular-root/app-test-run-page/div/div/as-split/as-split-area[1]/app-test-case-run-list/div/div[2]/app-test-cases-list/div/div[2]/div[53]"));
        //    jse.ExecuteScript("arguments[0].scrollIntoView(true)", elementToScrollTo22);
        //    Thread.Sleep(4000);

        //    IWebElement elementToScrollTo3 = webDriver.FindElement(By.XPath("/html/body/div[1]/div/app-angular-root/app-test-run-page/div/div/as-split/as-split-area[1]/app-test-case-run-list/div/div[2]/app-test-cases-list/div/div[2]/div[55]"));
        //    jse.ExecuteScript("arguments[0].scrollIntoView(true)", elementToScrollTo3);
        //    Thread.Sleep(3000);

        //    IWebElement elementToScrollTo4 = webDriver.FindElement(By.XPath("/html/body/div[1]/div/app-angular-root/app-test-run-page/div/div/as-split/as-split-area[1]/app-test-case-run-list/div/div[2]/app-test-cases-list/div/div[2]/div[85]"));
        //    jse.ExecuteScript("arguments[0].scrollIntoView(true)", elementToScrollTo4);
        //    Thread.Sleep(3000);

        //    IWebElement elementToScrollTo5 = webDriver.FindElement(By.XPath("/html/body/div[1]/div/app-angular-root/app-test-run-page/div/div/as-split/as-split-area[1]/app-test-case-run-list/div/div[2]/app-test-cases-list/div/div[2]/div[95]"));
        //    jse.ExecuteScript("arguments[0].scrollIntoView(true)", elementToScrollTo5);
        //    Thread.Sleep(3000);

        //    IWebElement elementToScrollTo6 = webDriver.FindElement(By.XPath("/html/body/div[1]/div/app-angular-root/app-test-run-page/div/div/as-split/as-split-area[1]/app-test-case-run-list/div/div[2]/app-test-cases-list/div/div[2]/div[100]"));
        //    jse.ExecuteScript("arguments[0].scrollIntoView(true)", elementToScrollTo6);
        //    Thread.Sleep(3000);

        //    IWebElement elementToScrollTo7 = webDriver.FindElement(By.XPath("/html/body/div[1]/div/app-angular-root/app-test-run-page/div/div/as-split/as-split-area[1]/app-test-case-run-list/div/div[2]/app-test-cases-list/div/div[2]/div[115]"));
        //    jse.ExecuteScript("arguments[0].scrollIntoView(true)", elementToScrollTo7);
        //    Thread.Sleep(3000);

        //    IWebElement elementToScrollTo71 = webDriver.FindElement(By.XPath("/html/body/div[1]/div/app-angular-root/app-test-run-page/div/div/as-split/as-split-area[1]/app-test-case-run-list/div/div[2]/app-test-cases-list/div/div[2]/div[123]"));
        //    jse.ExecuteScript("arguments[0].scrollIntoView(true)", elementToScrollTo71);
        //    Thread.Sleep(3000);

        //    //IWebElement elementToScrollTo8 = webDriver.FindElement(By.XPath("/html/body/div[1]/div/app-angular-root/app-test-run-page/div/div/as-split/as-split-area[1]/app-test-case-run-list/div/div[2]/app-test-cases-list/div/div[2]/div[130]"));
        //    //jse.ExecuteScript("arguments[0].scrollIntoView(true)", elementToScrollTo8);
        //    //Thread.Sleep(3000);

        //    wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[contains(text(), 'OLM-3')]")));
        //    Thread.Sleep(3000);
        //    Actions action = new Actions(webDriver);


        //    IList<IWebElement> AllTestsIds = webDriver.FindElements(By.XPath("/html/body/div[1]/div/app-angular-root/app-test-run-page/div/div/as-split/as-split-area[1]/app-test-case-run-list/div/div[2]/app-test-cases-list/div/div[2]/div[@*]/div[2]/div/span/test-case-label/span/a"));
        //    int[] TestIDsToBeSetToBlocked = {3, 6, 24, 31, 39, 57, 83, 84, 114, 115, 116, 123, 147, 498};

        //    void blockedSetter(int[] TestIDsToBeSetToBlocked, IList<IWebElement> AllTestsIds)
        //    {
        //        int index = 0;
        //        while (index < TestIDsToBeSetToBlocked.Length)
        //        {
        //            String currentTestIDtoBeSetToBlocked = String.Concat("OLM-", Convert.ToString(TestIDsToBeSetToBlocked[index]));

        //            foreach (var item in AllTestsIds)
        //            {
        //                String itemText = item.Text;
        //                if (itemText == currentTestIDtoBeSetToBlocked)
        //                {
        //                    IWebElement elementToScrollTo9 = webDriver.FindElement(By.LinkText(itemText));
        //                    jse.ExecuteScript("arguments[0].scrollIntoView(true)", elementToScrollTo9);

        //                    IWebElement toBeClicked = webDriver.FindElement(By.LinkText(itemText));
        //                    new Actions(webDriver)
        //                        .MoveToElement(toBeClicked, 180, 10)
        //                        .Click()
        //                        .Perform();


        //                    Thread.Sleep(1000);

        //                    currentTestIDtoBeSetToBlocked = "";

        //                    wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("/html/body/div[1]/div/test-run-page/div/div[2]/div[5]/div/test-result/div[3]/test-result-details/div[1]/statuses-on-test-result/div/div/button[2]")));

        //                    String expectedtext = String.Concat("OLM-", TestIDsToBeSetToBlocked[index]);
        //                    String actualTextInSecondPaneAbove = webDriver.FindElement(By.XPath("/html/body/div[1]/div/test-run-page/div/div[2]/div[3]/div/test-case-run/div[3]/div[1]/div/h2/test-case-label/span/a")).Text;

        //                    if (expectedtext == actualTextInSecondPaneAbove)
        //                    {
        //                        webDriver.FindElement(By.XPath("/html/body/div[1]/div/test-run-page/div/div[2]/div[5]/div/test-result/div[3]/test-result-details/div[1]/statuses-on-test-result/div/div/button[2]")).Click(); //set to blocked

        //                        //webDriver.FindElement(By.XPath("/html/body/div[1]/div/test-run-page/div/div[2]/div[5]/div/test-result/div[3]/test-result-details/div[1]/statuses-on-test-result/div/div/button[4]")).Click(); //set to not tested
        //                    }

        //                    Thread.Sleep(1000);
        //                    index++;
        //                    jse.ExecuteScript("arguments[0].scrollIntoView(true)", elementToScrollTo9);
        //                } 
        //            }    
        //        }
        //    }

        //    blockedSetter(TestIDsToBeSetToBlocked, AllTestsIds);

        //    Thread.Sleep(1000);

        //    int expected = 14;
        //    int actual = Convert.ToInt32(webDriver.FindElement(By.XPath("/html/body/div[1]/div/test-run-page/div/div[2]/div[1]/div[1]/test-case-run-list/div[1]/div[1]/div/div/test-run-progress-bar/div[2]/span[2]/a/span[2]")).Text.Substring(0,2));

        //    Assert.Equal(expected, actual);
        //}




        //static int[] workload = { 5, 3, 2, 1 }; 
        [Fact]
        public void OLM_0_2_AssignTestCases()
        {
            string[] testCaseLinesWithGroupNoReadFromFile = File.ReadAllLines("C:/Users/gluck/Documents/1 Structured Random Kft/Elszámolás/2 OLM/11edes felosztás jun 6 2023.csv");

            List<TestcaseIDline> testCaseIDlist = new List<TestcaseIDline>();

            for (int i = 1; i < testCaseLinesWithGroupNoReadFromFile.Length; i++)
            {
                testCaseIDlist.Add(new TestcaseIDline(testCaseLinesWithGroupNoReadFromFile[i]));
            }

            pairTestersToTestcaseIDs(testCaseIDlist); //randomolni párosítást megfelelő mennyiséget tesztelőnként
            prepareUIAndCallAssigner(testCaseIDlist); //beállítani a felületen a megfelelőtesztesetet a megfelelő tesztelőhöz
        }


        internal static void pairTestersToTestcaseIDs(List<TestcaseIDline> TestcaseIDlines)
        {
            int[] numbers = putNumbersFrom1to10inRandomOrder();

            //create a file to write assignment to, just in case
            TextWriter tw = new StreamWriter("C:/Users/gluck/OLM_random_assigment.txt");

            foreach (var TestcaseIDline in TestcaseIDlines) //végigmegyünk az összes teszteseten 
            {
                for (int i = 0; i < numbers.Length; i++) //végigmegyünk a 11 kategórián
                {
                    for (int j = 0; j < testers.Length; j++) //végigmegyünk a 4 tesztelőn
                    {
                        if (TestcaseIDline.groupNo == numbers[i])
                        {
                            TestcaseIDline.testerToBeAssignedOnFE = testers[i];
                        }
                    }
                }

                // write lines of text to the file
                tw.Write(TestcaseIDline.testerToBeAssignedOnFE);
                tw.Write(" - ");
                tw.Write(TestcaseIDline.testcaseID);
                tw.WriteLine();
            }

            // close the stream  
            tw.Close();
        }

        static String[] testers = {
                                    "Kovács Kata",
                                    "Kovács Kata",
                                    "Kovács Kata",
                                    "Kovács Kata",
                                    "Kovács Kata",
                                    "Kovács Kata",
                                    "Gáspár Dóra", //6
                                    "Gáspár Dóra",
                                    "Gáspár Dóra",
                                    "Gáspár Dóra",
                                    "Sólyom András" //4
        }; //hány darab tizenegyedet vigyen el pl. egy ember: 5-3-2-1




        internal static void prepareUIAndCallAssigner(List<TestcaseIDline> TestcaseIDlines)
        {
            //load testcaselab website
            webDriver.Navigate().GoToUrl("https://olm.testcaselab.com/projects/OLM/test_runs/75966?sort_by=created_at&sort_dir=asc");
            webDriver.Manage().Window.Maximize();

            Thread.Sleep(1000);

            //Login to testcaselab
            webDriver.FindElement(By.XPath("/html/body/div[1]/div[3]/div/div/form/div[1]/div/input")).SendKeys("gluckgabor@gmail.com");
            webDriver.FindElement(By.Name("user[password]")).SendKeys("Bumblebee12");
            Thread.Sleep(1500);

            WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(100));
            
            //Find and click Accept all
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[contains(text(), 'Accept all')]")));
            webDriver.FindElement(By.XPath("//*[contains(text(), 'Accept all')]")).Click();
            Thread.Sleep(500);

            //Click Submit
            webDriver.FindElement(By.XPath("/html/body/div[1]/div[3]/div/div/form/div[4]/input")).Click();

            Thread.Sleep(3000);

            var jse = (IJavaScriptExecutor)webDriver;

            //scrollLeftmostDivAndGatherXPathsToTestIDs
            (IList<IWebElement> AllTestsIds, List<TestcaseIDInLeftmostDiv> TestcaseIDInLeftmostDivlistInclXpaths) = scrollLeftmostDivAndGatherXPathsToTestIDs();

            //actualAssignmentOnUI
            actualAssignmentOnUI(TestcaseIDlines, AllTestsIds, TestcaseIDInLeftmostDivlistInclXpaths);



            (IList<IWebElement>, List<TestcaseIDInLeftmostDiv>) scrollLeftmostDivAndGatherXPathsToTestIDs()
            {
                //Scroll to bottom of leftmost div
                

                wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("/html/body/div[1]/div/app-angular-root/app-test-run-page/div/div/as-split/as-split-area[1]/app-test-case-run-list/div/div[2]/app-test-cases-list/div/div[2]")));

                IWebElement elementToScrollTo = webDriver.FindElement(By.XPath("/html/body/div[1]/div/app-angular-root/app-test-run-page/div/div/as-split/as-split-area[1]/app-test-case-run-list/div/div[2]/app-test-cases-list/div/div[2]/div[47]"));

                jse.ExecuteScript("arguments[0].scrollIntoView(true)", elementToScrollTo);
                Thread.Sleep(3000);

                IWebElement elementToScrollTo2 = webDriver.FindElement(By.XPath("/html/body/div[1]/div/app-angular-root/app-test-run-page/div/div/as-split/as-split-area[1]/app-test-case-run-list/div/div[2]/app-test-cases-list/div/div[2]/div[49]"));
                jse.ExecuteScript("arguments[0].scrollIntoView(true)", elementToScrollTo2);
                Thread.Sleep(4000);

                IWebElement elementToScrollTo21 = webDriver.FindElement(By.XPath("/html/body/div[1]/div/app-angular-root/app-test-run-page/div/div/as-split/as-split-area[1]/app-test-case-run-list/div/div[2]/app-test-cases-list/div/div[2]/div[51]"));
                jse.ExecuteScript("arguments[0].scrollIntoView(true)", elementToScrollTo21);
                Thread.Sleep(4000);

                IWebElement elementToScrollTo22 = webDriver.FindElement(By.XPath("/html/body/div[1]/div/app-angular-root/app-test-run-page/div/div/as-split/as-split-area[1]/app-test-case-run-list/div/div[2]/app-test-cases-list/div/div[2]/div[53]"));
                jse.ExecuteScript("arguments[0].scrollIntoView(true)", elementToScrollTo22);
                Thread.Sleep(4000);

                IWebElement elementToScrollTo3 = webDriver.FindElement(By.XPath("/html/body/div[1]/div/app-angular-root/app-test-run-page/div/div/as-split/as-split-area[1]/app-test-case-run-list/div/div[2]/app-test-cases-list/div/div[2]/div[55]"));
                jse.ExecuteScript("arguments[0].scrollIntoView(true)", elementToScrollTo3);
                Thread.Sleep(3000);

                IWebElement elementToScrollTo4 = webDriver.FindElement(By.XPath("/html/body/div[1]/div/app-angular-root/app-test-run-page/div/div/as-split/as-split-area[1]/app-test-case-run-list/div/div[2]/app-test-cases-list/div/div[2]/div[85]"));
                jse.ExecuteScript("arguments[0].scrollIntoView(true)", elementToScrollTo4);
                Thread.Sleep(3000);

                IWebElement elementToScrollTo5 = webDriver.FindElement(By.XPath("/html/body/div[1]/div/app-angular-root/app-test-run-page/div/div/as-split/as-split-area[1]/app-test-case-run-list/div/div[2]/app-test-cases-list/div/div[2]/div[95]"));
                jse.ExecuteScript("arguments[0].scrollIntoView(true)", elementToScrollTo5);
                Thread.Sleep(3000);

                IWebElement elementToScrollTo6 = webDriver.FindElement(By.XPath("/html/body/div[1]/div/app-angular-root/app-test-run-page/div/div/as-split/as-split-area[1]/app-test-case-run-list/div/div[2]/app-test-cases-list/div/div[2]/div[100]"));
                jse.ExecuteScript("arguments[0].scrollIntoView(true)", elementToScrollTo6);
                Thread.Sleep(3000);

                IWebElement elementToScrollTo7 = webDriver.FindElement(By.XPath("/html/body/div[1]/div/app-angular-root/app-test-run-page/div/div/as-split/as-split-area[1]/app-test-case-run-list/div/div[2]/app-test-cases-list/div/div[2]/div[115]"));
                jse.ExecuteScript("arguments[0].scrollIntoView(true)", elementToScrollTo7);
                Thread.Sleep(3000);

                IWebElement elementToScrollTo8 = webDriver.FindElement(By.XPath("/html/body/div[1]/div/app-angular-root/app-test-run-page/div/div/as-split/as-split-area[1]/app-test-case-run-list/div/div[2]/app-test-cases-list/div/div[2]/div[121]"));
                jse.ExecuteScript("arguments[0].scrollIntoView(true)", elementToScrollTo8);
                Thread.Sleep(3000);

                wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[contains(text(), 'OLM-1')]")));


                Thread.Sleep(3000);
                Actions action = new Actions(webDriver);

                //////////////////////////////////////////
                //////////////////////////////////////////
                ///TestcaseIDInLeftmostDiv testcaseID
                //gather testids like OLM-1 and so on

                IList<IWebElement> AllTestsIds = webDriver.FindElements(By.XPath("/html/body/div[1]/div/app-angular-root/app-test-run-page/div/div/as-split/as-split-area[1]/app-test-case-run-list/div/div[2]/app-test-cases-list/div/div[2]/div[@*]/div[1]/div/div[2]/span"));

                List<TestcaseIDInLeftmostDiv> TestcaseIDInLeftmostDivlist = new List<TestcaseIDInLeftmostDiv>();

                for (int i = 1; i < AllTestsIds.Count+1; i++)
                {
                    string testcaseID = AllTestsIds[i-1].Text;
                    string xpath = "/html/body/div[1]/div/app-angular-root/app-test-run-page/div/div/as-split/as-split-area[1]/app-test-case-run-list/div/div[2]/app-test-cases-list/div/div[2]/div[" + i + "]/div[1]/div/div[2]/span";
                    TestcaseIDInLeftmostDivlist.Add(new TestcaseIDInLeftmostDiv(testcaseID, xpath));
                }

                return (AllTestsIds, TestcaseIDInLeftmostDivlist);
            }

            String val;
            Boolean isPresent;
            void actualAssignmentOnUI(List<TestcaseIDline> TestcaseIDlines, IList<IWebElement> AllTestsIds, List<TestcaseIDInLeftmostDiv> TestcaseIDInLeftmostDivlist)
            {
                for (int i = 0; i < TestcaseIDlines.Count; i++)
                {
                    //0. eldönteni, h mit kattintunk, pl. OLM-5
                    String currentTestIDtoBeAssigned = String.Concat("OLM-", Convert.ToString(TestcaseIDlines[i].testcaseID));


                    //1. megtalálni balodalon
                    String itemText = currentTestIDtoBeAssigned;
                    IWebElement elementToScrollTo10 = webDriver.FindElement(By.LinkText(itemText));
                    jse.ExecuteScript("arguments[0].scrollIntoView(true)", elementToScrollTo10);

                    string XPath = "";
                    //2. rákkattintani xpath alapján
                    
                    //if (TestcaseIDInLeftmostDivlist.Find(p => p.testcaseID.Equals(itemText)).xpath != null)
                    //{
                    //XPath = TestcaseIDInLeftmostDivlist.Find(p => p.testcaseID.Equals(itemText)).xpath;
                    //}
                    //else
                    //{
                    //    continue;
                    //}

                    XPath = TestcaseIDInLeftmostDivlist.Find(p => p.testcaseID.Equals(itemText)).xpath;

                    WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(2));

                    wait.Until(d => webDriver.FindElement(By.XPath(XPath)).Displayed);
                    wait.IgnoreExceptionTypes(typeof(NoSuchElementException), typeof(ElementNotVisibleException));

                    try
                    {
                        webDriver.FindElement(By.XPath(XPath)).Click();
                        Thread.Sleep(500);
                    }
                    catch (ElementClickInterceptedException)
                    {
                        Thread.Sleep(500);
                        statclass.WaitAndClick(webDriver, webDriver.FindElement(By.XPath("/html/body/ngb-modal-window/div/div/app-test-run-full-screen/div/div[1]/app-test-run-case/div/div/div/div/div[1]/div[1]/button/span")));
                        Thread.Sleep(500);
                        try
                        {
                            webDriver.FindElement(By.XPath(XPath)).Click();
                        }
                        catch (ElementClickInterceptedException)
                        {
                            statclass.WaitAndClick(webDriver, webDriver.FindElement(By.XPath("/html/body/ngb-modal-window/div/div/app-test-run-full-screen/div/div[1]/app-test-run-case/div/div/div/div/div[1]/div[1]/button/span")));
                        }
                    }
                    

                    //3. full screen
                    try
                    {
                        Thread.Sleep(1000);
                        statclass.WaitAndClick(webDriver, webDriver.FindElement(By.XPath("/html/body/div[1]/div/app-angular-root/app-test-run-page/div/div/as-split/as-split-area[2]/app-test-run-case/div/div/div/div/div[1]/div[2]/div/div/span/span/button[1]")));
                    }
                    catch (NoSuchElementException)
                    {
                        Thread.Sleep(1000);
                        statclass.WaitAndClick(webDriver, webDriver.FindElement(By.XPath("/html/body/div[1]/div/app-angular-root/app-test-run-page/div/div/as-split/as-split-area[2]/app-test-run-case/div/div/div/div/div[1]/div[2]/div/div/span/span/button[1]/span")));
                        //IWebElement fullScreenIcon = webDriver.FindElement(By.XPath("/html/body/div[1]/div/app-angular-root/app-test-run-page/div/div/as-split/as-split-area[2]/app-test-run-case/div/div/div/div/div[1]/div[2]/div/div/span/span/button[1]/span"));
                        //fullScreenIcon.Click();                        
                    }
                    

                    //4. ellenőrizni, hogy az töltődött-e be, ha igaz tovább
                    try
                    {
                        statclass.WaitAndClick(webDriver, webDriver.FindElement(By.XPath("/html/body/div[1]/div/app-angular-root/app-test-run-page/div/div/as-split/as-split-area[2]/app-test-run-case/div/div/div/div/div[1]/div[2]/div/div/span/span/button[1]")));
                    }
                    catch (NoSuchElementException)
                    {
                        IWebElement testcaseIDinSecondColumn = webDriver.FindElement(By.XPath("/html/body/ngb-modal-window/div/div/app-test-run-full-screen/div/div[1]/app-test-run-case/div/div/div/div/div[1]/div[1]/h2/span/a"));

                        if (currentTestIDtoBeAssigned == testcaseIDinSecondColumn.Text)
                        {

                        //5. beállítani az embert
                        val = TestcaseIDlines[i].testerToBeAssignedOnFE;

                        //THE ACTUAL ASSIGNMENT IS DONE HERE, SOMETIMES ADVISIBLE TO COMMENT OUT NOT TO CHANGE ONGOING TESTEXEC ASSIGNMENT
                        webDriver.FindElement(By.XPath("/html/body/ngb-modal-window/div/div/app-test-run-full-screen/div/div[1]/app-test-run-case/div/div/div/div/div[2]/div[1]/div/app-test-run-case-details/div/div[1]/app-form-control[1]/div/div/div/app-edit-in-place/div/span")).Click();
                        Thread.Sleep(500);

                        //click again same to activate input dropdown to be able to send value
                        webDriver.FindElement(By.XPath("/html/body/ngb-modal-window/div/div/app-test-run-full-screen/div/div[1]/app-test-run-case/div/div/div/div/div[2]/div[1]/div/app-test-run-case-details/div/div[1]/app-form-control[1]/div/div/div/app-edit-in-place/div/div/div/ng-select/div/div/div[3]/input")).Click();
                        Thread.Sleep(500);

                        //sendkeys
                        webDriver.FindElement(By.XPath("/html/body/ngb-modal-window/div/div/app-test-run-full-screen/div/div[1]/app-test-run-case/div/div/div/div/div[2]/div[1]/div/app-test-run-case-details/div/div[1]/app-form-control[1]/div/div/div/app-edit-in-place/div/div/div/ng-select/div/div/div[3]/input")).SendKeys(val);

                        Thread.Sleep(500);

                            //click title to leave input dropdown field and activate toast message
                            webDriver.FindElement(By.XPath("/html/body/ngb-modal-window/div/div/app-test-run-full-screen/div/div[1]/app-test-run-case/div/div/div/div/div[2]/div[1]/div/app-test-run-case-details/div/div[1]/app-form-control[1]/div/div/div/app-edit-in-place/div/div/div/ng-select/div/div/div[3]/input")).SendKeys(Keys.Enter);

                        Thread.Sleep(500);

                            webDriver.FindElement(By.XPath("/html/body/ngb-modal-window/div/div/app-test-run-full-screen/div/div[1]/app-test-run-case/div/div/div/div/div[2]/div[1]/div/app-test-run-case-details/div/div[1]/app-form-control[1]/div/div/p")).Click();

                            //click toast message

                            var isElementPresent = webDriver.ElementExists(By.XPath("/html/body/div[4]/div"), TimeSpan.FromSeconds(90.00));
                            if (isElementPresent)
                            {
                                statclass.WaitAndClick(webDriver, webDriver.FindElement(By.XPath("/html/body/div[4]/div")));
                            }
                        
                            //wait.Until(d => webDriver.FindElement(By.XPath("/html/body/div[4]/div")).Displayed);
                            //webDriver.FindElement(By.XPath("/html/body/div[4]/div")).Click();

                        Thread.Sleep(500);

                            //6. kikapcsolni a fullscreent
                            isElementPresent = webDriver.ElementExists(By.XPath("/html/body/ngb-modal-window/div/div/app-test-run-full-screen/div/div[1]/app-test-run-case/div/div/div/div/div[1]/div[1]/button/span"), TimeSpan.FromSeconds(90.00));
                            if (isElementPresent)
                            {
                                statclass.WaitAndClick(webDriver, webDriver.FindElement(By.XPath("/html/body/ngb-modal-window/div/div/app-test-run-full-screen/div/div[1]/app-test-run-case/div/div/div/div/div[1]/div[1]/button/span")));
                            }
                        
                        //wait.Until(d => webDriver.FindElement(By.XPath("/html/body/ngb-modal-window/div/div/app-test-run-full-screen/div/div[1]/app-test-run-case/div/div/div/div/div[1]/div[1]/button/span")).Displayed);

                            //    isPresent = webDriver.FindElement(By.XPath("/html/body/ngb-modal-window/div/div/app-test-run-full-screen/div/div[1]/app-test-run-case/div/div/div/div/div[1]/div[1]/button/span")).Displayed;
                            //    while (isPresent)
                            //{
                            //    webDriver.FindElement(By.XPath("/html/body/ngb-modal-window/div/div/app-test-run-full-screen/div/div[1]/app-test-run-case/div/div/div/div/div[1]/div[1]/button/span")).Click();
                            //    Thread.Sleep(500);

                            //        try
                            //        {
                            //            isPresent = webDriver.FindElement(By.XPath("/html/body/ngb-modal-window/div/div/app-test-run-full-screen/div/div[1]/app-test-run-case/div/div/div/div/div[1]/div[1]/button/span")).Displayed;
                            //        }
                            //        catch (Exception)
                            //        {

                            //            continue;
                            //        }

                            //}
                        }
                        
                    }
                    catch (ElementClickInterceptedException)
                    {
                        IWebElement testcaseIDinSecondColumn = webDriver.FindElement(By.XPath("/html/body/ngb-modal-window/div/div/app-test-run-full-screen/div/div[1]/app-test-run-case/div/div/div/div/div[1]/div[1]/h2/span/a"));

                        if (currentTestIDtoBeAssigned == testcaseIDinSecondColumn.Text)
                        {

                            //5. beállítani az embert
                            String val = TestcaseIDlines[i].testerToBeAssignedOnFE;

                            //THE ACTUAL ASSIGNMENT IS DONE HERE, SOMETIMES ADVISIBLE TO COMMENT OUT NOT TO CHANGE ONGOING TESTEXEC ASSIGNMENT
                            webDriver.FindElement(By.XPath("/html/body/ngb-modal-window/div/div/app-test-run-full-screen/div/div[1]/app-test-run-case/div/div/div/div/div[2]/div[1]/div/app-test-run-case-details/div/div[1]/app-form-control[1]/div/div/div/app-edit-in-place/div/span")).Click();
                            Thread.Sleep(500);

                            //click again same to activate input dropdown to be able to send value
                            webDriver.FindElement(By.XPath("/html/body/ngb-modal-window/div/div/app-test-run-full-screen/div/div[1]/app-test-run-case/div/div/div/div/div[2]/div[1]/div/app-test-run-case-details/div/div[1]/app-form-control[1]/div/div/div/app-edit-in-place/div/div/div/ng-select/div/div/div[3]/input")).Click();
                            Thread.Sleep(500);

                            //sendkeys
                            webDriver.FindElement(By.XPath("/html/body/ngb-modal-window/div/div/app-test-run-full-screen/div/div[1]/app-test-run-case/div/div/div/div/div[2]/div[1]/div/app-test-run-case-details/div/div[1]/app-form-control[1]/div/div/div/app-edit-in-place/div/div/div/ng-select/div/div/div[3]/input")).SendKeys(val);

                            Thread.Sleep(500);

                            //click title to leave input dropdown field and activate toast message
                            webDriver.FindElement(By.XPath("/html/body/ngb-modal-window/div/div/app-test-run-full-screen/div/div[1]/app-test-run-case/div/div/div/div/div[2]/div[1]/div/app-test-run-case-details/div/div[1]/app-form-control[1]/div/div/div/app-edit-in-place/div/div/div/ng-select/div/div/div[3]/input")).SendKeys(Keys.Enter);

                            Thread.Sleep(500);

                            webDriver.FindElement(By.XPath("/html/body/ngb-modal-window/div/div/app-test-run-full-screen/div/div[1]/app-test-run-case/div/div/div/div/div[2]/div[1]/div/app-test-run-case-details/div/div[1]/app-form-control[1]/div/div/p")).Click();

                            //click toast message

                            var isElementPresent = webDriver.ElementExists(By.XPath("/html/body/div[4]/div"), TimeSpan.FromSeconds(90.00));
                            if (isElementPresent)
                            {
                                statclass.WaitAndClick(webDriver, webDriver.FindElement(By.XPath("/html/body/div[4]/div")));
                            }

                            //wait.Until(d => webDriver.FindElement(By.XPath("/html/body/div[4]/div")).Displayed);
                            //webDriver.FindElement(By.XPath("/html/body/div[4]/div")).Click();

                            Thread.Sleep(500);

                            //6. kikapcsolni a fullscreent
                            isElementPresent = webDriver.ElementExists(By.XPath("/html/body/ngb-modal-window/div/div/app-test-run-full-screen/div/div[1]/app-test-run-case/div/div/div/div/div[1]/div[1]/button/span"), TimeSpan.FromSeconds(90.00));
                            if (isElementPresent)
                            {
                                statclass.WaitAndClick(webDriver, webDriver.FindElement(By.XPath("/html/body/ngb-modal-window/div/div/app-test-run-full-screen/div/div[1]/app-test-run-case/div/div/div/div/div[1]/div[1]/button/span")));
                            }

                            //wait.Until(d => webDriver.FindElement(By.XPath("/html/body/ngb-modal-window/div/div/app-test-run-full-screen/div/div[1]/app-test-run-case/div/div/div/div/div[1]/div[1]/button/span")).Displayed);

                            //    isPresent = webDriver.FindElement(By.XPath("/html/body/ngb-modal-window/div/div/app-test-run-full-screen/div/div[1]/app-test-run-case/div/div/div/div/div[1]/div[1]/button/span")).Displayed;
                            //    while (isPresent)
                            //{
                            //    webDriver.FindElement(By.XPath("/html/body/ngb-modal-window/div/div/app-test-run-full-screen/div/div[1]/app-test-run-case/div/div/div/div/div[1]/div[1]/button/span")).Click();
                            //    Thread.Sleep(500);

                            //        try
                            //        {
                            //            isPresent = webDriver.FindElement(By.XPath("/html/body/ngb-modal-window/div/div/app-test-run-full-screen/div/div[1]/app-test-run-case/div/div/div/div/div[1]/div[1]/button/span")).Displayed;
                            //        }
                            //        catch (Exception)
                            //        {

                            //            continue;
                            //        }

                            //}
                        }

                    }
                    
                }
            }
        }

        

        internal static int[] putNumbersFrom1to10inRandomOrder()
        {

            // Create an array with numbers from 1 to 10
            int[] numbers = new int[11];
            for (int i = 0; i < numbers.Length; i++)
            {
                numbers[i] = i + 1;
            }

            // Shuffle the array using Fisher-Yates shuffle algorithm
            Random random = new Random();
            for (int i = numbers.Length - 1; i > 0; i--)
            {
                int j = random.Next(i + 1);
                int temp = numbers[i];
                numbers[i] = numbers[j];
                numbers[j] = temp;
            }

            return numbers;
        }

        













        [Theory]
        [InlineData("gluckgabor@gmail.com", "WnrsZBPXMhFw4z6s")]
        public void OLM_2_Bejelentkezes_Hun(String login_email, String login_password)
        {

            webDriver.Navigate().GoToUrl("https://olm-test.devwing.hu/login");
            webDriver.Manage().Window.Maximize();

            webDriver.FindElement(By.XPath("/html/body/div/header/div[2]/form/button[1]")).Click();
            webDriver.FindElement(By.Name("login_email")).SendKeys(login_email);
            webDriver.FindElement(By.Name("login_password")).SendKeys(login_password);

            WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(10));

            webDriver.FindElement(By.CssSelector("#submit > input[type=submit]")).Click();
            IWebElement SearchResult = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.XPath("/html/body/div[2]/div[2]/div/div/div[1]/h3")));

            //Thread.Sleep(5000);

            var expected = "Köszöntjük Rendszerünkben Gábor Glück!";
            String actual = webDriver.FindElement(By.XPath("/html/body/div[2]/div[2]/div/div/div[1]/h3")).Text;

            Assert.Equal(expected, actual);
        }

        public void Dispose()
        {
            webDriver.Dispose();
        }

        [Theory]
        [InlineData("xy@gmail.com", "DummyPasswordFails1", "HUN")]
        [InlineData("xy@gmail.com", "DummyPasswordFails1", "ENG")]
        [InlineData("xy@gmail.com", "DummyPasswordFails1", "GER")]
        public void OLM_2_Bejelentkezes_negativetest1(String login_email, String login_password, String lang)
        {

            webDriver.Navigate().GoToUrl("https://olm-test.devwing.hu/login");
            webDriver.Manage().Window.Maximize();

            if (lang == "HUN")
            {
                webDriver.FindElement(By.XPath("/html/body/div/header/div[2]/form/button[1]")).Click();

                webDriver.FindElement(By.Name("login_email")).SendKeys(login_email);
                webDriver.FindElement(By.Name("login_password")).SendKeys(login_password);

                webDriver.FindElement(By.CssSelector("#submit > input[type=submit]")).Click();

                var expected = "Hibás adat!";
                String actual = webDriver.FindElement(By.XPath("/html/body/div/section/form/fieldset[1]/div[3]/span")).Text;
                Assert.Equal(expected, actual);
            }
            else if (lang == "ENG")
            {
                webDriver.FindElement(By.XPath("/html/body/div/header/div[2]/form/button[2]")).Click();

                webDriver.FindElement(By.Name("login_email")).SendKeys(login_email);
                webDriver.FindElement(By.Name("login_password")).SendKeys(login_password);

                webDriver.FindElement(By.CssSelector("#submit > input[type=submit]")).Click();

                var expected = "Incorrect login data!";
                String actual = webDriver.FindElement(By.XPath("/html/body/div/section/form/fieldset[1]/div[3]/span")).Text;
                Assert.Equal(expected, actual);
            }
            else if (lang == "GER")
            {
                webDriver.FindElement(By.XPath("/html/body/div/header/div[2]/form/button[4]")).Click();

                webDriver.FindElement(By.Name("login_email")).SendKeys(login_email);
                webDriver.FindElement(By.Name("login_password")).SendKeys(login_password);

                webDriver.FindElement(By.CssSelector("#submit > input[type=submit]")).Click();

                var expected = "Falschen Daten!";
                String actual = webDriver.FindElement(By.XPath("/html/body/div/section/form/fieldset[1]/div[3]/span")).Text;
                Assert.Equal(expected, actual);
            }
        }















        [Fact]
        public void OLM_1_Regisztracio_Hun()
        {
            
        String company_name = "SR_Kft" + (DateTime.Now.ToString("yyyy.MM.dd")); //SR_Kft_2023.01.10
            output.WriteLine(company_name);

            Debug.Print(company_name);
            String company_tax_number = Convert.ToString(RandomDigits(11));
            String user_title = "I";
            String last_name = "Glück";
            String first_name = "Gábor";
            String email = "gluckgabor+" + Convert.ToString(RandomDigits(10)) + "@gmail.com";
            output.WriteLine(email);

            TextWriter tw = new StreamWriter("C:/Users/gluck/OLM_Registered_Company.txt");

            // write lines of text to the file
            tw.WriteLine(company_name);
            tw.WriteLine(email);

            // close the stream     
            tw.Close();

            String phone_number = "+36203271897";
            String number_of_employees = "3";
            String password = "Asdf1234";  
            String password_2 = "Asdf1234";

            webDriver.Navigate().GoToUrl("https://olm-test.devwing.hu/login");
            webDriver.Manage().Window.Maximize();

            webDriver.FindElement(By.XPath("/html/body/div/section/h2/a")).Click();

            webDriver.FindElement(By.Name("company_name")).SendKeys(company_name);
            webDriver.FindElement(By.Name("company_tax_number")).SendKeys(company_tax_number);
            webDriver.FindElement(By.Name("user_title")).SendKeys(user_title);
            webDriver.FindElement(By.Name("last_name")).SendKeys(last_name);
            webDriver.FindElement(By.Name("first_name")).SendKeys(first_name);
            webDriver.FindElement(By.Name("email")).SendKeys(email);
            webDriver.FindElement(By.Name("phone_number")).SendKeys(phone_number);
            webDriver.FindElement(By.Name("number_of_employees")).SendKeys(number_of_employees);
            webDriver.FindElement(By.Name("password")).SendKeys(password);
            webDriver.FindElement(By.Name("password_2")).SendKeys(password_2);
            webDriver.FindElement(By.XPath("/html/body/div/section/form/fieldset[10]/a")).Click();

            Thread.Sleep(2000);

            webDriver.FindElement(By.XPath("/html/body/div/section/form/fieldset[11]/div/input")).Click();
            
            var expected = "Köszönjük, hogy regisztrált Rendszerünkbe!";
            String actual = webDriver.FindElement(By.XPath("/html/body/div/div[2]/section/div/p[1]")).Text;

            Assert.Equal(expected, actual);
        }

        
        //[Theory]
        //[InlineData("gluckgabor@gmail.com", "WnrsZBPXMhFw4z6s")]
        //public void OLM_5_IE_kompatibilitas(String login_email, String login_password)
        //{
        //    //C:\Program Files (x86)\Microsoft\Edge\Application\114.0.1823.41 under this folder the BHO I renamed and finally now I again not get redirected to Edge.

        //    var driverService = EdgeDriverService.CreateDefaultService(@"C:\Users\gluck\.nuget\packages\microsoft.edge.seleniumtools\3.141.3");

        //    var options = new InternetExplorerOptions();

        //    // Set the desired capabilities
        //    options.IgnoreZoomLevel = true;
        //    options.IntroduceInstabilityByIgnoringProtectedModeSettings = true;
        //    //Create an instance of InternetExplorerDriver
        //    //IEwebDriver = new InternetExplorerDriver(driverService, options);
        //    IEdriver = new InternetExplorerDriver(options);

        //    IEdriver.Manage().Window.Maximize();

        //    IEdriver.Navigate().GoToUrl("https://olm-test.devwing.hu/login");
            

        //    TimeSpan ts = TimeSpan.FromSeconds(10);

        //    WebDriverWait Test_Wait = new WebDriverWait(IEdriver, ts);

        //    //Test_Wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.Name("login_email")));
        //    Thread.Sleep(3000);

        //    IEdriver.FindElement(By.XPath("/html/body/div/section/form/fieldset[1]/input")).SendKeys(login_email);
        //    IEdriver.FindElement(By.XPath("/html/body/div/section/form/fieldset[2]/input")).SendKeys(login_password);
        //    IEdriver.FindElement(By.Id("submit")).Click();

        //    var expected = "Nem támogatott böngésző!";
        //    String actual = webDriver.FindElement(By.ClassName("alert alert-danger w-100 position-fixed top-0 justify-content-center align-items-center p-0 border-0")).Text;

        //    Assert.Equal(expected, actual);
        //}

        [Fact]
        public void OLM_7_Szuro()
        {

        }

        public string RandomDigits(int length)
        {
            var random = new Random();
            string s = string.Empty;
            for (int i = 0; i < length; i++)
                s = String.Concat(s, random.Next(10).ToString());
            return s;
        }
    }
}
