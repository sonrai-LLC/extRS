using Newtonsoft.Json;

namespace Sonrai.ExtRS.Models
{
    public class SSRSConnection
    {
        public HttpClient _httpClient { get; set; }
        public string SqlAuthCookie;
        public string Administrator = "extRSAuth";
        public string serName;
        public string UserRole;
        public string ReportServerName;
        public AuthenticationType AuthenticationType;

        protected bool IsOnline = false;

        public SSRSConnection(string reportServerName, string adminUser, AuthenticationType authType = AuthenticationType.extRSAuth)
        {
            ReportServerName = reportServerName;
            Administrator = adminUser ?? Administrator;
            AuthenticationType = authType;
            _httpClient = new HttpClient();
        }
    }

    public enum AuthenticationType
    {
        Windows,
        Forms,
        extRSAuth
    }

    public enum HttpVerbs
    {
        POST,
        GET,
        PUT,
        DELETE
    }
}
