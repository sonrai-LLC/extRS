using ExtRS.Portal.Models;
using ExtRS.Portal.Models;
using Microsoft.AspNetCore.Mvc;
using Sonrai.ExtRS.Models;
using Sonrai.ExtRS;
using System.Diagnostics;
using EditorNetCoreDemo.Controllers;
using IO.Swagger.Model;

namespace ExtRS.Portal.Controllers
{
    public class SubscriptionController : Controller
    {
        private readonly ILogger<SubscriptionController> _logger;
        private readonly IConfiguration _configuration;

        public SubscriptionController(ILogger<SubscriptionController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<IActionResult> Subscriptions(SubscriptionView view)
        {
            var httpClient = new HttpClient();
            SSRSConnection connection = new SSRSConnection("localhost", "ExtRSAuth", AuthenticationType.ExtRSAuth);
            connection.SqlAuthCookie = await SSRSService.GetSqlAuthCookie(httpClient, connection.Administrator, "", connection.ServerName);
            var ssrs = new SSRSService(connection);

            //Report report = await ssrs.GetReport("path='/Reports/Team'");
            SubscriptionView model = new SubscriptionView { CurrentTab = "Subscriptions" };

            return View(model);
        }

        public IActionResult Subscription(ReportView view)
        {
            return View("Subscription", view);
        }
    }
}
