using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ExtRS.Portal.Models;
using Sonrai.ExtRS.Models;
using System.ComponentModel.Design;
using ReportingServices.Api.Models;
using Sonrai.ExtRS;

namespace ExtRS.Portal.Controllers
{
    public class CatalogItemsController : Controller
    {
        private readonly ILogger<CatalogItemsController> _logger;
        private readonly IConfiguration _configuration;
        private readonly SSRSConnection _connection;
        private readonly HttpClient _httpClient;
        private SSRSService _ssrs;

        public CatalogItemsController(ILogger<CatalogItemsController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            _httpClient = new HttpClient();
            _connection = new SSRSConnection(_configuration["ReportServerName"]!, "ExtRSAuth", AuthenticationType.ExtRSAuth);
            _connection.SqlAuthCookie = SSRSService.GetSqlAuthCookie(_httpClient, _connection.Administrator, _configuration["extrspassphrase"]!, _connection.ServerName).Result;
            _ssrs = new SSRSService(_connection, _configuration);
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetManageResourceModal(string id)
        {
            CatalogItem catalogItem = await _ssrs.GetCatalogItem(id);
            return PartialView("_ManageResource", catalogItem);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Users()
        {
            return View("Users");
        }
        public IActionResult Reports()
        {
            return View("Reports");
        }

        public IActionResult Staff()
        {
            return View("Staff");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorView { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
