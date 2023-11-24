using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace Selenium.NetCore.Test
{
    public class workitem
    {        
        public int WIID { get; set; }
        public String Description { get; set; }
        public String WI_Type { get; set; }
        public String Status { get; set; }
        public String Date { get; set; }

        public workitem(int WIID, String Description, String WI_Type, String Status, String Date)
        {
            this.WIID = WIID;
            this.Description = Description;
            this.WI_Type = WI_Type;
            this.Status = Status;
            this.Date = Date;
        }
    }
}