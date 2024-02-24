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
        public List<ParameterValue> ReportParameters { get; set; }
        public List<RecurrenceType> RecurrenceTypes = new List<RecurrenceType> { RecurrenceType.Hourly, RecurrenceType.Daily, RecurrenceType.Weekly, RecurrenceType.Monthly, RecurrenceType.MonthlyDOW, RecurrenceType.Onetime };
        public string DefaultEmailAddress { get; set; }
        public MinuteRecurrence? MinuteRecurrence { get; set; }
        public DailyRecurrence? DailyRecurrence { get; set; }
        public WeeklyRecurrence? WeeklyRecurrence { get; set; }
        public MonthlyRecurrence? MonthlyRecurrence { get; set; }
        public MonthlyDOWRecurrence? MonthlyDOWRecurrence { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        private string? _currentTab;
        public override string? CurrentTab { get { return _currentTab; } set { _currentTab = value!; } }
    }
}
