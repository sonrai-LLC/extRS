using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using ReportingServices.Api.Models;
using Sonrai.ExtRS.Models;
using System.Security.Policy;
using System;
using System.Collections.Generic;
using ExtRS.Properties;

namespace Sonrai.ExtRS
{
    public class SSRSService
    {
        public IConfiguration _configuration;
        public SSRSConnection _conn;
        private HttpClient _client;
        CookieContainer _cookieContainer = new CookieContainer();
        string _serverUrl;

        public SSRSService(SSRSConnection connection, IConfiguration? configuration)
        {
            _conn = connection;
            _client = new HttpClient();
            _cookieContainer.Add(new Cookie("sqlAuthCookie", _conn.SqlAuthCookie, "/", configuration == null ? "localhost" : configuration["ReportServerName"]!));
            _serverUrl = string.Format("https://{0}/reports/api/v2.0/", _conn.ReportServerName);
            _configuration = configuration;
        } 

        public async Task<HttpResponseMessage> CallApi(HttpVerbs verb, string operation, string content = "", string parameters = "")
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

        public async Task<HttpResponseMessage> DeleteSession()
        {
            HttpResponseMessage response = await CallApi(HttpVerbs.DELETE, "Session");
            return response;
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

        public async Task<Subscription> CreateSubscription(Subscription subscription)
        {
            Subscription newSubscription = new Subscription();
            try
            {
                var subscriptionJson = JsonConvert.SerializeObject(subscription);
                var response = await CallApi(HttpVerbs.POST, "Subscriptions", subscriptionJson);
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
            return JsonConvert.DeserializeObject<Subscription>(await response.Content.ReadAsStringAsync())!;
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
            var response = await CallApi(HttpVerbs.POST, string.Format("Reports({0})/HistorySnapshots)", reportId));
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

        public async Task<DataSetParameter> GetDataSetParameterDefinition(string idOrPath)
        {
            var response = await CallApi(HttpVerbs.GET, string.Format("DataSets({0})/ParameterDefinitions", idOrPath));
            return JsonConvert.DeserializeObject<DataSetParameter>(await response.Content.ReadAsStringAsync())!;
        }

        public async Task<List<ReportParameterDefinition>> GetReportParameterDefinition(string idOrPath)
        {
            var response = await CallApi(HttpVerbs.GET, string.Format("Reports({0})/ParameterDefinitions", idOrPath));
            List<ReportParameterDefinition> parms = JsonConvert.DeserializeObject<List<ReportParameterDefinition>>(await response.Content.ReadAsStringAsync())!;

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

        public static async Task<string> GetSqlAuthCookie(HttpClient client, string user = "extRSAuth", string password = "", string domain = "localhost")
        {
            string cookie = "";
            StringContent httpContent = new StringContent(GetCredentialJson(user, password, domain), Encoding.UTF8, "application/json");

            var response = await client.PostAsync(string.Format("https://{0}/reports/api/v2.0/Session", domain), httpContent);
            HttpHeaders headers = response.Headers;
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
            List<ReportParameterDefinition> definitions = await GetReportParameterDefinition(pathOrId);
            // TODO: will improve above method and implement this method
            // once ExtRS is fully implemented
            return "";
        }
    }
}
