﻿using Microsoft.AspNetCore.Mvc;
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
            _connection = new SSRSConnection(_configuration["ReportServerName"]!, "extRSAuth", AuthenticationType.ExtRSAuth);
            _ssrs = new SSRSService(_connection, _configuration);
            _ssrs._conn.SqlAuthCookie = _ssrs.GetSqlAuthCookie(_httpClient, "extRSAuth", _configuration["extrspassphrase"]!, _connection.ReportServerName).Result;
        }

        [Authorize]
        public async Task<IActionResult> Dashboard()
        {
            Report report = await _ssrs.GetReport("path='/Reports/Team'");
            DashboardView model = new DashboardView { Report = report, CurrentTab = "Dashboard" };

            return View(model);
        }

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorView { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}
