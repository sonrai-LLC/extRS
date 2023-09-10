﻿using Microsoft.AspNetCore.Mvc;
using ExtRS.Portal.Models;
using ReportingServices.Api.Models;
using Sonrai.ExtRS;
using Sonrai.ExtRS.Models;

namespace ExtRS.Portal.Controllers
{
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        private readonly IConfiguration _configuration;
        private readonly SSRSConnection _connection;
        private readonly HttpClient _httpClient;
        private SSRSService _ssrs;

        public AdminController(ILogger<AdminController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            _httpClient = new HttpClient();
            _connection = new SSRSConnection("localhost", "ExtRSAuth", AuthenticationType.ExtRSAuth);
            _connection.SqlAuthCookie = SSRSService.GetSqlAuthCookie(_httpClient, _connection.Administrator, _configuration["passphrase"]!, _connection.ServerName).Result;
            _ssrs = new SSRSService(_connection);
        }

        public async Task<IActionResult> Admin()
        {
            Report report = await _ssrs.GetReport("path='/Reports/Team'");
            AdminView model = new AdminView { AdminID = Guid.NewGuid(), CurrentTab = "Admin" };

            return View(model);
        }
    }
}
