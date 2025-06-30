using Microsoft.AspNetCore.Components;
using System.ComponentModel;

namespace ExtRS.Portal.Models
{
    public class AdminView : LayoutView
	{
        private string? _currentTab;

        public override string CurrentTab { get { return _currentTab!; } set { _currentTab = value; } }

        public Guid AdminID { get; set; }

        public string? AdminUser { get; set; }

        public int AuthType { get; set; }

        public String? UserName { get; set; }
    }
}
