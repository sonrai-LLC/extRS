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

namespace ExtRS.Portal.Controllers
{
    [Authorize]
    public class ReportsController : Controller
    {
        private readonly ILogger<ReportsController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly SSRSConnection _connection;
        private readonly HttpClient _httpClient;
        private SSRSService _ssrs;

        public ReportsController(ILogger<ReportsController> logger, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _httpClient = new HttpClient();
            _connection = new SSRSConnection(_configuration["ReportServerName"]!, "extRSAuth", AuthenticationType.ExtRSAuth);   
            _ssrs = new SSRSService(_connection, _configuration);

            _ssrs._conn.SqlAuthCookie = ""; // cleare the cookie
            var loggedInIsWho = _ssrs.GetRSSessionUser().Result;

            _ssrs._conn.SqlAuthCookie = _ssrs.GetSqlAuthCookie(_httpClient, "extRSAuth", _configuration["extrspassphrase"]!, _connection.ReportServerName).Result;

           
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
            if (reportName is not null)
            {
                report = await _ssrs.GetReport(string.Format("path='/Reports/{0}'", reportName));
            }
            else
            {
                report = await _ssrs.GetReport(id);
            }
            string uri = string.Format("https://{0}/ReportServer/Pages/ReportViewer.aspx?/Reports/{1}&rs:embed=true&UserName={2}", _ssrs._conn.ReportServerName, report.Name, "extRSAuth");

            uri += "&Qs=" + EncryptionService.Encrypt(uri, _configuration["cle"]!);
            report.Uri = uri;

            ReportView view = new ReportView() { SelectedReport = report };


            Response.Cookies.Delete("sqlAuthCookie");
            return View("_Report", view);
        }

        public async Task<IActionResult> ReportSnapshot(string reportId, string creationDate)
        {
            creationDate = Convert.ToDateTime(creationDate).ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss");
            Report report = await _ssrs.GetReport(reportId);
            string uri = string.Format("https://{0}/ReportServer/Pages/ReportViewer.aspx?/Reports/{1}&rs:embed=true&UserName={2}&rs:Snapshot={3}", _ssrs._conn.ReportServerName, report.Name, "extRSAuth", creationDate);
            uri += "&Qs=" + EncryptionService.Encrypt(uri, _configuration["cle"]!);

            ReportView view = new ReportView() { SelectedReport = new Report { Uri = uri } };
            return View("_Report", view);
        }

        public async Task<string> CreateReportSnapshotAjax(string reportId)
        {
            var result = await _ssrs.CreateReportSnapshot(reportId);
            List<HistorySnapshot> snapshots = await _ssrs.GetReportSnapshots(reportId);
            string viewHtml = GetSnapshotsHtml(reportId, snapshots);

            return viewHtml;
        }

        public async Task<string> DeleteReportSnapshotAjax(string reportId, string historyId)
        {
            bool isDeleted = await _ssrs.DeleteReportSnapshot(reportId, historyId);
            List<HistorySnapshot> snapshots = await _ssrs.GetReportSnapshots(reportId);
            string viewHtml = GetSnapshotsHtml(reportId, snapshots);

            return viewHtml;
        }

        public static string GetSnapshotsHtml(string reportId, List<HistorySnapshot> snapshots)
        {
            string viewHtml = "";

            foreach (var snapshot in snapshots)
            {
                viewHtml +=
                @"<div class=""bg-dark"" style=""box-shadow: 2.5px 5px 4px #888888;"">
                    <span id=" + snapshot.Id + @" class=""nav_link"" style=""float:right"" onclick=""confirmDeleteReportSnapshot('" + @reportId + "', '" + @snapshot.Id + @"', 'Snapshot deleted');"">
                        <a href = ""javascript:;"">
                            x
                        </a>
                    </span>" +
                    string.Format(@"<a href='/reports/reportsnapshot?reportId={0}&creationDate={1}' class=""nav_link""><i class=""bx bx-line-chart""></i><span class=""nav_name"">" + snapshot.CreationDate + @"</span></a>
                    </div>", reportId, snapshot.CreationDate);
            }

            return viewHtml;
        }

        public async Task<IActionResult> SnapshotHistory(string reportId)
        {
            var snapshots = await _ssrs.GetReportSnapshots(reportId);

            return View("_SnapshotHistory", new SnapshotHistoryView { HistorySnapshots = snapshots, CurrentTab = "Reports", ReportId = reportId });
        }
    }
}
