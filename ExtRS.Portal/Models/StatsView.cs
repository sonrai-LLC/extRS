using ReportingServices.Api.Models;
using System.ComponentModel;
using System.Numerics;

namespace ExtRS.Portal.Models
{
    public class StatsView : LayoutView
    {
        private string? _currentTab;
        public override string CurrentTab { get { return _currentTab!; } set { _currentTab = value; } }
        public SystemInfo? SystemInfo { get; set; }
        public IEnumerable<ReportExecutionStats>? ReportExecutionStats;
    }
}
