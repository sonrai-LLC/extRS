using ExtRS.Portal.Models;
using Microsoft.AspNetCore.Mvc;
using Sonrai.ExtRS.Models;
using Sonrai.ExtRS;
using System.Diagnostics;
using IO.Swagger.Model;
using System.IO;
using Microsoft.AspNetCore.Authorization;
using ExtRS.Portal.Areas.Identity.Pages.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ExtRS.Portal.Controllers
{
	public class ReportsController : Controller
    {
        private readonly ILogger<ReportsController> _logger;
        private readonly IConfiguration _configuration;
        private readonly SSRSConnection _connection;
        private readonly HttpClient _httpClient;
        private SSRSService _ssrs;

		public ReportsController(ILogger<ReportsController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
			_httpClient = new HttpClient();
			_connection = new SSRSConnection("localhost", "ExtRSAuth", AuthenticationType.ExtRSAuth);
			_connection.SqlAuthCookie = SSRSService.GetSqlAuthCookie(_httpClient, _connection.Administrator, "", _connection.ServerName).Result;
			_ssrs = new SSRSService(_connection);
		}

        public async Task<IActionResult> Reports(ReportsView view)
        {			
			List<Report> reports = await _ssrs.GetReports();
            ReportsView model = new ReportsView { Reports = reports, CurrentTab = "Reports" };

            return View(model);
        }

        public async Task<IActionResult> Report(string reportName)
		{
            Report report = await _ssrs.GetReport(string.Format("path='/Reports/{0}'", reportName));
            ReportsView view = new ReportsView() { SelectedReport = report };
            return View("_Report", view);
        }

		public async Task<IActionResult> Login()
		{
            ViewData.Clear();
            return View("_LoginPartial");
		}
	}
}
