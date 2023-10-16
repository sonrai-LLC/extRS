# ExtRS
ExtRS is a .NET class library for extending the capabilities of Reporting Services, among other things. With ExtRS, (pronounced "extras"), common public API endpoints and SDK clients are consolidated into a utility .dll containing features that can compliment your .NET development. ExtRS also contains a simplified interface to the SSRS v2 API for programmatically managing SSRS catalog item types (CatalogItem, Report, DataSource, DataSet, etc.).

SSRS ain't dead- it's just a niche tool that hasn't fully realized its potential- yet. 🤷‍♂️

Here, hosted is an implementation of ExtRS in an SSRS client (ExtRS.Portal.csproj) that exposes some of the default SSRS Portal feature set: https://extrsportal.azurewebsites.net

# Requirements
This plug-in works as a drop-in Nuget package for .NET projects as well an SSRS Custom Assembly as described by Microsoft here: https://docs.microsoft.com/en-us/sql/reporting-services/custom-assemblies/using-custom-assemblies-with-reports?view=sql-server-ver15

# Contents
This package includes the following components:
- Sonrai.ExtRS.dll

# Examples

```
        [TestInitialize]
        public async Task InitializeTests()
        {
            httpClient = new HttpClient();
            SSRSConnection connection = new SSRSConnection("localhost", "ExtRSAuth", 
            AuthenticationType.ExtRSAuth);
            connection.SqlAuthCookie = await SSRSService.GetSqlAuthCookie(httpClient, 
            connection.Administrator, "", connection.ServerName);
            ssrs = new SSRSService(connection);
        }

       [TestMethod]
        public async Task CreateGetDeleteReportSucceeds()
        {
            string content = ssrsReportItemJsonString; // ie:  { "@odata.type": "#Model.Report", "id":...

            var postResp = await ssrs.CallApi("POST", "Reports", "{" + content + "}");
            Assert.IsTrue(postResp.IsSuccessStatusCode);   
            
            var getResponse = await ssrs.CallApi("GET", postResp.Headers.Location.Segments[4]);
            Assert.IsTrue(getResponse.IsSuccessStatusCode);

            var deleteResp = await ssrs.CallApi("DELETE", postResp.Headers.Location.Segments[4]);
            Assert.IsTrue(deleteResp.IsSuccessStatusCode);
        }
        
        public SSRSService(SSRSConnection connection)
        {          
            conn = connection;
            client = new HttpClient();
            cookieContainer.Add(new Cookie("sqlAuthCookie", conn.SqlAuthCookie, "/", "localhost"));
            serverUrl = string.Format("https://{0}/reports/api/v2.0/", conn.ServerName);
        }
```

# Related SSRS Tools
- [ExtRSAuth](https://github.com/sonrai-LLC/ExtRSAuth) for enabling further extension of the SSRS Microsoft Custom Security Sample.
