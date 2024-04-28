using Microsoft.AspNetCore.Mvc;
using ReportingServices.Api.Models;
using ExtRS.Portal.Models;
using Sonrai.ExtRS;
using Sonrai.ExtRS.Models;
using System.Diagnostics;
using System.Data;
using DataSet = ReportingServices.Api.Models.Subscription;
using Microsoft.Extensions.Azure;
using Microsoft.Identity.Client;

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

        public async Task<List<Subscription>> GetSubscriptions()
        {
            List<Subscription> subscriptions = await _ssrs.GetSubscriptions();
            foreach (var subscription in subscriptions)
            {
                string uri = string.Format(Url.ActionLink("GetSubscription", "Subscriptions", new { id = subscription.Id })!);
                subscription.Uri = uri + "&Qs=" + EncryptionService.Encrypt(uri, _configuration["cle"]!);
            }

            return subscriptions;
        }

        public async Task<IActionResult> Subscriptions()
        {
            var subscriptions = await GetSubscriptions();
            SubscriptionsView model = new SubscriptionsView() { CurrentTab = "Subscriptions", Subscriptions = subscriptions, ReportServerName = _configuration["ReportServerName"]! };
            return View("Subscriptions", model);
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
                            WeeklyRecurrence = new WeeklyRecurrence() { WeeksInterval = 1, DaysOfWeek = new DaysOfWeekSelector() { Friday = true }, WeeksIntervalSpecified = true },
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

        [HttpGet]
        public async Task<IActionResult> Subscription(SubscriptionView viewModel)
        {
            viewModel.Reports = await _ssrs.GetReports();
            viewModel.Subscription = new Subscription()
            {
                DeliveryExtension = "Report Server Email",
                EventType = "TimedSubscription",
                IsActive = true,
                IsDataDriven = false,
                Owner = "extRSAuth",
                ExtensionSettings = new ExtensionSettings()
                {
                    Extension = "Report Server Email",
                    ParameterValues = new List<ParameterValue>()
                    {
                        new ParameterValue() { Name = "TO", IsValueFieldReference = true },
                        new ParameterValue() { Name = "CC" },
                        new ParameterValue() { Name = "BCC" },
                        new ParameterValue() { Name = "ReplyTo" },
                        new ParameterValue() { Name = "Subject" },
                        new ParameterValue() { Name = "RenderFormat" },
                        new ParameterValue() { Name = "IncludeReport" },
                        new ParameterValue() { Name = "IncludeLink" },
                        new ParameterValue() { Name = "Priority" },
                        new ParameterValue() { Name = "Comment" }
                    }
                }
            };

            return View("_Subscription", viewModel);
        }

        public async Task<IActionResult> PostSubscription(SubscriptionView viewModel)     
        {
            viewModel.Subscription!.ExtensionSettings.ParameterValues[6].Value = viewModel.IncludeReport ? "True" : "False";
            viewModel.Subscription!.ExtensionSettings.ParameterValues[7].Value = viewModel.IncludeLink ? "True" : "False";

            foreach(var p in viewModel.Subscription!.ExtensionSettings.ParameterValues)
            {
                p.IsValueFieldReference = false;
            }

            ProcessSchedule(ref viewModel);

            await _ssrs.CreateSubscription(viewModel.Subscription!);
            var subscriptions = await GetSubscriptions();
            var subscriptionsViewModel = new SubscriptionsView() { Subscriptions = subscriptions, CurrentTab = "Subscriptions" };

            return View("Subscriptions", subscriptionsViewModel);
        }

        public SubscriptionView ProcessSchedule(ref SubscriptionView viewModel)
        {
            if (viewModel.Subscription!.Schedule.Definition.Recurrence.DailyRecurrence!.DaysInterval == null)
                viewModel.Subscription.Schedule.Definition!.Recurrence.DailyRecurrence = null;
            if (viewModel.Subscription!.Schedule.Definition.Recurrence.MonthlyDOWRecurrence!.MonthsOfYear == null)
                viewModel.Subscription.Schedule.Definition.Recurrence.MonthlyDOWRecurrence = null;
            if (viewModel.Subscription!.Schedule.Definition.Recurrence.MinuteRecurrence!.MinutesInterval == null)
                viewModel.Subscription.Schedule.Definition.Recurrence.MinuteRecurrence = null;
            if (viewModel.Subscription!.Schedule.Definition.Recurrence.WeeklyRecurrence!.WeeksInterval == null)
                viewModel.Subscription.Schedule.Definition.Recurrence.WeeklyRecurrence = null;
            if (viewModel.Subscription!.Schedule.Definition.Recurrence.MonthlyRecurrence!.Days == null)
                viewModel.Subscription.Schedule.Definition.Recurrence.MonthlyRecurrence = null;

            viewModel.Subscription!.Schedule.Definition.StartDateTime = viewModel.Subscription!.Schedule.Definition.StartDateTime!.Value
            .AddHours(viewModel.IsPM ? viewModel.ScheduleStartHours + 12 : viewModel.ScheduleStartHours)
            .AddMinutes(viewModel.ScheduleStartMinutes)
            .ToUniversalTime();

            if (viewModel.Subscription!.Schedule.Definition.Recurrence.MinuteRecurrence!.MinutesInterval != null
                && viewModel.RecurrenceHours != 0)
            {
                viewModel.Subscription!.Schedule.Definition.Recurrence.MinuteRecurrence!.MinutesInterval +=
                    (viewModel.RecurrenceHours * 60);
            }

            if (viewModel.ScheduleRecurrenceIsEveryWeekday)
            {
                viewModel.Subscription!.Schedule.Definition.Recurrence.WeeklyRecurrence!.DaysOfWeek.Monday = true;
                viewModel.Subscription.Schedule.Definition.Recurrence.WeeklyRecurrence!.DaysOfWeek.Tuesday = true;
                viewModel.Subscription.Schedule.Definition.Recurrence.WeeklyRecurrence!.DaysOfWeek.Wednesday = true;
                viewModel.Subscription.Schedule.Definition.Recurrence.WeeklyRecurrence!.DaysOfWeek.Thursday = true;
                viewModel.Subscription.Schedule.Definition.Recurrence.WeeklyRecurrence!.DaysOfWeek.Friday = true;
            }

            if (viewModel.ScheduleRecurrenceIsEveryWeekend)
            {
                viewModel.Subscription.Schedule.Definition.Recurrence.WeeklyRecurrence!.DaysOfWeek.Saturday = true;
                viewModel.Subscription.Schedule.Definition.Recurrence.WeeklyRecurrence!.DaysOfWeek.Sunday = true;
            }

            return viewModel;
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
