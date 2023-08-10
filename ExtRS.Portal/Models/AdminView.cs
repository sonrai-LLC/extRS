using System.ComponentModel;

namespace ExtRS.Portal.Models
{
    public class AdminView
    {
        public Guid AdminID { get; set; }

        public string AdminUser { get; set; }

        public int AuthType { get; set; }

        public String UserName { get; set; }
    }
}
