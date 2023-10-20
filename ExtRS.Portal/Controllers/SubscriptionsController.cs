using Microsoft.AspNetCore.Mvc;
using ReportingServices.Api.Models;
using ExtRS.Portal.Models;
using Sonrai.ExtRS;
using Sonrai.ExtRS.Models;
using System.Diagnostics;
using System.Data;
using DataSet = ReportingServices.Api.Models.Subscription;

namespace ExtRS.Portal.Controllers
{
    public class SubscriptionsController : Controller
    {
        private readonly ILogger<SubscriptionsController> _logger;
        private readonly IConfiguration _configuration;
        private readonly SSRSConnection _connection;
        private readonly HttpClient _httpClient;
        private SSRSService _ssrs;

        public SubscriptionsController(ILogger<SubscriptionsController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            _httpClient = new HttpClient();
            _connection = new SSRSConnection(_configuration["ReportServerName"]!, "ExtRSAuth", AuthenticationType.ExtRSAuth);
            _connection.SqlAuthCookie = SSRSService.GetSqlAuthCookie(_httpClient, _connection.Administrator, _configuration["extrspassphrase"]!, _connection.ReportServerName).Result;
            _ssrs = new SSRSService(_connection, _configuration);
        }
        public async Task<IActionResult> Subscriptions()
        {
            List<Subscription> subscriptions = await _ssrs.GetSubscriptions();
            foreach (var subscription in subscriptions)
            {
                string uri = string.Format(Url.ActionLink("Subscription", "Subscriptions", new { id = subscription.Id })!);
                subscription.Uri = uri + "&Qs=" + EncryptionService.Encrypt(uri, _configuration["cle"]!);
            }

            SubscriptionsView model = new SubscriptionsView() { CurrentTab = "Subscriptions", Subscriptions = subscriptions, ReportServerName = _configuration["ReportServerName"]! };
            return View(model);
        }

        public async Task<IActionResult> Subscription(string? subscriptionName, string id)
        {
            Subscription subscription;
            if (subscriptionName is not null)
            {
                subscription = await _ssrs.GetSubscription(string.Format("path='/Subscriptions/{0}'", subscriptionName));
            }
            else
            {
                subscription = await _ssrs.GetSubscription(id);
            }

            //string uri = string.Format("https://{0}/Reportserver/Subscriptions?%Subscriptions/{1}", _ssrs._conn.ReportServerName, subscription.Name);
            //subscription.Uri = uri + "&Qs=" + EncryptionService.Encrypt(uri, _configuration["cle"]!);

            SubscriptionView view = new SubscriptionView { CurrentTab = "Subscriptions", Subscription = subscription, ReportServerName = _configuration["ReportServerName"]! };
            return View("_Subscription", view);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorView { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
