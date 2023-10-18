using System.ComponentModel;

namespace ExtRS.Portal.Models
{
    public class LayoutView
    {
        public virtual string? CurrentTab { get; set; }
        public virtual bool OpenInLinksNewTab { get { return false; } set { } }
    }
}
