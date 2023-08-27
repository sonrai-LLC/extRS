﻿using ExtRS.Portal.Models;
using ExtRS.Portal.Models;
using IO.Swagger.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Sonrai.ExtRS;
using Sonrai.ExtRS.Models;
using System.Configuration;
using System.Diagnostics;
using System.Net.Http;

namespace ExtRS.Portal.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ILogger<DashboardController> _logger;
        private readonly IConfiguration _configuration;

        public DashboardController(ILogger<DashboardController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        //[Authorize]
        public async Task<IActionResult> Dashboard()
        {
			var httpClient = new HttpClient();
			SSRSConnection connection = new SSRSConnection("localhost", "ExtRSAuth", AuthenticationType.ExtRSAuth);
			connection.SqlAuthCookie = await SSRSService.GetSqlAuthCookie(httpClient, connection.Administrator, _configuration["passphrase"]!, connection.ServerName);
			var ssrs = new SSRSService(connection);

            Report report = await ssrs.GetReport("path='/Reports/Team'");
            DashboardView model = new DashboardView { Report = report, CurrentTab = "Dashboard" };

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorView { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}
