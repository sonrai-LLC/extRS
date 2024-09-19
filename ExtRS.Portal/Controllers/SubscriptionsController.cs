using Microsoft.AspNetCore.Mvc;
using ReportingServices.Api.Models;
using ExtRS.Portal.Models;
using Sonrai.ExtRS;
using Sonrai.ExtRS.Models;
using System.Diagnostics;
using System.Data;
using Microsoft.Extensions.Azure;
using Microsoft.Identity.Client;
using Sonrai.ExtRS.Models.Enums;
using System.ComponentModel;
using System.Reflection;
using AngleSharp.Css;
using Microsoft.AspNetCore.Authorization;

namespace ExtRS.Portal.Controllers
{
    [Authorize]
    public class SubscriptionsController : Controller
    {
        private readonly ILogger<SubscriptionsController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly SSRSConnection _connection;
        private readonly HttpClient _httpClient;
        private SSRSService _ssrs;

        public SubscriptionsController(ILogger<SubscriptionsController> logger, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _httpClient = new HttpClient();
            _connection = new SSRSConnection(_configuration["ReportServerName"]!, _configuration["User"]!, AuthenticationType.ExtRSAuth);
            _ssrs = new SSRSService(_connection, _configuration, _httpContextAccessor);
            _ssrs._conn.SqlAuthCookie = SSRSService.GetSqlAuthCookie(_httpClient, _httpContextAccessor.HttpContext!.User!.Identity!.Name!, _configuration["extrspassphrase"]!, _connection.ReportServerName).Result;
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
                Owner = _httpContextAccessor.HttpContext!.User.Identity!.Name!,
                IsDataDriven = false,
                Description = description,
                Report = "/Reports/Team",
                IsActive = true,
                EventType = "TimedSubscription",
                ScheduleDescription = description,
                LastRunTime = DateTime.Now,
                DeliveryExtension = "Report Server Email",
                LocalizedDeliveryExtensionName = "Email",
                ModifiedBy = _httpContextAccessor.HttpContext!.User.Identity!.Name!,
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

            await _ssrs.SaveSubscription(newSubscription);
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
                Owner = _httpContextAccessor.HttpContext!.User.Identity!.Name!,
                ExtensionSettings = GetNewExtensionSettings(null)
            };

            return View("_Subscription", viewModel);
        }

        // refactor to populate for existing ParameterValues
        public ExtensionSettings GetNewExtensionSettings(List<ParameterValue>? extensionParameters)
        {
            if (extensionParameters != null)
            {
                extensionParameters = new List<ParameterValue>();
            }
            var to = extensionParameters.Where(x => x.Name == "TO").FirstOrDefault();
            var cc = extensionParameters.Where(x => x.Name == "CC").FirstOrDefault();
            var bcc = extensionParameters.Where(x => x.Name == "BCC").FirstOrDefault();
            var replyTo = extensionParameters.Where(x => x.Name == "ReplyTo").FirstOrDefault();
            var subject = extensionParameters.Where(x => x.Name == "Subject").FirstOrDefault();
            var renderFormat = extensionParameters.Where(x => x.Name == "RenderFormat").FirstOrDefault();
            var includeReport = extensionParameters.Where(x => x.Name == "IncludeReport").FirstOrDefault();
            var includeLink = extensionParameters.Where(x => x.Name == "IncludeLink").FirstOrDefault();
            var priority = extensionParameters.Where(x => x.Name == "Priority").FirstOrDefault();
            var comment = extensionParameters.Where(x => x.Name == "Comment").FirstOrDefault();

            return new ExtensionSettings()
            {
                Extension = "Report Server Email",
                ParameterValues = new List<ParameterValue>()
                {
                    new ParameterValue() { Name = "TO", IsValueFieldReference = true, Value = to?.Value! },
                    new ParameterValue() { Name = "CC", Value = cc?.Value! },
                    new ParameterValue() { Name = "BCC", Value = bcc?.Value! },
                    new ParameterValue() { Name = "ReplyTo", Value = replyTo?.Value! },
                    new ParameterValue() { Name = "Subject", Value = subject?.Value! },
                    new ParameterValue() { Name = "RenderFormat", Value = renderFormat?.Value! },
                    new ParameterValue() { Name = "IncludeReport", Value = includeReport?.Value! },
                    new ParameterValue() { Name = "IncludeLink", Value = includeLink?.Value! },
                    new ParameterValue() { Name = "Priority", Value = priority?.Value! },
                    new ParameterValue() { Name = "Comment", Value = comment?.Value! }
                }
            };
        }

        public async Task<IActionResult> PostSubscription(SubscriptionView viewModel)
        {
            if (!viewModel.Subscription.Schedule.Definition.EndDateSpecified)
            {
                viewModel.Subscription.Schedule.Definition.EndDate = null;
            }

            viewModel.Subscription!.ExtensionSettings.ParameterValues[6].Value = viewModel.IncludeReport ? "True" : "False";
            viewModel.Subscription!.ExtensionSettings.ParameterValues[7].Value = viewModel.IncludeLink ? "True" : "False";
            if (viewModel.Subscription.Schedule.Definition.EndDate != null)
            {
                viewModel.Subscription.Schedule.Definition.EndDateSpecified = true;
            }

            foreach (var p in viewModel.Subscription!.ExtensionSettings.ParameterValues)
            {
                p.IsValueFieldReference = false;
            }

            ProcessSchedule(ref viewModel);

            await _ssrs.SaveSubscription(viewModel.Subscription!);

            var subscriptions = await GetSubscriptions();
            var subscriptionsViewModel = new SubscriptionsView() { Subscriptions = subscriptions, CurrentTab = "Subscriptions" };

            return View("Subscriptions", subscriptionsViewModel);
        }

        public SubscriptionView ProcessSchedule(ref SubscriptionView viewModel)
        {
            if (viewModel.SelectedRecurrence != RecurrenceType.Daily)
            {
                viewModel.Subscription.Schedule.Definition!.Recurrence.DailyRecurrence = null;
            }
            if (viewModel.SelectedRecurrence != RecurrenceType.Hourly)
            {
                viewModel.Subscription.Schedule.Definition.Recurrence.MinuteRecurrence = null;
            }
            else
            {
                viewModel.Subscription!.Schedule.Definition.Recurrence.MinuteRecurrence!.MinutesInterval = viewModel.RecurrenceMinutes + (viewModel.RecurrenceHours * 60);
            }
            if (viewModel.SelectedRecurrence != RecurrenceType.Weekly)
            {
                viewModel.Subscription.Schedule.Definition.Recurrence.WeeklyRecurrence = null;
            }
            else
            {
                viewModel.Subscription.Schedule.Definition.Recurrence.WeeklyRecurrence!.WeeksIntervalSpecified = viewModel.Subscription.Schedule.Definition.Recurrence.WeeklyRecurrence!.WeeksInterval != null;
            }
            if (viewModel.SelectedRecurrence != RecurrenceType.Monthly)
            {
                viewModel.Subscription.Schedule.Definition.Recurrence.MonthlyDOWRecurrence = null;
                viewModel.Subscription.Schedule.Definition.Recurrence.MonthlyRecurrence = null;
            }

            viewModel.Subscription!.Schedule.Definition.StartDateTime = viewModel.Subscription!.Schedule.Definition.StartDateTime!.Value
            .AddHours(viewModel.IsPM ? viewModel.ScheduleStartHours + 12 : viewModel.ScheduleStartHours)
            .AddMinutes(viewModel.ScheduleStartMinutes);

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

            if (subscription.ExtensionSettings.ParameterValues.Count != 10)
            {
                subscription.ExtensionSettings = GetNewExtensionSettings(subscription.ExtensionSettings.ParameterValues);
            }

            view.IsPM = view.Subscription.Schedule.Definition.StartDateTime.Value.Hour >= 12;
            view.IsAM = !view.IsPM;
            view.ScheduleStartHours = view.Subscription.Schedule.Definition.StartDateTime!.Value.Hour - (view.IsPM ? 12 : 0);
            view.ScheduleStartMinutes = view.Subscription.Schedule.Definition.StartDateTime.Value.Minute;

            if (view.Subscription.Schedule.Definition.Recurrence.MonthlyRecurrence != null
                || view.Subscription.Schedule.Definition.Recurrence.MonthlyDOWRecurrence != null)
            {
                view.SelectedRecurrence = RecurrenceType.Monthly;
            }
            if (view.Subscription.Schedule.Definition.Recurrence.MonthlyRecurrence == null
                && view.Subscription.Schedule.Definition.Recurrence.MonthlyDOWRecurrence == null
                && view.Subscription.Schedule.Definition.Recurrence.WeeklyRecurrence != null
                && view.Subscription.Schedule.Definition.Recurrence.DailyRecurrence == null
                && view.Subscription.Schedule.Definition.Recurrence.MinuteRecurrence == null)
            {
                view.SelectedRecurrence = RecurrenceType.Weekly;
            }
            if (view.Subscription.Schedule.Definition.Recurrence.MonthlyRecurrence == null
             && view.Subscription.Schedule.Definition.Recurrence.MonthlyDOWRecurrence == null
             && view.Subscription.Schedule.Definition.Recurrence.WeeklyRecurrence == null
             && view.Subscription.Schedule.Definition.Recurrence.DailyRecurrence != null
             && view.Subscription.Schedule.Definition.Recurrence.MinuteRecurrence == null)
            {
                view.SelectedRecurrence = RecurrenceType.Daily;
            }
            if (view.Subscription.Schedule.Definition.Recurrence.MonthlyRecurrence == null
             && view.Subscription.Schedule.Definition.Recurrence.MonthlyDOWRecurrence == null
             && view.Subscription.Schedule.Definition.Recurrence.WeeklyRecurrence == null
             && view.Subscription.Schedule.Definition.Recurrence.DailyRecurrence == null
             && view.Subscription.Schedule.Definition.Recurrence.MinuteRecurrence != null)
            {
                view.SelectedRecurrence = RecurrenceType.Hourly;
                view.RecurrenceHours = (int)view.Subscription.Schedule.Definition.Recurrence.MinuteRecurrence!.MinutesInterval! / 60;
                view.RecurrenceMinutes = (int)view.Subscription.Schedule.Definition.Recurrence.MinuteRecurrence!.MinutesInterval! - (view.RecurrenceHours * 60);
            }
            if (view.Subscription.Schedule.Definition.Recurrence.MonthlyRecurrence == null
                && view.Subscription.Schedule.Definition.Recurrence.MonthlyDOWRecurrence == null
                && view.Subscription.Schedule.Definition.Recurrence.WeeklyRecurrence == null
                && view.Subscription.Schedule.Definition.Recurrence.DailyRecurrence == null
                && view.Subscription.Schedule.Definition.Recurrence.MinuteRecurrence == null)
            {
                view.SelectedRecurrence = RecurrenceType.Onetime;
            }

            if (view.Subscription.ExtensionSettings.ParameterValues.Count >= 7 &&
                view.Subscription.ExtensionSettings.ParameterValues[6].Value == "True")
            {
                view.IncludeReport = true;
            }

            if (view.Subscription.ExtensionSettings.ParameterValues.Count >= 8 &&
                view.Subscription.ExtensionSettings.ParameterValues[7].Value == "True")
            {
                view.IncludeLink = true;
            }

            return View("_Subscription", view);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorView { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
