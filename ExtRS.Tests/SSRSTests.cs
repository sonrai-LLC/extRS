using ExtRS.Tests.Properties;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Sonrai.ExtRS.Models;
using ReportingServices.Api.Models;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace Sonrai.ExtRS.UnitTests
{
    [TestClass]
    public class SSRSTests
    {
        private SSRSService ssrs;
        private HttpClient httpClient;

        [TestInitialize]
        public async Task InitializeTests()
        {
            httpClient = new HttpClient();
            SSRSConnection connection = new SSRSConnection(Resources.ReportServerName, Resources.User, AuthenticationType.ExtRSAuth);
            connection.SqlAuthCookie = await SSRSService.GetSqlAuthCookie(httpClient, Resources.User, Resources.Passphrase, connection.ReportServerName);
            ssrs = new SSRSService(connection, null);
        }

        [TestMethod]
        public async Task GetGetSqlAuthCookieSucceeds()
        {
            var cookieString = await SSRSService.GetSqlAuthCookie(httpClient, Resources.User, Resources.Passphrase, Resources.ReportServerName);
            Assert.IsTrue(cookieString.Length > 0);
        }

        [TestMethod]
        public async Task CreateSessionSucceeds()
        {   
            HttpResponseMessage result = await ssrs.CreateSession(Resources.User, Resources.Passphrase, Resources.ReportServerName); 
            Assert.IsTrue(Convert.ToString(result.StatusCode) == "Created");
        }

        [TestMethod]
        public async Task DeleteSessionSucceeds()
        {
            var result = await ssrs.DeleteSession();
            Assert.IsTrue(Convert.ToString(result.StatusCode) == "OK");
        }

        [TestMethod]
        public async Task GetAllCatalogItemsSucceeds()
        {
            var catalogItems = await ssrs.GetCatalogItems();
            Assert.IsTrue(catalogItems.Count > 0);
        }

        [TestMethod]
        public async Task GetCatalogItemSucceeds()
        {
            CatalogItem dataSource = await ssrs.GetCatalogItem("path='/Reports/Team'");
            Assert.IsTrue(dataSource.Name != null);
        }

        [TestMethod]
        public async Task GetReportsSucceeds()
        {
            List<Report> reports = await ssrs.GetReports();
            Assert.IsTrue(reports.Count > 0);
        }

        [TestMethod]
        public async Task GetReportSucceeds()
        {
            Report report = await ssrs.GetReport("path='/Reports/Team'");
            Assert.IsTrue(report.Name != null);
        }

        [TestMethod]
        public async Task GetReportSnapshotsSucceeds()
        {
            Report report = await ssrs.GetReport("path='/Reports/Team'");
            Assert.IsTrue(report.Name != null);
        }

        [TestMethod]
        public async Task GetFoldersSucceeds()
        {
            List<Folder> folders = await ssrs.GetFolders();
            Assert.IsTrue(folders.Count > 0);
        }

        [TestMethod]
        public async Task GetFolderSucceeds()
        {
            Folder folder = await ssrs.GetFolder("path='/Reports'");
            Assert.IsTrue(folder.Name != null);
        }

        [TestMethod]
        public async Task GetDataSourcesSucceeds()
        {
            List<DataSource> dataSources = await ssrs.GetDataSources();
            Assert.IsTrue(dataSources.Count > 0);
        }

        [TestMethod]
        public async Task GetDataSourceSucceeds()
        {
            DataSource dataSource = await ssrs.GetDataSource("path='/Data Sources/localhost'");
            Assert.IsTrue(dataSource.Name != null);
        }

        [TestMethod]
        public async Task GetDataSetsSucceeds()
        {
            List<DataSet> datasets = await ssrs.GetDataSets();
            Assert.IsTrue(datasets.Count > 0);
        }

        [TestMethod]
        public async Task GetDataSetSucceeds()
        {
            DataSet dataSource = await ssrs.GetDataSet("path='/DataSets/Team'");
            Assert.IsTrue(dataSource.Name != null);
        }

        [TestMethod]
        public async Task GetReportSnapshotHistorySucceeds()
        {
            DataSet dataSource = await ssrs.GetDataSet("path='/DataSets/Team'");
            Assert.IsTrue(dataSource.Name != null);
        }

        [TestMethod]
        public async Task CreateSubscriptionSucceeds()
        {
            DataSet dataSource = await ssrs.GetDataSet("path='/DataSets/Team'");
            Assert.IsTrue(dataSource.Name != null);
        }

        [TestMethod]
        public async Task GetSubscriptionSucceeds()
        {
            DataSet dataSource = await ssrs.GetDataSet("path='/DataSets/Team'");
            Assert.IsTrue(dataSource.Name != null);
        }

        [TestMethod]
        public async Task SubscriptionSucceeds()
        {
            DataSet dataSource = await ssrs.GetDataSet("path='/DataSets/Team'");
            Assert.IsTrue(dataSource.Name != null);
        }

        [TestMethod]
        public async Task CreateGetDeleteSubscriptionSucceeds()
        {
            string json = @"{
            ""@odata.context"": ""https://localhost/reports/api/v2.0/$metadata#Subscriptions/$entity"",
                ""Id"": ""eff45b9d-d850-41ed-85ec-3cd59a5673c9"",
                ""Owner"": """ + Resources.User + @""",
                ""IsDataDriven"": false,
                ""Description"": ""string..."",
                ""Report"": ""/Reports/Team"",
                ""IsActive"": true,
                ""EventType"": ""TimedSubscription"",
                ""ScheduleDescription"": ""string..."",
                ""LastRunTime"": ""2023-04-13T15:51:04Z"",
                ""LastStatus"": ""string..."",
                ""DeliveryExtension"": ""Report Server Email"",
                ""LocalizedDeliveryExtensionName"": ""Email"",
                ""ModifiedBy"": """ + Resources.User + @""",
                ""ModifiedDate"": ""2023-04-13T15:51:04Z"",
                ""Schedule"": {
                    ""ScheduleID"": null,
                    ""Definition"": {
                        ""StartDateTime"": ""2021-01-01T02:00:00-07:00"",
                        ""EndDate"": ""0001-01-01T00:00:00Z"",
                        ""EndDateSpecified"": false,
                        ""Recurrence"": {
                            ""MinuteRecurrence"": null,
                            ""DailyRecurrence"": null,
                            ""WeeklyRecurrence"": null,
                            ""MonthlyRecurrence"": null,
                            ""MonthlyDOWRecurrence"": null
                        }
                    }
                },
                ""DataQuery"": null,
                ""ExtensionSettings"": {
                    ""Extension"": ""DeliveryExtension"",
                    ""ParameterValues"": [
                        {
                            ""Name"": ""TO"",
                            ""Value"": ""colin@sonrai.io"",
                            ""IsValueFieldReference"": false
                        },
                        {
                            ""Name"": ""IncludeReport"",
                            ""Value"": ""true"",
                            ""IsValueFieldReference"": false
                        },
                        {
                            ""Name"": ""Subject"",
                            ""Value"": ""true"",
                            ""IsValueFieldReference"": false
                        },
                        {
                            ""Name"": ""RenderFormat"",
                            ""Value"": ""PDF"",
                            ""IsValueFieldReference"": false
                        }
                    ]
                },
                ""ParameterValues"": []
            }";

            Subscription subscription = await ssrs.SaveSubscription(JsonConvert.DeserializeObject<Subscription>(json)!);
            Assert.IsTrue(subscription.DeliveryExtension != null);

            var getResponse = await ssrs.GetSubscription(subscription.Id.ToString()!);
            Assert.IsTrue(getResponse.Id != null);

            var delResp = await ssrs.DeleteSubscription(subscription.Id.ToString()!);
            Assert.IsTrue(delResp);
        }

        [TestMethod]
        public async Task CreateGetDeleteCatalogItemSucceeds()
        {
            string json = @"{
                ""@odata.type"": ""#Model.Folder"",
                ""Name"": ""TopSecretFolder11"",
                ""Description"": null,
                ""Path"": ""/Data Sources"",
                ""Type"": ""Folder"",
                ""Hidden"": false,
                ""Size"": 0,
                ""ModifiedBy"": ""extRSAuth"",
                ""ModifiedDate"": ""2023-11-08T19:58:11.277-06:00"",
                ""CreatedBy"": ""DayliteAdmin"",
                ""CreatedDate"": ""2022-07-30T15:03:24.563-05:00"",
                ""ParentFolderId"": ""0cb3efb3-41cb-4480-a9c9-da642a19526e"",
                ""IsFavorite"": false,
                ""ContentType"": null,
                ""Content"": """",
                ""Roles"": []
            }";

            var createResponse = await ssrs.CreateCatalogItem(json);
            Assert.IsTrue(createResponse.Id != null);

            var getResponse = await ssrs.GetCatalogItem(createResponse.Id.ToString()!);
            Assert.IsTrue(getResponse.Id != null);

            var deleteResponse = await ssrs.DeleteCatalogItem(getResponse.Id.ToString());
            Assert.IsTrue(deleteResponse);
        }

        [Ignore]
        [TestMethod]
        public async Task GetParameterHtmlSucceeds()
        {
            var result = await ssrs.GetParameterHtml("path='/Reports/Team'");
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Contains("<") && result.Contains("/>"));
        }

        [TestMethod]
        public async Task GetCatalogItemHtmlSucceeds()
        {
            string catalogItemResponse = await ssrs.GetCatalogItemHtml("path='/Reports/Team'");
            Assert.IsTrue(catalogItemResponse.ToString().Contains("<div "));
            Assert.IsTrue(catalogItemResponse.ToString().Contains("</div>"));
        }

        [TestMethod]
        public async Task GetSystemInfoSucceeds()
        {
            SystemInfo systemInfo = await ssrs.GetSystemInfo();
            Assert.IsTrue(systemInfo.ProductName.Length > 0);
            Assert.IsTrue(systemInfo.ProductType.Length > 0);
            Assert.IsTrue(systemInfo.ProductVersion.Length > 0);
            Assert.IsTrue(systemInfo.TimeZone.Length > 0);
        }

        [TestMethod]
        public async Task GetExecutionStatsSucceeds()
        {
            var stats = await ssrs.GetReportExecutionStats("Server=localhost;Database=ReportServer;User Id=extRSAuth;Integrated Security=true");
            Assert.IsTrue(stats.Count() > 0);
        }
    }
}
