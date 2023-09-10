﻿using ExtRS.Portal.Areas.Identity.Pages.Account;
using ExtRS.Portal.Models;
using ReportingServices.Api.Models;
using Microsoft.AspNetCore.Identity;
using Sonrai.ExtRS.Models;

namespace ExtRS.Portal.Models
{
    public class ReportsView : LayoutView
	{
        public List<Report> Reports;
        public Report SelectedReport;
        public string ReportServerName;
        private string _currentTab;

		public override string CurrentTab { get { return _currentTab; } set { _currentTab = value; } }
    }

    public class ReportView : LayoutView
    {
        public Report SelectedReport;
        public string ReportServerName;
        private string _currentTab;

        public override string CurrentTab { get { return _currentTab; } set { _currentTab = value; } }
    }
}
