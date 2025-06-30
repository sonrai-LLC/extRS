using ExtRS.Portal.Models;
using ReportingServices.Api.Models;
using Microsoft.AspNetCore.Identity;
using Sonrai.ExtRS.Models;

namespace ExtRS.Portal.Models
{
    public class InteractivesView : LayoutView
	{
        public List<InteractiveView>? Interactives;
        private string? _currentTab;

		public override string CurrentTab { get { return _currentTab!; } set { _currentTab = value; } }
    }

    public class InteractiveView : LayoutView
    {
        private string? _currentTab;

        public override string CurrentTab { get { return _currentTab!; } set { _currentTab = value; } }
    }
}
