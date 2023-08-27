using Microsoft.AspNetCore.Components;

namespace ExtRS.Portal.Models
{
    public class CatalogItemView : LayoutView
    {
        public string SelectedView = string.Empty;
        private string _currentTab;
        public override string CurrentTab { get { return _currentTab; } set { _currentTab = value; } }
    }
}
