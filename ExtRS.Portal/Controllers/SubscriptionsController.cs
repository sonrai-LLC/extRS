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
    public class SubscriptionsController : Controller
    {
        private readonly ILogger<SubscriptionsController> _logger;
        private readonly IConfiguration _configuration;

        public SubscriptionsController(ILogger<SubscriptionsController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<IActionResult> Subscriptions(SubscriptionsView view)
        {
            var _httpClient = new HttpClient();
            SSRSConnection connection = new SSRSConnection("localhost", "ExtRSAuth", AuthenticationType.ExtRSAuth);
            connection.SqlAuthCookie = await SSRSService.GetSqlAuthCookie(_httpClient, connection.Administrator, "", connection.ServerName);
            var ssrs = new SSRSService(connection);

            //Report report = await ssrs.GetReport("path='/Reports/Team'");
            SubscriptionsView model = new SubscriptionsView { CurrentTab = "Subscriptions" };

            return View(model);
        }

        public IActionResult Subscription(ReportsView view)
        {
            return View("Subscription", view);
        }
    }
}
