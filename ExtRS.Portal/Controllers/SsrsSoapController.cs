using ExtRS.Portal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using ReportingServices.Api.Models;
using ReportService2010;
using Sonrai.ExtRS;
using Sonrai.ExtRS.Models;
using System;
using System.Net;
using System.Reflection;
using System.Security.Policy;
using static Microsoft.EntityFrameworkCore.Metadata.Internal.EntityType;

namespace ExtRS.Portal.Controllers
{
    public class SsrsController : Controller
    {
        public async Task<IActionResult> ListItemTypes()
        {
            var client = new ReportingService2010SoapClient(ReportingService2010SoapClient.EndpointConfiguration.ReportingService2010Soap);

            client.ClientCredentials.Windows.ClientCredential = new NetworkCredential("extRSAuth", "This_IS_a_simpl_Passphrase", "localhost");
            var req = new ListItemTypesRequest();     
            var resp = client.ListItemTypes(new TrustedUserHeader(), out var result);

            return View();

            // or use default creds if applicable:
            // client.ClientCredentials.Windows.ClientCredential = CredentialCache.DefaultNetworkCredentials;

            //var req = new CreateCatalogItemRequest
            //{
            //    ItemType = "Folder",
            //    Name = "Test11111",
            //    Parent = "/"
            //    // fill required fields here
            //};

            //// You still need a generated proxy instance (from the SSRS WSDL) to create the "web service client".
            //// The wrapper below then calls it generically via reflection.

            //var serviceUrl = "https://localhost/ReportServer/ReportService2010.asmx"; //Environment.GetEnvironmentVariable("SSRS_REPORTEXECUTION_URL")!; ReportService2010.asmx
            //var user = "extRSAuth"; //Environment.GetEnvironmentVariable("SSRS_USER")!;
            //var pass = "This_IS_a_simpl_Passphrase"; // Environment.GetEnvironmentVariable("SSRS_PASSWORD")!;
            //var domain = ""; // Environment.GetEnvironmentVariable("SSRS_DOMAIN") ?? "";

            //// ---- IMPORTANT ----
            //// Replace this type with the one from your generated proxy.
            //// Example common name: Microsoft.ReportingServices.ReportExecutionService.ReportExecutionService
            //// ---------------------------------
            //var proxyTypeName = "Microsoft.ReportingServices.ReportExecutionService.ReportService";
            //var proxyType = Type.GetType(proxyTypeName, throwOnError: true)!;

            //object proxy = Activator.CreateInstance(proxyType)!;

            //// Set Url property (common name: Url)
            //proxyType.GetProperty("Url", BindingFlags.Public | BindingFlags.Instance)!
            //         .SetValue(proxy, serviceUrl);

            //// Set Credentials (common name: Credentials)
            //var cred = new NetworkCredential(user, pass, domain);
            //proxyType.GetProperty("Credentials", BindingFlags.Public | BindingFlags.Instance)!
            //         .SetValue(proxy, cred);

            //var client = new SsrsSoapClient(proxy);

            //// Example params (adjust to your report’s expected parameters)
            //var parameters = new Dictionary<string, string>
            //{
            //    ["StartDate"] = DateTime.UtcNow.AddDays(-30).ToString("yyyy-MM-dd"),
            //    ["EndDate"] = DateTime.UtcNow.ToString("yyyy-MM-dd")
            //};

            //byte[] pdf = client.RenderPdf(reportPath, parameters);

            //return File(pdf, "application/pdf", "report.pdf");
        }
    }
}
