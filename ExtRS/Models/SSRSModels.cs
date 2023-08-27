using Newtonsoft.Json;

namespace Sonrai.ExtRS.Models
{
    public class SSRSConnection
    {
        public HttpClient HttpClient { get; set; }
        public string SqlAuthCookie;
        public string Administrator = "ExtRSAuth";
        public string serName;
        public string UserRole;
        public string ServerName;
        public AuthenticationType AuthenticationType;

        protected bool IsOnline = false;

        public SSRSConnection(string serverName, string adminUser, AuthenticationType authType = AuthenticationType.ExtRSAuth)
        {
            ServerName = serverName;
            Administrator = adminUser ?? Administrator;
            AuthenticationType = authType;
            HttpClient = new HttpClient();
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
