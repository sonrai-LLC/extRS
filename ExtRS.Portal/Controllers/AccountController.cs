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

		//[HttpGet]
		//public IActionResult SignIn() // TODO: implement Scaffolding (harder than it seems like it should be...)
		//{
		//	_signInManager.SignInAsync(new ApplicationUser(), new , true);
		//	_logger.LogInformation("User logged in.");
		//	return View("Dashboard", "Dashboard");
		//}

        [HttpGet]
		public IActionResult LogOut()
		{
			_ssrs.DeleteSession();
			_ssrs.ClearCookies(_httpContextAccessor, "portal.ssrssrv.net, ssrssrv.net");
			_signInManager.SignOutAsync();
			HttpContext.Session.Clear();
			_httpContextAccessor!.HttpContext!.Session.Clear();

			return RedirectToAction("Dashboard", "Dashboard");
		}
    }
}

//[HttpGet]
//public IActionResult SignedIn() // TODO: implement Scaffolding (harder than it seems like it should be...)
//{
//    return "";
//}

//[HttpGet]
//      public IActionResult SignedOut()
//      {
//          _ssrs.DeleteSession();
//          _ssrs.ClearCookies(_httpContextAccessor, "portal.ssrssrv.net, ssrssrv.net");
//          _signInManager.SignOutAsync();

//          //HttpContext.Session.Clear();
//          //_httpContextAccessor!.HttpContext!.Session.Clear();

//          return RedirectToAction("Dashboard", "Dashboard");
//      }