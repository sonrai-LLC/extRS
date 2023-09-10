using Microsoft.AspNetCore.Mvc;
using ExtRS.Portal.Models;
using ReportingServices.Api.Models;
using Sonrai.ExtRS;
using Sonrai.ExtRS.Models;
using System.Security.Policy;

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
            _connection = new SSRSConnection(_configuration["ReportServerName"]!, "ExtRSAuth", AuthenticationType.ExtRSAuth);
            _connection.SqlAuthCookie = SSRSService.GetSqlAuthCookie(_httpClient, _connection.Administrator, _configuration["extrspassphrase"]!, _connection.ServerName).Result;
            _ssrs = new SSRSService(_connection, _configuration);
        }

        public async Task<IActionResult> Reports(ReportsView view)
        {			
			List<Report> reports = await _ssrs.GetReports();

            foreach(var report in reports)
            {
                string uri = string.Format("https://{0}/ReportServer/Pages/ReportViewer.aspx?/Reports/{1}&rs:embed=true", _ssrs._conn.ServerName, report.Name);
                report.Uri = uri + "&Qs=" + EncryptionService.Encrypt(uri, _configuration["cle"]!);
            }

            ReportsView model = new ReportsView { Reports = reports, CurrentTab = "Reports", ReportServerName = _configuration["ReportServerName"]! };

            return View(model);
        }

        public async Task<IActionResult> Report(string reportName)
		{
            Report report = await _ssrs.GetReport(string.Format("path='/Reports/{0}'", reportName));

            string uri = string.Format("https://{0}/ReportServer/Pages/ReportViewer.aspx?/Reports/{1}&rs:embed=true", _ssrs._conn.ServerName, report.Name);
            report.Uri = uri + "&Qs=" + EncryptionService.Encrypt(uri, _configuration["cle"]!); // + "?Url=" + uri;

            ReportView view = new ReportView() { SelectedReport = report };
            return View("_Report", view);
        }
	}
}
