using System.ComponentModel;
using ReportingServices.Api.Models;
using Sonrai.ExtRS.Models.Enums;

namespace ExtRS.Portal.Models
{
    public class SubscriptionsView : LayoutView
    {
        public Guid? UserID { get; set; }
        public string? DefaultEmailAddress { get; set; }
        private string? _currentTab;
        public List<Subscription>? Subscriptions { get; set; }
        public Subscription? SelectedSubscription { get; set; }
        public override string? CurrentTab { get { return _currentTab; } set { _currentTab = value!; } }
    }

    public class SubscriptionView : LayoutView
    {
        public Guid? UserID { get; set; }
        public Subscription? Subscription { get; set; }
        public List<Report>? Reports { get; set; }
        public Report? SelectedReport { get; set; }
        public int RecurrenceHours { get; set; }
        public string? SelectedRecurrence { get; set; }
        public List<ReportParameterDefinition>? ReportParameters { get; set; }
        public List<ParameterValue>? SelectedReportParameters { get; set; }
        public List<RecurrenceType> RecurrenceTypes = new List<RecurrenceType> { RecurrenceType.Hourly, RecurrenceType.Daily, RecurrenceType.Weekly, RecurrenceType.Monthly, RecurrenceType.Onetime }; // @Html.GetEnumSelectList (leaving out for now..)
        public string? ToAddress { get; set; }
        public string? CcAddress { get; set; }
        public string? BccAddress { get; set; }
        public string? ReplyToAddress { get; set; }
        public string? RenderFormat { get; set; }
        public string? EmailBody { get; set; }
        public bool IncludeReport { get; set; }
        public bool IncludeLink { get; set; }
        public string? EmailSubject { get; set; }
        public string? EmailPriority { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        private string? _currentTab;
        public override string? CurrentTab { get { return _currentTab; } set { _currentTab = value!; } }
    }
}
