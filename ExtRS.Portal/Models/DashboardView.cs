using ReportingServices.Api.Models;
using Microsoft.AspNetCore.Components;
using Sonrai.ExtRS.Models;

namespace ExtRS.Portal.Models
{
    public class DashboardView : LayoutView
    {
        private string _currentTab;
        public override string CurrentTab { get { return _currentTab; } set { _currentTab = value; } }
        public Report Report { get; set; }
    }
}
