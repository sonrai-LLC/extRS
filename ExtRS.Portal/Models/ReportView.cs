using ExtRS.Portal.Models;
using IO.Swagger.Model;
using Sonrai.ExtRS.Models;

namespace ExtRS.Portal.Models
{
    public class ReportView : LayoutView
    {
        public Report Report;
        private string _currentTab;
        public override string CurrentTab { get { return _currentTab; } set { _currentTab = value; } }
    }
}
