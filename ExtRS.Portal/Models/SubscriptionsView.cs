using System.ComponentModel;
using ReportingServices.Api.Models;
using Sonrai.ExtRS.Models.Enums;

namespace ExtRS.Portal.Models
{
    public class SubscriptionsView : LayoutView
    {
        public Guid UserID { get; set; }

        public string DefaultEmailAddress { get; set; }
        private string _currentTab;
        public string ReportServerName;
        public List<Subscription> Subscriptions { get; set; }
        public Subscription SelectedSubscription { get; set; }
        public override string? CurrentTab { get { return _currentTab; } set { _currentTab = value!; } }
    }

    public class SubscriptionView : LayoutView
    {
        public Guid UserID { get; set; }
        public string ReportServerName;
        public Subscription Subscription { get; set; }
        public List<Report> Reports { get; set; }
        public Report SelectedReport { get; set; }
        public List<ReportParameterDefinition> ReportParameters { get; set; }
        public List<ParameterValue> SelectedReportParameters { get; set; }
        public List<RecurrenceType> RecurrenceTypes = new List<RecurrenceType> { RecurrenceType.Hourly, RecurrenceType.Daily, RecurrenceType.Weekly, RecurrenceType.Monthly, RecurrenceType.MonthlyDOW, RecurrenceType.Onetime };
        public string ToEmailAddress { get; set; }
        public string RenderFormat { get; set; }
        public string Subject { get; set; }
        public string Priority { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        private string? _currentTab;
        public override string? CurrentTab { get { return _currentTab; } set { _currentTab = value!; } }
    }
}
