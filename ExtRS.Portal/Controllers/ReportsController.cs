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
            _connection = new SSRSConnection(_configuration["ReportServerName"]!, _configuration["User"]!, AuthenticationType.ExtRSAuth);
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

            uri += "&Qs=" + EncryptionService.Encrypt(uri, _configuration["cle"]!);
            report.Uri = uri;

            ReportView view = new ReportView() { SelectedReport = report };
            return View("_Report", view);
        }

        public async Task<IActionResult> ReportSnapshot(string id, string reportId, string creationDate)
        {
            creationDate = Convert.ToDateTime(creationDate).ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss");
            Report report = await _ssrs.GetReport(reportId);
            string uri = string.Format("https://{0}/ReportServer/Pages/ReportViewer.aspx?/Reports/{1}&rs:embed=true&rs:Snapshot={2}", _ssrs._conn.ReportServerName, report.Name, creationDate);
            uri += "&Qs=" + EncryptionService.Encrypt(uri, _configuration["cle"]!);

            ReportView view = new ReportView() { SelectedReport = new Report { Uri = uri } };
            return View("_Report", view);
        }

        public async Task<IActionResult> CreateReportSnapshot(string id)
        {
            await _ssrs.CreateReportSnapshot(id);
            List<HistorySnapshot> snapshots = await _ssrs.GetReportSnapshotHistory(id);
            return View("_SnapshotHistory", new SnapshotHistoryView { CurrentTab = "Reports", SnapShotCreated = true, HistorySnapshots = snapshots, ReportId = id });
        }

        public async Task<bool> DeleteReportSnapshotAjax(string id, string historyId)
        {
            bool isDeleted = await _ssrs.DeleteReportSnapshot(id, historyId);
            return isDeleted;
        }

        [HttpPost]
        public async Task CreateReportSnapshotAjax(string id)
        {
            await _ssrs.CreateReportSnapshot(id);
        }

        public async Task<IActionResult> SnapshotHistory(string id)
        {
            var snapshots = await _ssrs.GetReportSnapshotHistory(id);

            return View("_SnapshotHistory", new SnapshotHistoryView { HistorySnapshots = snapshots, CurrentTab = "Reports", ReportId = id });
        }      
    }
}
