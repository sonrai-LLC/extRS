using Sonrai.ExtRS.Models;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Net;
using Newtonsoft.Json;

namespace Sonrai.ExtRS
{
    public class SSRSService
    {
        public SSRSConnection conn;
        private HttpClient client;
        CookieContainer cookieContainer = new CookieContainer();
        string serverUrl;
       
        public SSRSService(SSRSConnection connection)
        {          
            conn = connection;
            client = new HttpClient();
            cookieContainer.Add(new Cookie("sqlAuthCookie", conn.SqlAuthCookie, "/", "localhost"));
            serverUrl = string.Format("https://{0}/reports/api/v2.0/", conn.ServerName);
        }

        public async Task<HttpResponseMessage> CallApi(HttpVerbs verb, string operation, string content = "", string parameters = "")
        {
            HttpResponseMessage response = new HttpResponseMessage();
            HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");
            using (var handler = new HttpClientHandler() { CookieContainer = cookieContainer })
            {
                using (client = new HttpClient(handler))
                {
                    switch (verb)
                    {
                        case HttpVerbs.POST:
                            return await client.PostAsync(serverUrl + operation, httpContent);
                        case HttpVerbs.GET:
                            return await client.GetAsync(serverUrl + operation);
                        case HttpVerbs.PUT:
                            return await client.DeleteAsync(serverUrl + operation);
                        case HttpVerbs.DELETE:
                            return await client.DeleteAsync(serverUrl + operation);
                    }
                }

                return null;
            }
        }

        public async Task<Report> GetReport(string idOrPath)
        {
            var response = await CallApi(HttpVerbs.GET, string.Format("Reports({0})", idOrPath));
            return JsonConvert.DeserializeObject<Report>(await response.Content.ReadAsStringAsync())!;
        }

        public async Task<CatalogItem> GetCatalogItem(string idOrPath)
        {
            var response = await CallApi(HttpVerbs.GET, string.Format("CatalogItems({0})", idOrPath));
            return JsonConvert.DeserializeObject<CatalogItem>(await response.Content.ReadAsStringAsync())!;
        }

        public async Task<Folder> GetFolder(string idOrPath)
        {
            var response = await CallApi(HttpVerbs.GET, string.Format("Folders({0})", idOrPath));
            return JsonConvert.DeserializeObject<Folder>(await response.Content.ReadAsStringAsync())!;
        }

        public async Task<DataSource> GetDataSource(string idOrPath)
        {
            var response = await CallApi(HttpVerbs.GET, string.Format("DataSources({0})", idOrPath));
            return JsonConvert.DeserializeObject<DataSource>(await response.Content.ReadAsStringAsync())!;
        }

        public async Task<DataSet> GetDataSet(string idOrPath)
        {
            var response = await CallApi(HttpVerbs.GET, string.Format("DataSets({0})", idOrPath));
            return JsonConvert.DeserializeObject<DataSet>(await response.Content.ReadAsStringAsync())!;
        }

        // TODO: create test
        public async Task<DataSetParameter> GetDataSetParameterDefinition(string idOrPath)
        {
            var response = await CallApi(HttpVerbs.GET, string.Format("DataSets({0})/ParameterDefinitions", idOrPath));
            return JsonConvert.DeserializeObject<DataSetParameter>(await response.Content.ReadAsStringAsync())!;
        }

        public async Task<ReportParameterDefinitions> GetReportParameterDefinition(string idOrPath)
        {
            var response = await CallApi(HttpVerbs.GET, string.Format("Reports({0})/ParameterDefinitions", idOrPath));
            var parms = JsonConvert.DeserializeObject<ReportParameterDefinitions>(await response.Content.ReadAsStringAsync())!;

            return parms;
        }

        public static string GetCredentialJson(string user, string password, string domain)
        {
            return string.Format("\"UserName\":\"{0}\",\"Password\": \"{1}\",\"Domain\":\"{2}\"", user, password, domain);
        }

        public static async Task<string> GetSqlAuthCookie(HttpClient client, string user = "ExtRSAuth", string password = "", string domain = "localhost")
        {
            string cookie = "";
            StringContent httpContent = new StringContent("{" + GetCredentialJson(user, password, domain) + "}", Encoding.UTF8, "application/json");

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

            switch (catalogItem.Type)
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
            ReportParameterDefinitions definitions = await GetReportParameterDefinition(pathOrId);
            // TODO: will improve above method and implement this method
            // once ExtRS is fully implemented
            return "</>";
        }
    }   
}
