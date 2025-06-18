# extRS
extRS is a .NET class library for extending the capabilities of Reporting Services, among other things. With extRS, (pronounced "extras"), common public API endpoints and SDK clients are consolidated into a utility .dll containing features that can compliment your .NET development. extRS also contains a simplified interface to the SSRS v2 API for programmatically managing SSRS catalog item types (CatalogItem, Report, DataSource, DataSet, etc.).

SSRS ain't dead- it's just a niche tool that hasn't fully realized its potential- yet. 🤷‍♂️

Here is an implementation of extRS in the form of an SSRS client (extRS.Portal.csproj) that exposes some of the default SSRS Portal feature set: https://extrs.net

# Examples

```
        [TestInitialize]
        public async Task InitializeTests()
        {
            httpClient = new HttpClient();
            SSRSConnection connection = new SSRSConnection("localhost", "extRSAuth", 
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
- [extRSAuth](https://github.com/sonrai-LLC/extRSAuth) for enabling further extension of the SSRS Microsoft Custom Security Sample.
