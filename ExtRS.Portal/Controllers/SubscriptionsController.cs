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
            _connection = new SSRSConnection(_configuration["ReportServerName"]!, _configuration["User"]!, AuthenticationType.ExtRSAuth);
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

        [HttpGet]
        public async Task<IActionResult> GetManageSubscriptionModal(string id)
        {
            Subscription subscription;
            subscription = await _ssrs.GetSubscription(id);

            return PartialView("_ManageSubscription", subscription);
        }

        public async Task<string> CreateSubscription(string id)
        {
//            Subscription subscription = new()
//            {
//                {
//    "Owner": "extRSAuth",
//    "IsDataDriven": false,
//    "Description": "string...",
//    "Report": "/Reports/Team",
//    "IsActive": true,
//    "EventType": "TimedSubscription",
//    "ScheduleDescription": "string...",
//    "LastRunTime": "2023-04-13T15:51:04Z",
//    "LastStatus": "string...",
//    "DeliveryExtension": "Report Server Email",
//    "LocalizedDeliveryExtensionName": "Email",
//    "ModifiedBy": "extRSAuth",
//    "ModifiedDate": "2023-04-13T15:51:04Z",
//    "Schedule": {
//                "ScheduleID": null,
//        "Definition": {
//                    "StartDateTime": "2021-01-01T02:00:00-07:00",
//            "EndDate": "0001-01-01T00:00:00Z",
//            "EndDateSpecified": false,
//            "Recurrence": {
//                        "MinuteRecurrence": null,
//                "DailyRecurrence": null,
//                "WeeklyRecurrence": null,
//                "MonthlyRecurrence": null,
//                "MonthlyDOWRecurrence": null
//            }
//                }
//            },
//    "DataQuery": null,
//    "ExtensionSettings": {
//                "Extension": "DeliveryExtension",
//        "ParameterValues": [
//            {
//                    "Name": "TO",
//                "Value": "cfitzg1983@gmail.com",
//                "IsValueFieldReference": false
//            },
//            {
//                    "Name": "IncludeReport",
//                "Value": "true",
//                "IsValueFieldReference": false
//            },
//            {
//                    "Name": "Subject",
//                "Value": "true",
//                "IsValueFieldReference": false
//            },
//            {
//                    "Name": "RenderFormat",
//                "Value": "PDF",
//                "IsValueFieldReference": false
//            }
//        ]
//    },
//    "ParameterValues": []
//}

   // };
            
           // bool isCreated = await _ssrs.CreateSubscription(subscription);

            //foreach (var subscription in subscriptions)
            //{
            //    string uri = string.Format(Url.ActionLink("Subscription", "Subscriptions", new { id = subscription.Id })!);
            //    subscription.Uri = uri + "&Qs=" + EncryptionService.Encrypt(uri, _configuration["cle"]!);
            //}

            //SubscriptionsView model = new SubscriptionsView() { CurrentTab = "Subscriptions", Subscriptions = subscriptions, ReportServerName = _configuration["ReportServerName"]! };
            //string viewHtml = GetSubscriptiontHtml(reportId, snapshots);

            return "";
        }

        public async Task<IActionResult> DeleteSubscription(string id)
        {
            bool isDeleted = await _ssrs.DeleteSubscription(id);
            List<Subscription> subscriptions = await _ssrs.GetSubscriptions();

            foreach (var subscription in subscriptions)
            {
                string uri = string.Format(Url.ActionLink("Subscription", "Subscriptions", new { id = subscription.Id })!);
                subscription.Uri = uri + "&Qs=" + EncryptionService.Encrypt(uri, _configuration["cle"]!);
            }

            SubscriptionsView model = new SubscriptionsView() { CurrentTab = "Subscriptions", Subscriptions = subscriptions, ReportServerName = _configuration["ReportServerName"]! };
            return View("Subscriptions", model);
        }

        //public async Task<IActionResult> Subscription(string? subscriptionName, string id)
        //{
        //    Subscription subscription;
        //    if (subscriptionName is not null)
        //    {
        //        subscription = await _ssrs.GetSubscription(string.Format("path='/Subscriptions/{0}'", subscriptionName));
        //    }
        //    else
        //    {
        //        subscription = await _ssrs.GetSubscription(id);
        //    }

        //    //string uri = string.Format("https://{0}/Reportserver/Subscriptions?%Subscriptions/{1}", _ssrs._conn.ReportServerName, subscription.Name);
        //    //subscription.Uri = uri + "&Qs=" + EncryptionService.Encrypt(uri, _configuration["cle"]!);

        //    SubscriptionView view = new SubscriptionView { CurrentTab = "Subscriptions", Subscription = subscription, ReportServerName = _configuration["ReportServerName"]! };
        //    return View("_Subscription", view);
        //}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorView { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
