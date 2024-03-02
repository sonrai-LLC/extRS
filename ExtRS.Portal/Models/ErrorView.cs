namespace ExtRS.Portal.Models
{
    public class ErrorView : LayoutView
    {
        private string _currentTab;
        public override string CurrentTab { get { return _currentTab; } set { _currentTab = value; } }
        public string? RequestId { get; set; }
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
