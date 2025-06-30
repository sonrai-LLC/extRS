using Newtonsoft.Json;

namespace Sonrai.ExtRS.Models
{
    public class SSRSConnection
    {
        public HttpClient _httpClient { get; set; }
        public string? SqlAuthCookie;
        public string UserName;
        public string? UserRole;
        public string ReportServerName;
        public AuthenticationType AuthenticationType;

        protected bool IsOnline = false;

        public SSRSConnection(string reportServerName, string userName, AuthenticationType authType = AuthenticationType.ExtRSAuth)
        {
            ReportServerName = reportServerName;
            AuthenticationType = authType;
            _httpClient = new HttpClient();
            UserName = userName;
        }
    }

    public enum AuthenticationType
    {
        Windows,
        Forms,
        ExtRSAuth
    }

    public enum HttpVerbs
    {
        POST,
        GET,
        PUT,
        DELETE
    }
}
