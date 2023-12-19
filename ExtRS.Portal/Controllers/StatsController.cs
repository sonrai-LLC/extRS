﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using DataTables;
using ExtRS.Portal.Models;
using Sonrai.ExtRS;
using Sonrai.ExtRS.Models;

namespace EditorNetCoreDemo.Controllers
{
    public class StatsController : Controller
    {
        private readonly ILogger<StatsController> _logger;
        private readonly IConfiguration _configuration;
        private readonly SSRSConnection _connection;
        private readonly HttpClient _httpClient;
        private SSRSService _ssrs;

        public StatsController(ILogger<StatsController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            _httpClient = new HttpClient();
            _connection = new SSRSConnection(_configuration["ReportServerName"]!, _configuration["User"]!, AuthenticationType.ExtRSAuth);
            _connection.SqlAuthCookie = SSRSService.GetSqlAuthCookie(_httpClient, _configuration["User"]!, _configuration["extrspassphrase"]!, _connection.ReportServerName).Result;
            _ssrs = new SSRSService(_connection, _configuration);
        }

        public async Task<ActionResult> Stats() 
        {
            return View("Stats", new StatsView() { CurrentTab = "Stats", SystemInfo = await _ssrs.GetSystemInfo(), ReportExecutionStats = await _ssrs.GetReportExecutionStats(_configuration["rapport"]) });
        }
    }
}
