using ExtRS.Portal.Models;
using ReportingServices.Api.Models;
using Microsoft.AspNetCore.Identity;
using Sonrai.ExtRS.Models;
using Sonrai.ExtRS;

namespace ExtRS.Portal.Models
{
    public class ChartsView : LayoutView
	{
        public HighChartsTimeSeriesModel HighChartsModel;
        public List<KeyValuePair<string, string>> HighChartsMarkupApproval;
        public List<KeyValuePair<string, string>> HighChartsMarkupDisapproval;

        public List<ChartView> Charts;
        private string _currentTab;

		public override string CurrentTab { get { return _currentTab; } set { _currentTab = value; } }
    }

    public class ChartView : LayoutView
    {
        public string Markup;
        private string _currentTab;
        public override string CurrentTab { get { return _currentTab; } set { _currentTab = value; } }
    }
}
