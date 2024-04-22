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
            _connection.SqlAuthCookie = SSRSService.GetSqlAuthCookie(_httpClient, _configuration["User"]!, _configuration["extrspassphrase"]!, _connection.ReportServerName).Result;
            _ssrs = new SSRSService(_connection, _configuration);
        }

        public async Task<IActionResult> Subscriptions()
        {
            List<Subscription> subscriptions = await _ssrs.GetSubscriptions();
            foreach (var subscription in subscriptions)
            {
                string uri = string.Format(Url.ActionLink("GetSubscription", "Subscriptions", new { id = subscription.Id })!);
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

        public async Task<string> CreateSubscriptionAjax(string id, string email = "colin@sonrai.io", string subject = "", string description = "", string renderFormat = "PDF")
        {
            Subscription newSubscription = new Subscription()
            {
                Id = new Guid(),
                Owner = "extRSAuth",
                IsDataDriven = false,
                Description = description,
                Report = "/Reports/Team",
                IsActive = true,
                EventType = "TimedSubscription",
                ScheduleDescription = description,
                LastRunTime = DateTime.Now,
                DeliveryExtension = "Report Server Email",
                LocalizedDeliveryExtensionName = "Email",
                ModifiedBy = "extRSAuth",
                ModifiedDate = DateTime.Now,
                Uri = "",
                LastStatus = "",
                Schedule = new Schedule()
                {
                    Definition = new ScheduleDefinition()
                    {
                        StartDateTime = DateTime.Now,
                        EndDate = DateTime.Now.AddDays(10),
                        EndDateSpecified = true,
                        Recurrence = new ScheduleRecurrence()
                        {
                            MinuteRecurrence = new MinuteRecurrence() { MinutesInterval = 1000 } //,
                            //DailyRecurrence = new DailyRecurrence() { DaysInterval = 100 },
                            //WeeklyRecurrence = new WeeklyRecurrence() { WeeksInterval = 10, DaysOfWeek = new DaysOfWeekSelector() { Friday = true, Saturday = true }, WeeksIntervalSpecified = true },
                            //MonthlyRecurrence = new MonthlyRecurrence() { MonthsOfYear = new MonthsOfYearSelector() { July = true, April = true }, Days = "1" },
                            //MonthlyDOWRecurrence = new MonthlyDOWRecurrence()
                            //{
                            //    MonthsOfYear = new MonthsOfYearSelector() { July = true, April = true },
                            //    DaysOfWeek = new DaysOfWeekSelector() { Friday = true, Saturday = true }
                            //}
                        }
                    }
                },
                ExtensionSettings = new ExtensionSettings()
                {
                    Extension = "DeliveryExtension",
                    ParameterValues = new List<ParameterValue>()
                    {
                        new ParameterValue() { Name = "TO", Value = email, IsValueFieldReference = false },
                        new ParameterValue() { Name = "IncludeReport", Value = "true", IsValueFieldReference = false },
                        new ParameterValue() { Name = "Subject", Value = subject, IsValueFieldReference = false },
                        new ParameterValue() { Name = "RenderFormat", Value = renderFormat, IsValueFieldReference = false }
                    }
                },
                ParameterValues = new List<ParameterValue>()
            };

            await _ssrs.CreateSubscription(newSubscription);
            List<Subscription> subscriptions = await _ssrs.GetSubscriptions();

            foreach (var subscription in subscriptions)
            {
                string uri = string.Format(Url.ActionLink("GetSubscription", "Subscriptions", new { id = subscription.Id })!);
                subscription.Uri = uri + "&Qs=" + EncryptionService.Encrypt(uri, _configuration["cle"]!);
            }

            return GetSubscriptionsHtml(subscriptions);
        }

        public async Task<IActionResult> DeleteSubscriptionAjax(string id)
        {
            bool isDeleted = await _ssrs.DeleteSubscription(id);
            List<Subscription> subscriptions = await _ssrs.GetSubscriptions();

            foreach (var subscription in subscriptions)
            {
                string uri = string.Format(Url.ActionLink("GetSubscription", "Subscriptions", new { id = subscription.Id })!);
                subscription.Uri = uri + "&Qs=" + EncryptionService.Encrypt(uri, _configuration["cle"]!);
            }

            SubscriptionsView model = new SubscriptionsView() { CurrentTab = "Subscriptions", Subscriptions = subscriptions, ReportServerName = _configuration["ReportServerName"]! };
            return View("Subscriptions", model);
        }

        public static string GetSubscriptionsHtml(List<Subscription> subscriptions)
        {
                string viewHtml = "";

                foreach (var subscription in subscriptions)
                {
                    viewHtml +=
                    string.Format(@"<div id='subscriptionDialog{0}' class=""dialog"" style=""display: none""></div>
                    <div class=""bg-dark"" style=""box-shadow: 2.5px 5px 4px #888888;"">
                    <span id={0} class=""nav_link"" style=""float:right"" onclick=""asyncManageSubscriptionModal('{0}');"">
                    <a href = ""#"" >
                        ...
                    </a>
                    </span><a href='{1}' class=""nav_link"")><i class=""bx bx-mail-send""></i><span class=""nav_name"">{2}</span></a>
                    </div>", @subscription.Id, subscription.Uri, @subscription.Description);
                }

                return viewHtml;
        }

        [HttpPost]
        public async Task<IActionResult> PostSubscription(SubscriptionView viewModel)
        {
            viewModel.Subscription = new Subscription()
            {
                Report = "Reports/" + viewModel.SelectedReport.Name,
                DeliveryExtension = "Report Server Email",
                EventType = "TimedSubscription",
                Description = viewModel.SelectedReport.Description, 
                IsDataDriven = false,
                IsActive = true,
                ExtensionSettings = new ExtensionSettings()
                {

                }

            };

//            "Report": "/Folder Name/Report Name", 
//"DataQuery": null,
//"DeliveryExtension": "Report Server Email",
//"EventType": "TimedSubscription",
//"Description": "test3",


//"Schedule": { "Definition": { "EndDate": "0001-01-01T00:00:00Z", "EndDateSpecified": false, "Recurrence": { "DailyRecurrence": { "DaysInterval": 1 }, "MinuteRecurrence": null, "MonthlyDOWRecurrence": null, "MonthlyRecurrence": null, "WeeklyRecurrence": null }, "StartDateTime": "2020-09-04T02:00:00-05:00" }, "ScheduleID": null }, "ScheduleDescription": "At 2:00 AM every day, starting 9/4/2020" }


//"ExtensionSettings": {
//  "Extension": "Report Server Email",
//  "ParameterValues": [
//    {"IsValueFieldReference": false, "Name": "TO", "Value": "userName" },
//    {"IsValueFieldReference": false, "Name": "IncludeReport", "Value": "True" },
//    { "IsValueFieldReference": false, "Name": "RenderFormat", "Value": "PDF" }, 
//    { "IsValueFieldReference": false, "Name": "Subject", "Value": "@ReportName was executed at @ExecutionTime" },
//    { "IsValueFieldReference": false, "Name": "IncludeLink", "Value": "True" }, 
//    { "IsValueFieldReference": false, "Name": "Priority", "Value": "NORMAL" }
//  ]}, 

//"LocalizedDeliveryExtensionName": "E-Mail",
//"IsActive": true, 
//"IsDataDriven": false, 
//"LastRunTime": null, 
//"LastStatus": "New Subscription",  
//"ModifiedBy": "extRSAuth", 
//"ModifiedDate": "2020-09-04T13:45:51.343-05:00", 
//"Owner": "extRSAuth", 




//"ParameterValues": [], 


            await _ssrs.CreateSubscription(viewModel.Subscription!);
            
            return await Subscriptions();
        }

        [HttpGet]
        public async Task<IActionResult> Subscription(SubscriptionView viewModel)
        {
            viewModel.Reports = await _ssrs.GetReports();

            return View("_Subscription", viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> GetSubscription(string id, string? subscriptionName)
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

            List<Report> reports = await _ssrs.GetReports();

            //string uri = string.Format("https://{0}/Reportserver/Subscriptions?%Subscriptions/{1}", _ssrs._conn.ReportServerName, subscription.Name);
            //subscription.Uri = uri + "&Qs=" + EncryptionService.Encrypt(uri, _configuration["cle"]!);

            SubscriptionView view = new SubscriptionView { CurrentTab = "Subscriptions", Subscription = subscription, ReportServerName = _configuration["ReportServerName"]!, Reports = reports };
            return View("_Subscription", view);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorView { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
