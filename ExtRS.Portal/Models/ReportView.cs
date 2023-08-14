using ExtRS.Portal.Models;
using Sonrai.ExtRS.Models;

namespace ExtRS.Portal.Models
{
    public class ReportView : LayoutModel
    {
        public Report Report;
        private string _currentTab;
        public override string CurrentTab { get { return _currentTab; } set { _currentTab = value; } }
    }
}
