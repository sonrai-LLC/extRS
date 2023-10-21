using Microsoft.AspNetCore.Mvc;
using ExtRS.Portal.Models;
using ReportingServices.Api.Models;
using Sonrai.ExtRS;
using Sonrai.ExtRS.Models;
using System.Security.Policy;
using Microsoft.AspNetCore.Cors;

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
            _connection.SqlAuthCookie = SSRSService.GetSqlAuthCookie(_httpClient, _connection.Administrator, _configuration["extrspassphrase"]!, _connection.ReportServerName).Result;
            _ssrs = new SSRSService(_connection, _configuration);
        }

        public async Task<IActionResult> Reports(ReportsView view)
        {
            List<Report> reports = await _ssrs.GetReports();

            foreach (var report in reports)
            {
                
                string uri = string.Format(Url.ActionLink("Report", "Reports", new { reportName = report.Name })!);
                report.Uri = uri + "&Qs=" + EncryptionService.Encrypt(uri, _configuration["cle"]!);
            }

            ReportsView model = new ReportsView { Reports = reports, CurrentTab = "Reports", ReportServerName = _configuration["ReportServerName"]! };

            return View(model);
        }

        public async Task<IActionResult> Report(string reportName, string id)
        { 
            Report report;
            if(reportName is not null)
            {
                 report = await _ssrs.GetReport(string.Format("path='/Reports/{0}'", reportName));
            }
            else
            {
                report = await _ssrs.GetReport(id);
            }
            string uri = string.Format("https://{0}/ReportServer/Pages/ReportViewer.aspx?/Reports/{1}&rs:embed=true", _ssrs._conn.ReportServerName, report.Name);
            report.Uri = uri + "&Qs=" + EncryptionService.Encrypt(uri, _configuration["cle"]!);

            ReportView view = new ReportView() { SelectedReport = report };
            return View("_Report", view);
        }

        public async Task<IActionResult> SnapshotHistory(string id)
        {
            List<HistorySnapshot> snapshots = await _ssrs.GetReportSnapshotHistory(id);

            foreach (var snapshot in snapshots)
            {

               // string uri = string.Format(Url.ActionLink("Report", "Reports", new { reportName = snapshot.Name })!);
                //snapshot.Uri = uri + "&Qs=" + EncryptionService.Encrypt(uri, _configuration["cle"]!);
            }

            //ReportsView model = new ReportsView { Reports = reports, CurrentTab = "Reports", ReportServerName = _configuration["ReportServerName"]! };

            return View("_SnapshotHistory", new SnapshotHistoryView { HistorySnapshots = snapshots, CurrentTab = "Reports" });
        }      
    }
}
