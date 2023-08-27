namespace ExtRS.Portal.Models
{
    public class DatasetView : LayoutView
    {
       public string SelectedView = string.Empty;
        private string _currentTab;
        public override string CurrentTab { get { return _currentTab; } set { _currentTab = value; } }
    }
}
