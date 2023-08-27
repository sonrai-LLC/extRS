using System.ComponentModel;

namespace ExtRS.Portal.Models
{
    public class StatsView : LayoutView
    {
        private string _currentTab;
        public override string CurrentTab { get { return _currentTab; } set { _currentTab = value; } }

        public string first_name { get; set; }

        public string last_name { get; set; }

        public string position { get; set; }

        public string email { get; set; }

        public string office { get; set; }

        public string extn { get; set; }

        public decimal age { get; set; }

        public decimal salary { get; set; }

        public string start_date { get; set; }
    }
}
