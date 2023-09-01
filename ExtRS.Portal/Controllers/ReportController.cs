using ExtRS.Portal.Models;
using ExtRS.Portal.Models;
using Microsoft.AspNetCore.Mvc;
using Sonrai.ExtRS.Models;
using Sonrai.ExtRS;
using System.Diagnostics;
using IO.Swagger.Model;

namespace ExtRS.Portal.Controllers
{
	public class ReportController : Controller
    {
        private readonly ILogger<ReportController> _logger;
        private readonly IConfiguration _configuration;

        public ReportController(ILogger<ReportController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<IActionResult> Reports(ReportView view)
        {
            var httpClient = new HttpClient();
            SSRSConnection connection = new SSRSConnection("localhost", "ExtRSAuth", AuthenticationType.ExtRSAuth);
            connection.SqlAuthCookie = await SSRSService.GetSqlAuthCookie(httpClient, connection.Administrator, "", connection.ServerName);
            var ssrs = new SSRSService(connection);

            Report report = await ssrs.GetReport("path='/Reports/Team'");
            ReportView model = new ReportView { Report = report, CurrentTab = "Reports" };

            return View(model);
        }

        public IActionResult Report(ReportView view)
        {
            return View("Reports", view);
        }
    }
}
