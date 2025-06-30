using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
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
        public required Subscription Subscription { get; set; }
        public required List<Report> Reports { get; set; }
        public List<ReportParameterDefinition>? ReportParameters { get; set; }
        public List<ParameterValue>? SelectedReportParameters { get; set; }
        public List<RecurrenceType> RecurrenceTypes = new List<RecurrenceType> { RecurrenceType.Hourly, RecurrenceType.Daily, RecurrenceType.Weekly, RecurrenceType.Monthly, RecurrenceType.Onetime };
        public RecurrenceType? SelectedRecurrence { get; set; }
        public int RecurrenceHours { get; set; }
        public int RecurrenceMinutes { get; set; }
        [Range(1, 12, ErrorMessage = "Enter value between 1 and 12")]
        public int ScheduleStartHours { get; set; }
        [Range(1, 59, ErrorMessage = "Enter value between 1 and 59")]
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

    public static class EnumExtensions
    {
        public static string GetEnumDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString())!;
            DescriptionAttribute[] attributes = fi.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];

            if (attributes != null && attributes.Any())
            {
                return attributes.First().Description;
            }

            return value.ToString();
        }
    }
}
