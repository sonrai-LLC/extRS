using IO.Swagger.Model;

namespace ExtRS.Portal.Models
{
    public class DatasetsView : LayoutView
    {
       public string SelectedView = string.Empty;
        private string _currentTab;
        public List<DataSet> Datasets;
        public override string CurrentTab { get { return _currentTab; } set { _currentTab = value; } }
    }
}
