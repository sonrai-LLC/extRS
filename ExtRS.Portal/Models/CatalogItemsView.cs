using Microsoft.AspNetCore.Components;

namespace ExtRS.Portal.Models
{
    public class CatalogItemsView : LayoutView
    {
        private string? _currentTab;
        public override string CurrentTab { get { return _currentTab!; } set { _currentTab = value; } }
    }
}
