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
    [AllowAnonymous]
    public class AccountMvcController : Controller
	{
		private readonly ILogger<AccountMvcController> _logger;
		private readonly IConfiguration _configuration;
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly SignInManager<ApplicationUser> _signInManager;
		private readonly SSRSConnection _connection;
		private readonly HttpClient _httpClient;
		private SSRSService _ssrs;
		public readonly string _domains;
		private readonly IWebHostEnvironment _hostingEnvironment;

		public AccountMvcController(ILogger<AccountMvcController> logger, IConfiguration configuration, IHttpContextAccessor httpContextAccessor, SignInManager<ApplicationUser> signInManager, IWebHostEnvironment hostingEnvironment)
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

        [AllowAnonymous]
        //[HttpPost]
		public async Task<IActionResult> LogoutMvc()
		{

            //var sqlAuthCookie = new Cookie("sqlAuthCookie", "sqlAuthCookie");
            //sqlAuthCookie.Expires = DateTime.Now.AddDays(-1);
            //_httpContextAccessor.HttpContext.Response.Cookies.Delete("sqlAuthCookie");

            //         var cookieOptions = new CookieOptions();
            //         cookieOptions.Expires = DateTime.Now.AddDays(-1);
            //         cookieOptions.Path = "/";
            //cookieOptions.Domain = "ssrssrv.net";
            //cookieOptions.IsEssential = true;
            //cookieOptions.SameSite = SameSiteMode.None;
            ////cookieOptions.
            //         Response.Cookies.Append("sqlAuthCookie", "", cookieOptions);
            //         _httpContextAccessor.HttpContext.Response.Cookies.Append("sqlAuthCookie", "", cookieOptions);

            //var cookies = _httpContextAccessor.HttpContext.Response.Cookies;

            // Response.HttpContext.Session.Remove("sqlAuthCookie");

            //HttpContext.Session.Clear();

   //         List<Report> reports = await _ssrs.GetReports();

   //         foreach (var report in reports)
   //         {
   //             string uri = string.Format(Url.ActionLink("Report", "Reports", new { reportName = report.Name })!);
   //             report.Uri = uri + "&Qs=" + EncryptionService.Encrypt(uri, _configuration["cle"]!);
   //         }

            //var URL = reports.First().Uri.Replace("localhost:7047", "ssrssrv.net");
   //         response = await _ssrs.DeleteSession();
			//_ssrs.ClearSqlAuthCookies(_httpContextAccessor, "ssrssrv.net");


            await _signInManager.SignOutAsync();


            //// Create a CookieContainer instance
            //var cookieContainer = new CookieContainer();

            //// Create an HttpClientHandler and assign the CookieContainer to it
            //var handler = new HttpClientHandler
            //{
            //    CookieContainer = cookieContainer,
            //    UseCookies = true, // Ensure that the handler uses the CookieContainer
            //};

			//// Create an HttpClient instance with the HttpClientHandler
			//using (var client = new HttpClient(handler))
			//{
			//	// Sample URI that sets cookies
			//	var uri = new Uri("https://ssrssrv.net/ReportServer/logon.aspx");

			//	// Send a GET request to the URI
			//	var response = await client.GetAsync(uri);
			//}

          // var response = client.GetStreamAsync("https://ssrssrv.net/ReportServer/logon.aspx"); // clear RS user session

			//return Redirect("https://ssrssrv.net/ReportServer/logon.aspx");

            return RedirectToPage("/Account/Login", new { area = "Identity" });

            // return RedirectToAction("Reports", "Reports");
        }

        [AllowAnonymous]
        //[HttpGet]
		public async Task<IActionResult> SignedIn()
		{
			var info = await _signInManager.GetExternalLoginInfoAsync();
			await _signInManager.ExternalLoginSignInAsync(info!.LoginProvider, info.ProviderKey, isPersistent: false);
			await _ssrs.DeleteSession();
			await _ssrs.CreateSession(_httpContextAccessor.HttpContext.User.Identity.Name!, _configuration["extrspassphrase"]!, _connection.ReportServerName);
			_ssrs._conn.SqlAuthCookie = SSRSService.GetSqlAuthCookie(_httpClient, _httpContextAccessor.HttpContext.User.Identity.Name!, _configuration["extrspassphrase"]!, _connection.ReportServerName).Result;
			return RedirectToAction("Dashboard", "Dashboard");
		}


		[HttpGet]
		public IActionResult SignedOut()
		{
			//_ssrs.DeleteSession();
			//_ssrs.ClearCookies(_httpContextAccessor, "portal.ssrssrv.net, ssrssrv.net");
			_signInManager.SignOutAsync();

			//HttpContext.Session.Clear();
			//_httpContextAccessor!.HttpContext!.Session.Clear();

			return RedirectToAction("Dashboard", "Dashboard");
		}

	}
}


