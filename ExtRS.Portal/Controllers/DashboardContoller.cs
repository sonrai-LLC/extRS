using Microsoft.AspNetCore.Mvc;
using ReportingServices.Api.Models;
using ExtRS.Portal.Models;
using Sonrai.ExtRS;
using Sonrai.ExtRS.Models;

namespace ExtRS.Portal.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ILogger<DashboardController> _logger;
        private readonly IConfiguration _configuration;
        private readonly SSRSConnection _connection;
        private readonly HttpClient _httpClient;
        private SSRSService _ssrs;

        public DashboardController(ILogger<DashboardController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            _httpClient = new HttpClient();
            _connection = new SSRSConnection("localhost", "ExtRSAuth", AuthenticationType.ExtRSAuth);
            _connection.SqlAuthCookie = SSRSService.GetSqlAuthCookie(_httpClient, _connection.Administrator, _configuration["passphrase"]!, _connection.ServerName).Result;
            _ssrs = new SSRSService(_connection);
        }

        public async Task<IActionResult> Dashboard()
        {
            Report report = await _ssrs.GetReport("path='/Reports/Team'");
            DashboardView model = new DashboardView { Report = report, CurrentTab = "Dashboard" };

            return View(model);
        }

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorView { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}
