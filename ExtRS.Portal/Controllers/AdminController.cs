using Microsoft.AspNetCore.Mvc;
using ExtRS.Portal.Models;
using ReportingServices.Api.Models;
using Sonrai.ExtRS;
using Sonrai.ExtRS.Models;
using Microsoft.AspNetCore.Authorization;

namespace ExtRS.Portal.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly SSRSConnection _connection;
        private readonly HttpClient _httpClient;
        private SSRSService _ssrs;

        public AdminController(ILogger<AdminController> logger, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _httpClient = new HttpClient();
            _connection = new SSRSConnection(_configuration["ReportServerName"]!, _httpContextAccessor.HttpContext.User.Identity.Name!, AuthenticationType.ExtRSAuth);
            _ssrs = new SSRSService(_connection, _configuration);
            _ssrs._conn.SqlAuthCookie = _ssrs.GetSqlAuthCookie(_httpClient, _httpContextAccessor.HttpContext.User.Identity.Name!, _configuration["extrspassphrase"]!, _connection.ReportServerName).Result;
        }

        public async Task<IActionResult> Admin()
        {
            Report report = await _ssrs.GetReport("path='/Reports/Team'");
            AdminView model = new AdminView { AdminID = Guid.NewGuid(), CurrentTab = "Admin" };

            return View(model);
        }
    }
}
