using IO.Swagger.Model;
using Sonrai.ExtRS.Models;

namespace ExtRS.Portal.Models
{
    public class DataSourcesView : LayoutView
    {
        public string SelectedView = string.Empty;
        private string _currentTab;
        public override string CurrentTab { get { return _currentTab; } set { _currentTab = value; } }
        public List<DataSource> DataSources;
    }
}
