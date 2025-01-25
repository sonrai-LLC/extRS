using Microsoft.AspNetCore.Mvc;
using ReportingServices.Api.Models;
using ExtRS.Portal.Models;
using Sonrai.ExtRS;
using Sonrai.ExtRS.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Principal;

namespace ExtRS.Portal.Controllers
{
    [AllowAnonymous]
    public class DashboardController : Controller
    {
        private readonly ILogger<DashboardController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly SSRSConnection _connection;
        private readonly HttpClient _httpClient;
        private SSRSService _ssrs;

		public DashboardController(ILogger<DashboardController> logger, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _httpClient = new HttpClient();
            _connection = new SSRSConnection(_configuration["ReportServerName"]!, _httpContextAccessor.HttpContext!.User.Identity!.Name!, AuthenticationType.ExtRSAuth); //_httpContextAccessor.HttpContext.User.Identity.Name!
			_ssrs = new SSRSService(_connection, _configuration, _httpContextAccessor);
		}

        [AllowAnonymous]
        public async Task<IActionResult> Dashboard()
        {
            DashboardView model = new DashboardView { CurrentTab = "Dashboard" };
            return View(model);
        }
    }
}
