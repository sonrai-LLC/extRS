using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using DataTables;
using ExtRS.Portal.Models;
using Sonrai.ExtRS;
using Sonrai.ExtRS.Models;
using Microsoft.AspNetCore.Authorization;

namespace ExtRS.Portal.Controllers
{
    [AllowAnonymous]
    public class StatsController : Controller
    {
        private readonly ILogger<StatsController> _logger;
        private readonly IConfiguration _configuration;
        private readonly SSRSConnection _connection;
        private readonly HttpClient _httpClient;
        private SSRSService _ssrs;
        private readonly IHttpContextAccessor _httpContextAccessor;

		public StatsController(ILogger<StatsController> logger, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
            _configuration = configuration;
            _httpClient = new HttpClient();
            _connection = new SSRSConnection(_configuration["ReportServerName"]!, _configuration["User"]!, AuthenticationType.ExtRSAuth);
            _ssrs = new SSRSService(_connection, _configuration, _httpContextAccessor!);
            _ssrs._conn.SqlAuthCookie = SSRSService.GetSqlAuthCookie(_httpClient, _configuration["User"]!, _configuration["extrspassphrase"]!, _connection.ReportServerName).Result;
        }

        public async Task<ActionResult> Stats() 
        {
            return View("Stats", new StatsView() { CurrentTab = "Stats", SystemInfo = await _ssrs.GetSystemInfo(), ReportExecutionStats = await _ssrs.GetReportExecutionStats(_configuration["defaultConnection"]!) });
        }
    }
}
