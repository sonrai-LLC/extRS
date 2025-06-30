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
            ChartsView model = new ChartsView { Charts = new List<ChartView>(), CurrentTab = "HighCharts", HighChartsModel = await ReferenceDataService.GetGetVoteHubPollingData(_configuration["defaultConnection"]!) };
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
