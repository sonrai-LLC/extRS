using Dapper;
using Microsoft.AspNetCore.Mvc;
using ExtRS.Portal.Models;
using ReportingServices.Api.Models;
using Sonrai.ExtRS;
using Sonrai.ExtRS.Models;
using System.Security.Policy;
using Microsoft.AspNetCore.Cors;
using System;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using static Microsoft.EntityFrameworkCore.Metadata.Internal.EntityType;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Text.Json;

namespace ExtRS.Portal.Controllers
{
    public class ChartsController : Controller
    {
        private readonly ILogger<ReportsController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public ChartsController(ILogger<ReportsController> logger, IConfiguration configuration, IHttpContextAccessor httpContextAccessor, SignInManager<ApplicationUser> signInManager)
        {
            _logger = logger;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _signInManager = signInManager;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Charts(ReportsView view)
        {
            ChartsView model = new ChartsView { Charts = new List<ChartView>(), CurrentTab = "Charts", HighChartsModel = await ReferenceDataService.GetGetVoteHubPollingData() };



            List<KeyValuePair<string, string>> approvalKeyVals = new();
            List<KeyValuePair<string, string>> disapprovalKeyVals = new();

            //int approvalCount = model.HighChartsModel.Approve.Count;
            // int propertyCount = 2; // Assuming we have two properties: Id and Name
            // string[,] approvalArray = new string[approvalCount, propertyCount];

            for (int i = 0; i < model.HighChartsModel.Approve.Count; i++)
            {
                approvalKeyVals.Add(new KeyValuePair<string, string>(model.HighChartsModel.Approve[i].Date.ToString(), model.HighChartsModel.Approve[i].Approve.ToString()));
            }

            //int disApprovalCount = model.HighChartsModel.Disapprove.Count;
            //string[,] disApprovalArray = new string[approvalCount, propertyCount];

            for (int i = 0; i < model.HighChartsModel.Approve.Count; i++)
            {
                disapprovalKeyVals.Add(new KeyValuePair<string, string>(model.HighChartsModel.Approve[i].Date.ToString(), model.HighChartsModel.Disapprove[i].Disapprove.ToString()));
            }

            model.HighChartsMarkupApproval = approvalKeyVals;
            model.HighChartsMarkupDisapproval = disapprovalKeyVals;

            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> Chart(string reportName, string id)
        {
            ChartView chart = new ChartView() { Markup = "" };
            return View("_Chart", chart);
        }
    }
}
