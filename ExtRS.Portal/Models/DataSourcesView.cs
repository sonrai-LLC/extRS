using ReportingServices.Api.Models;
using Sonrai.ExtRS.Models;

namespace ExtRS.Portal.Models
{
    public class DataSourcesView : LayoutView
    {
        private string _currentTab;
        public string SelectedDataSource;
        public string SelectedView = string.Empty;
        public string ReportServerName;
        public override string CurrentTab { get { return _currentTab; } set { _currentTab = value; } }
        public List<DataSource> DataSources;
    }

    public class DataSourceView : LayoutView
    {
        private string _currentTab;
        public DataSource SelectedDataSource;
        public string ReportServerName;

        public override string CurrentTab { get { return _currentTab; } set { _currentTab = value; } }
    }
}
