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
using System.Web;

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

		[HttpGet]
		public async Task<IActionResult> LogOutAsync()
		{
			await _ssrs.DeleteSession();		
			await _signInManager.SignOutAsync();
			_ssrs.ClearCookies(_httpContextAccessor, "http://ssrssrv.net,http://portal.ssrssrv.net,_dltdgst");
			HttpContext.Session.Clear();
			_httpContextAccessor!.HttpContext!.Session.Clear();
			_httpContextAccessor!.HttpContext!.Session.Remove("_dltdgst");

			return new RedirectToPageResult("/Account/Login", new { area = "Identity" });
		}

		[HttpGet]
		public IActionResult SignedIn()
		{
			return RedirectToAction("Dashboard", "Dashboard");
		}
	}
}



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