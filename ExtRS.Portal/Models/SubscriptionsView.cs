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
        public Subscription? Subscription { get; set; }
        public List<Report>? Reports { get; set; }
        public List<ReportParameterDefinition>? ReportParameters { get; set; }
        public List<ParameterValue>? SelectedReportParameters { get; set; }
        public List<RecurrenceType> RecurrenceTypes = new List<RecurrenceType> { RecurrenceType.Hourly, RecurrenceType.Daily, RecurrenceType.Weekly, RecurrenceType.Monthly, RecurrenceType.Onetime };
        public string? SelectedRecurrence { get; set; }
        public int RecurrenceHours { get; set; }
        public int ScheduleStartHours { get; set; }
        public int ScheduleStartMinutes { get; set; }
        public bool IsAM { get; set; }
        public bool IsPM { get; set; }
        public bool ScheduleRecurrenceIsEveryWeekday { get; set; }
        public bool ScheduleRecurrenceIsEveryWeekend { get; set; }
        public bool IncludeReport { get; set; }
        public bool IncludeLink { get; set; }    
        private string? _currentTab;
        public override string? CurrentTab { get { return _currentTab; } set { _currentTab = value!; } }
    }
}
