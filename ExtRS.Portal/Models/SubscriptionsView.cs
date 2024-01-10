using System.ComponentModel;
using ReportingServices.Api.Models;

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
        public override string CurrentTab { get { return _currentTab; } set { _currentTab = value; } }
    }

    public class SubscriptionView : LayoutView
    {
        public Guid UserID { get; set; }
        public string ReportServerName;
        public Subscription Subscription { get; set; }
        public List<Report> Reports { get; set; }
        public string DefaultEmailAddress { get; set; }
        private string _currentTab;
        public override string CurrentTab { get { return _currentTab; } set { _currentTab = value; } }
    }
}
