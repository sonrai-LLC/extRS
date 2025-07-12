using System.ComponentModel;

namespace ExtRS.Portal.Models
{
    public class SettingsView : LayoutView
    {
        private string? _currentTab;
        public override string CurrentTab { get { return _currentTab!; } set { _currentTab = value; } }

        public Guid UserID { get; set; }

        public string? DefaultEmailAddress { get; set; }
    }

    public class UserView
    {
        public int Id { get; set; }

        public string? Type { get; set; }

        public string? UserName { get; set; }

        public string? Email { get; set; }
    }

    public class ConfigurationInfoView
    {
        public Guid ConfigInfoID { get; set; }

        public string? Name { get; set; }

        public string? Value { get; set; }
    }
}
