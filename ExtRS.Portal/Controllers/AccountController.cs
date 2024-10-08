﻿using Microsoft.AspNetCore.Mvc;
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
using Microsoft.Extensions.Hosting.Internal;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Infrastructure;

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
		public readonly string _domains;
		private readonly IWebHostEnvironment _hostingEnvironment;

		public AccountController(ILogger<AccountController> logger, IConfiguration configuration, IHttpContextAccessor httpContextAccessor, SignInManager<ApplicationUser> signInManager, IWebHostEnvironment hostingEnvironment)
		{
			_logger = logger;
			_configuration = configuration;
			_signInManager = signInManager;
			_httpContextAccessor = httpContextAccessor;
			_httpClient = new HttpClient();
			_connection = new SSRSConnection(_configuration["ReportServerName"]!, _httpContextAccessor.HttpContext!.User.Identity!.Name!, AuthenticationType.ExtRSAuth);
			_ssrs = new SSRSService(_connection, _configuration, _httpContextAccessor);
			_ssrs._conn.SqlAuthCookie = SSRSService.GetSqlAuthCookie(_httpClient, _httpContextAccessor.HttpContext.User.Identity.Name!, _configuration["extrspassphrase"]!, _connection.ReportServerName).Result;
		    _domains = _configuration["ReportServerName"] + "," + "_dltdgst"; ;
			_hostingEnvironment = hostingEnvironment;
		}

		[HttpGet]
		public async Task LogOutAsync()
		{
			await _signInManager.SignOutAsync();
			await _ssrs.DeleteSession();

			 await Logon();
        }

		[AllowAnonymous]
        //[HttpGet("/Identity/Account/Logon")]
        //[Route("~/Identity/Account/Logon")]
        public async Task<IActionResult> Logon()
		{
			//return new RedirectToPageResult("/Login.cshtml", new { area = "Pages/Identity/Account" });
			//return new RedirectToPageResult("/Login.cshtml");
            return LocalRedirect("/Login.cshtml");
        }

		[HttpGet]
		public async Task<IActionResult> SignedIn()
		{
			var info = await _signInManager.GetExternalLoginInfoAsync();
			await _signInManager.ExternalLoginSignInAsync(info!.LoginProvider, info.ProviderKey, isPersistent: false);
			await _ssrs.DeleteSession();
			await _ssrs.CreateSession(_httpContextAccessor.HttpContext.User.Identity.Name!, _configuration["extrspassphrase"]!, _connection.ReportServerName);
			_ssrs._conn.SqlAuthCookie = SSRSService.GetSqlAuthCookie(_httpClient, _httpContextAccessor.HttpContext.User.Identity.Name!, _configuration["extrspassphrase"]!, _connection.ReportServerName).Result;
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