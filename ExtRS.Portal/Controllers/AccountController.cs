using Microsoft.AspNetCore.Mvc;
using ExtRS.Portal.Models;
using ReportingServices.Api.Models;
using Sonrai.ExtRS;
using Sonrai.ExtRS.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.Net;

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
            _connection = new SSRSConnection(_configuration["ReportServerName"]!, "extRSAuth", AuthenticationType.ExtRSAuth);
            _ssrs = new SSRSService(_connection, _configuration);
            _ssrs._conn.SqlAuthCookie = _ssrs.GetSqlAuthCookie(_httpClient, "extRSAuth", _configuration["extrspassphrase"]!, _connection.ReportServerName).Result;
        }

        public async Task<IActionResult> Login() // TODO: implement Scaffolding (harder than it seems like it should be...)
        {
           //await _signInManager.SignInAsync();
            _logger.LogInformation("User logged in.");
            await _ssrs.DeleteSession();
            _logger.LogInformation("User logged out of SSRS.");

            return View("Dashboard", "Dashboard");
        }

        public async Task<IActionResult> Logout()
        {

            //await _ssrs.DeleteSession();
                  
            //_ssrs._conn.SqlAuthCookie = "";
            //await _ssrs.DeleteSession();

            await _signInManager.SignOutAsync();

            HttpContext.Response.Cookies.Delete("sqlAuthCookie");


            //HttpCookie myCookie = new HttpCookie("sqlAuthCookie");
            //cookie.Expires = DateTime.Now.AddDays(-1d);
            //Response.Cookies.Add(cookie);

            //_httpClient.Dispose();

            //_logger.LogInformation("User logged out.");

            //Response.Cookies.Delete("sqlAuthCookie");
            ////Response.Cookies.Append("sqlAuthCookie", "", new CookieOptions()
            ////{
            ////    Expires = DateTime.Now.AddDays(-1)
            ////});

            //_logger.LogInformation("User logged out of SSRS.");

            return RedirectToAction("Dashboard", "Dashboard");
        }
    }
}
