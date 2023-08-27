using System.ComponentModel;

namespace ExtRS.Portal.Models
{
    public class UserView : LayoutView
    {
        private string _currentTab;
        public override string CurrentTab { get { return _currentTab; } set { _currentTab = value; } }

        public Guid UserID { get; set; }

        public string DefaultEmailAddress { get; set; }
    }
}
