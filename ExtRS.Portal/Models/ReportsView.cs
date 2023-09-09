using ExtRS.Portal.Areas.Identity.Pages.Account;
using ExtRS.Portal.Models;
using IO.Swagger.Model;
using Microsoft.AspNetCore.Identity;
using Sonrai.ExtRS.Models;

namespace ExtRS.Portal.Models
{
    public class ReportsView : LayoutView
	{
        public List<Report> Reports;
        public Report SelectedReport;
        private string _currentTab;

		public override string CurrentTab { get { return _currentTab; } set { _currentTab = value; } }
    }
}
