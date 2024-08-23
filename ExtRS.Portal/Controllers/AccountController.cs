using Microsoft.AspNetCore.Mvc;
using ExtRS.Portal.Models;
using ReportingServices.Api.Models;
using Sonrai.ExtRS;
using Sonrai.ExtRS.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.Net;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ExtRS.Portal.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly SSRSConnection _connection;
        private readonly HttpClient _httpClient;
        private SSRSService _ssrs;

        public AccountController(ILogger<AccountController> logger, IConfiguration configuration, IHttpContextAccessor httpContextAccessor, SignInManager<ApplicationUser> signInManager)
        {
            _logger = logger;
            _configuration = configuration;
            _signInManager = signInManager;
            _httpContextAccessor = httpContextAccessor;
            _httpClient = new HttpClient();
            _connection = new SSRSConnection(_configuration["ReportServerName"]!, _httpContextAccessor.HttpContext!.User.Identity!.Name!, AuthenticationType.ExtRSAuth);
            _ssrs = new SSRSService(_connection, _configuration);
            _ssrs._conn.SqlAuthCookie = _ssrs.GetSqlAuthCookie(_httpClient, _httpContextAccessor.HttpContext.User.Identity.Name!, _configuration["extrspassphrase"]!, _connection.ReportServerName).Result;
        }

        public async Task<IActionResult> Login() // TODO: implement Scaffolding (harder than it seems like it should be...)
        {
            _logger.LogInformation("User logged in.");

            return View("Dashboard", "Dashboard");
        }

        public async Task<IActionResult> Logout()
        {

			await _ssrs.DeleteSession();
			var cookieContent = Request.Cookies["sqlAuthCookie"];
			Response.Cookies.Delete("sqlAuthCookie");
			await _signInManager.SignOutAsync();
			
			return RedirectToAction("Subscriptions", "Subscriptions");
        }
    }
}
