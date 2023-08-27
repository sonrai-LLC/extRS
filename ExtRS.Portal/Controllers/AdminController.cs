using ExtRS.Portal.Models;
using Microsoft.AspNetCore.Mvc;
using Sonrai.ExtRS.Models;
using Sonrai.ExtRS;
using System.Diagnostics;
using IO.Swagger.Model;

namespace ExtRS.Portal.Controllers
{
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        private readonly IConfiguration _configuration;

        public AdminController(ILogger<AdminController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<IActionResult> Admin()
        {
            var httpClient = new HttpClient();
            SSRSConnection connection = new SSRSConnection("localhost", "ExtRSAuth", AuthenticationType.ExtRSAuth);
            connection.SqlAuthCookie = await SSRSService.GetSqlAuthCookie(httpClient, connection.Administrator, _configuration["passphrase"]!, connection.ServerName);
            var ssrs = new SSRSService(connection);

            Report report = await ssrs.GetReport("path='/Reports/Team'");
            AdminView model = new AdminView { AdminID = Guid.NewGuid(), AdminUser = "Admin" };

            return View(model);
        }
    }
}
