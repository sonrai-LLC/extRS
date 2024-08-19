using Dapper;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ReportingServices.Api.Models;
using Sonrai.ExtRS.Models;
using System.Data.SqlClient;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;

namespace Sonrai.ExtRS
{
    public class SSRSService
    {
        public IConfiguration? _configuration;
        public SSRSConnection _conn;
        private HttpClient _client;
        CookieContainer _cookieContainer = new CookieContainer();
        string _serverUrl;

        public SSRSService(SSRSConnection connection, IConfiguration? configuration)
        {
            _conn = connection;
            _client = new HttpClient();
            var cookie = new Cookie("sqlAuthCookie", _conn.SqlAuthCookie, "/", configuration == null ? "localhost" : configuration["ReportServerName"]!);
            _cookieContainer.Add(cookie);
            _serverUrl = string.Format("https://{0}/reports/api/v2.0/", _conn.ReportServerName);
            _configuration = configuration;
        } 

        public async Task<HttpResponseMessage?> CallApi(HttpVerbs verb, string operation, string content = "", string parameters = "")
        {
            HttpResponseMessage response = new HttpResponseMessage();
            HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");
            using (var handler = new HttpClientHandler() { CookieContainer = _cookieContainer })
            {
                using (_client = new HttpClient(handler))
                {
                    switch (verb)
                    {
                        case HttpVerbs.POST:
                            return await _client.PostAsync(_serverUrl + operation, httpContent);
                        case HttpVerbs.GET:
                            return await _client.GetAsync(_serverUrl + operation);
                        case HttpVerbs.PUT:
                            return await _client.PutAsync(_serverUrl + operation, httpContent);
                        case HttpVerbs.DELETE:
                            return await _client.DeleteAsync(_serverUrl + operation);
                    }
                }

                return null;
            }
        }
 
        public async Task<HttpResponseMessage> CreateSession(string user, string password, string server)
        {
            string credentials = GetCredentialJson(user, password, server);
            HttpResponseMessage response = await CallApi(HttpVerbs.POST, "Session", credentials);
            return response;
        }

        public async Task<Me> GetRSSessionUser()
        {
            var response = await CallApi(HttpVerbs.GET, "Me");
            return JsonConvert.DeserializeObject<Me>(await response.Content.ReadAsStringAsync())!;
        }
         
        public async Task<CatalogItem> CreateCatalogItem(string json)
        {
            var response = await CallApi(HttpVerbs.POST, "CatalogItems", json);
            return JsonConvert.DeserializeObject<CatalogItem>(await response.Content.ReadAsStringAsync())!;
        }

        public async Task<CatalogItem> GetCatalogItem(string id)
        {
            var response = await CallApi(HttpVerbs.GET, string.Format("CatalogItems({0})", id));
            return JsonConvert.DeserializeObject<CatalogItem>(await response.Content.ReadAsStringAsync())!;
        }

        public async Task<List<CatalogItem>> GetCatalogItems()
        {
            var response = await CallApi(HttpVerbs.GET, "CatalogItems");
            return JsonConvert.DeserializeObject<ODataCatalogItems>(await response.Content.ReadAsStringAsync())!.Value;
        }

        public async Task<Subscription> SaveSubscription(Subscription subscription)
        {
            Subscription newSubscription = new Subscription();
            try
            {
                // offset selected datetime for UTC format
                var offset = TimeZoneInfo.Local.GetUtcOffset(DateTime.UtcNow);
                subscription!.Schedule.Definition.StartDateTime = subscription!.Schedule.Definition.StartDateTime!.Value.AddHours(Math.Abs(offset.TotalHours));

                var subscriptionJson = JsonConvert.SerializeObject(subscription, Formatting.Indented, new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    DateFormatString = "yyyy-MM-ddTHH:mm:ssZ",
                });

                HttpResponseMessage response;
                if(subscription.Id != null)
                {
                    response = await CallApi(HttpVerbs.PUT, string.Format("Subscriptions({0})", subscription.Id), subscriptionJson);
                }
                else
                {
                    response = await CallApi(HttpVerbs.POST, "Subscriptions", subscriptionJson);
                }
                
                var newSubscription2 = JsonConvert.DeserializeObject<Subscription>(await response.Content.ReadAsStringAsync());

                return newSubscription2!;
            }
            catch (Exception ex)
            { 
            
            }

            return newSubscription!;
        }

        public async Task<Subscription> GetSubscription(string id)
        {
            var response = await CallApi(HttpVerbs.GET, string.Format("Subscriptions({0})", id));
            var subscription = JsonConvert.DeserializeObject<Subscription>(await response.Content.ReadAsStringAsync());

            return subscription;
        }

        public async Task<bool> DeleteSubscription(string id)
        {
            var response = await CallApi(HttpVerbs.DELETE, string.Format("Subscriptions({0})", id));      
            return true;
        }

        public async Task<Report> GetReport(string idOrPath)
        {
            var response = await CallApi(HttpVerbs.GET, string.Format("Reports({0})", idOrPath));
            return JsonConvert.DeserializeObject<Report>(await response.Content.ReadAsStringAsync())!;
        }

        public async Task<List<Report>> GetReports()
        {
            var response = await CallApi(HttpVerbs.GET, "Reports");
            return JsonConvert.DeserializeObject<ODataReports>(await response.Content.ReadAsStringAsync())!.Value;
        }

        public async Task<HistorySnapshot> CreateReportSnapshot(string reportId)
        {
            var response = await CallApi(HttpVerbs.POST, string.Format("Reports({0})/HistorySnapshots", reportId));
            return JsonConvert.DeserializeObject<HistorySnapshot>(await response.Content.ReadAsStringAsync())!;
        }

        public async Task<List<HistorySnapshot>> GetReportSnapshots(string id)
        {
            var response = await CallApi(HttpVerbs.GET, string.Format("Reports({0})/HistorySnapshots", id));
            return JsonConvert.DeserializeObject<ODataHistorySnapshots>(await response.Content.ReadAsStringAsync())!.Value;
        }

        public async Task<HistorySnapshot> GetReportSnapshot(string reportId, string historyId)
        {
            var response = await CallApi(HttpVerbs.GET, string.Format("Reports({0})/HistorySnapshots({1})", reportId, historyId));
            return JsonConvert.DeserializeObject<HistorySnapshot>(await response.Content.ReadAsStringAsync())!;
        }

        public async Task<bool> DeleteReportSnapshot(string id, string historyId)
        {
            var response = await CallApi(HttpVerbs.DELETE, string.Format("Reports({0})/HistorySnapshots({1})", id, historyId));
            return true;
        }

        public async Task<string> GetCatalogItemContent(string idOrPath)
        {
            var response = await CallApi(HttpVerbs.GET, string.Format("CatalogItems({0})/Content/$value", idOrPath));
            return await response.Content.ReadAsStringAsync()!;
        }

        public async Task<bool> DeleteCatalogItem(string idOrPath)
        {
            var response = await CallApi(HttpVerbs.DELETE, string.Format("CatalogItems({0})", idOrPath));
            return true;
        }

        public async Task<HttpResponseMessage> DeleteSession()
        {
            var response = await CallApi(HttpVerbs.DELETE, "Session");
            return response;
        }

        public async Task<List<Folder>> GetFolders()
        {
            var response = await CallApi(HttpVerbs.GET, "Folders");
            return JsonConvert.DeserializeObject<ODataFolders>(await response.Content.ReadAsStringAsync())!.Value;
        }

        public async Task<Folder> GetFolder(string idOrPath)
        {
            var response = await CallApi(HttpVerbs.GET, string.Format("Folders({0})", idOrPath));
            return JsonConvert.DeserializeObject<Folder>(await response.Content.ReadAsStringAsync())!;
        }

        public async Task<List<DataSource>> GetDataSources()
        {
            var response = await CallApi(HttpVerbs.GET, "DataSources");
            return JsonConvert.DeserializeObject<ODataDataSources>(await response.Content.ReadAsStringAsync())!.Value;
        }

        public async Task<DataSource> GetDataSource(string idOrPath)
        {
            var response = await CallApi(HttpVerbs.GET, string.Format("DataSources({0})", idOrPath));
            return JsonConvert.DeserializeObject<DataSource>(await response.Content.ReadAsStringAsync())!;
        }

        public async Task<List<DataSet>> GetDataSets()
        {
            var response = await CallApi(HttpVerbs.GET, "DataSets");
            return JsonConvert.DeserializeObject<ODataDataSets>(await response.Content.ReadAsStringAsync())!.Value;
        }

        public async Task<DataSet> GetDataSet(string idOrPath)
        {
            var response = await CallApi(HttpVerbs.GET, string.Format("DataSets({0})", idOrPath));
            return JsonConvert.DeserializeObject<DataSet>(await response.Content.ReadAsStringAsync())!;
        }

        public async Task<List<Subscription>> GetSubscriptions()
        {
            var response = await CallApi(HttpVerbs.GET, "Subscriptions");
            return JsonConvert.DeserializeObject<ODataSubscriptions>(await response.Content.ReadAsStringAsync())!.Value;
        }

        public async Task<ODataDataSetParameters> GetDataSetParameterDefinitions(string idOrPath)
        {
            var response = await CallApi(HttpVerbs.GET, string.Format("DataSets({0})/ParameterDefinitions", idOrPath));
            return JsonConvert.DeserializeObject<ODataDataSetParameters>(await response.Content.ReadAsStringAsync())!;
        }

        public async Task<List<ReportParameterDefinition>> GetReportParameterDefinition(string idOrPath)
        {
            var response = await CallApi(HttpVerbs.GET, string.Format("Reports({0})/ParameterDefinitions", idOrPath));
            var parms = JsonConvert.DeserializeObject<ODataReportParameterDefinitions>(await response.Content.ReadAsStringAsync())!.Value.ToList();

            return parms;
        }

        public async Task<ReportParameterDefinition> GetReportParameterDefinitions()
        {
            var response = await CallApi(HttpVerbs.GET, string.Format("Reports({0})/ParameterDefinitions"));
            var parms = JsonConvert.DeserializeObject<ReportParameterDefinition>(await response.Content.ReadAsStringAsync())!;

            return parms;
        }

        public static string GetCredentialJson(string user, string password, string domain)
        {
            return "{" + string.Format("\"UserName\":\"{0}\",\"Password\": \"{1}\",\"Domain\":\"{2}\"", user, password, domain) + "}";
        }

        public async Task<string> GetSqlAuthCookie(HttpClient client, string user = "extRSAuth", string password = "", string domain = "localhost")
        {
            string cookie = "sqlAuthCookie=";
            //StringContent httpContent = new StringContent(GetCredentialJson(user, password, domain), Encoding.UTF8, "application/json");

            // first check the ReportServer db to ensure the user exists, and if not, create new RS user.
            // {{ ie. "Id": "00000000-0000-0000-0000-000000000000"
            // ....otherwise the user session is ephemeral and no Policies can be assoc'd w/the user}}

            //var postResponse = await client.PostAsync(string.Format("https://{0}/reports/api/v2.0/Session", domain), httpContent);
            //var deleteResponse = await client.DeleteAsync(string.Format("https://{0}/reports/api/v2.0/Session"));
            //postResponse = await client.PostAsync(string.Format("https://{0}/reports/api/v2.0/Session", domain), httpContent);

            // first, delete existing session to replace with new cookie if user has changed
            var postResponse = await CreateSession(user, password, "localhost");
            //await DeleteSession();
            //postResponse = await CreateSession(user, password, domain);

            HttpHeaders headers = postResponse.Headers;
            if (headers.TryGetValues("Set-Cookie", out IEnumerable<string> values))
            {
                cookie = values.First();
            }
            string pattern = @"(sqlAuthCookie=[A-Z0-9])\w+";
            Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
            Match sqlAuthCookie = regex.Match(cookie);

            return sqlAuthCookie.Value.Replace("sqlAuthCookie=", "");
        }

        public async Task<string> GetCatalogItemHtml(string pathOrId, string onClick = "", string css = "")
        {
            CatalogItem catalogItem = await GetCatalogItem(pathOrId);
            StringBuilder sb = new StringBuilder();

            switch (catalogItem.Type.ToString())
            {
                case "Folder":
                    {
                        sb.Append("<a id=\"").Append(catalogItem.Id).Append("\" href=\"").Append(catalogItem.Path).Append("\"><div style=\"color: lime; background-color:#000000; font-size:11pt; font-weight:bold;\">").Append(catalogItem.Name).Append("</div></a>");
                        break;
                    }
                case "Report":
                    {
                        sb.Append("<a id=\"").Append(catalogItem.Id).Append("\" href=\"").Append(catalogItem.Path).Append("\"><div style=\"color: gold; background-color:#000000; font-size:11pt; font-weight:bold;\">").Append(catalogItem.Name).Append("</div></a>");
                        break;
                    }
                case "DataSource":
                    {
                        sb.Append("<a id=\"").Append(catalogItem.Id).Append("\" href=\"").Append(catalogItem.Path).Append("\"><div style=\"color: cyan; background-color:#000000; font-size:11pt; font-weight:bold;\">").Append(catalogItem.Name).Append("</div></a>");
                        break;
                    }
                case "Dataset":
                    {
                        sb.Append("<a id=\"").Append(catalogItem.Id).Append("\" href=\"").Append(catalogItem.Path).Append("\"><div style=\"color: pink; background-color:#000000; font-size:11pt; font-weight:bold;\">").Append(catalogItem.Name).Append("</div></a>");
                        break;
                    }
            }

            return sb.ToString();
        }

        public async Task<string> GetParameterHtml(string pathOrId)
        {
            var definitions = await GetReportParameterDefinition(pathOrId);

            return "";
        }

        public string GetCascadeParameters(string source, string sourceCol, string cascade, string casecadeCol)
        {
            return "";
        }

        public byte[] GetRecentSSRSLogs()
        {
            return new byte[0]; // TODO
        }

        public string GetReportServerInfo()
        {
            return "";
        }

        public async Task<SystemInfo> GetSystemInfo()
        {
            var response = await CallApi(HttpVerbs.GET, string.Format("System"));
            return JsonConvert.DeserializeObject<SystemInfo>(await response.Content.ReadAsStringAsync())!;
        }

        public async Task<IEnumerable<ReportExecutionStats>> GetReportExecutionStats(string connectionString)
        {
            using (var db = new SqlConnection(connectionString))
            {
                db.Open();
                string query = @"USE ReportServer;
                                SELECT el.UserName, 
                                C.NAME as Report, 
                                COUNT(1) AS[GeneratedNumber]
                                FROM REPORTSERVER.DBO.EXECUTIONLOG(NOLOCK) el
                                INNER JOIN REPORTSERVER.DBO.CATALOG(NOLOCK) c ON el.REPORTID = c.ITEMID
                                GROUP BY el.USERNAME, c.NAME
                                ORDER BY el.USERNAME, c.NAME";

                var stats = await db.QueryAsync<ReportExecutionStats>(query);

                return stats;
            }
        }
    }
}
