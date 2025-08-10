# extRS
extRS is a .NET 9 class library for extending the capabilities of Reporting Services, among other things. With extRS, (pronounced "extras"), common public API endpoints and SDK clients are consolidated into a utility .dll containing features that can compliment your .NET development. extRS also contains a simplified interface to the SSRS v2 API for programmatically managing SSRS catalog item types (CatalogItem, Report, DataSource, DataSet, etc.).

SSRS ain't dead- it's just a niche tool that hasn't fully realized its potential- yet. 🤷‍♂️

Here is an implementation of extRS in the form of an SSRS client (extRS.Portal.csproj) that exposes some of the default SSRS Portal feature set: https://extrs.net

# Examples

```
       public SSRSTests()
 {
     var builder = new ConfigurationBuilder()
       .AddUserSecrets<ReferenceDataTests>();
     _configuration = builder.Build();
 }

 [TestInitialize]
 public async Task InitializeTests()
 {
     _httpClient = new HttpClient();
     SSRSConnection connection = new SSRSConnection(Resources.ReportServerName, Resources.User, AuthenticationType.ExtRSAuth);
     _ssrs = new SSRSService(connection, _configuration, null!);
     connection.SqlAuthCookie = await SSRSService.GetSqlAuthCookie(_httpClient, Resources.User, _configuration["extrspassphrase"]!, connection.ReportServerName);
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
    ""CreatedBy"": ""extRSAuth"",
    ""CreatedDate"": ""2022-07-30T15:03:24.563-05:00"",
    ""ParentFolderId"": ""0cb3efb3-41cb-4480-a9c9-da642a19526e"",
    ""IsFavorite"": false,
    ""ContentType"": null,
    ""Content"": """",
    ""Roles"": []
}";

    var createResponse = await _ssrs.CreateCatalogItem(json);
    Assert.IsTrue(createResponse.Id != null);

    var getResponse = await _ssrs.GetCatalogItem(createResponse.Id.ToString()!);
    Assert.IsTrue(getResponse.Id != null);

    var deleteResponse = await _ssrs.DeleteCatalogItem(getResponse!.Id!.ToString()!);
    Assert.IsTrue(deleteResponse);
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
