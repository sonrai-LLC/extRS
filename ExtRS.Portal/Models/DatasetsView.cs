using ReportingServices.Api.Models;

namespace ExtRS.Portal.Models
{
    public class DatasetsView : LayoutView
    {
        private string _currentTab;
        public string SelectedView = string.Empty;
        public string ReportServerName;
        public List<DataSet> Datasets;
        public override string CurrentTab { get { return _currentTab; } set { _currentTab = value; } }
    }
}
