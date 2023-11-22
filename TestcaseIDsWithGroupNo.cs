using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace Selenium.NetCore.Test
{
    class TestcaseIDline
    {
        
        public int groupNo { get; set; }
        public int testcaseID { get; set; }
        public String testerToBeAssignedOnFE { get; set; }
        public String testcaseID_and_testerToBeAssignedOnFE { get; set; }

        public TestcaseIDline(string testcaseIDline)
        {
            string[] testcaseIDlineSplitted = testcaseIDline.Split(";");
        
            testcaseID = Convert.ToInt32(testcaseIDlineSplitted[1]);
            groupNo = Convert.ToInt32(testcaseIDlineSplitted[5]);
            testcaseID_and_testerToBeAssignedOnFE = string.Concat(testcaseID," - ",testerToBeAssignedOnFE);
        }
    }

    class TestcaseIDInLeftmostDiv
    {
        public string testcaseID { get; set; }
        public string xpath { get; set; }

        public TestcaseIDInLeftmostDiv(string testcaseID, string xpath)
        {
            this.testcaseID = testcaseID;
            this.xpath = xpath;
        }
    }
}