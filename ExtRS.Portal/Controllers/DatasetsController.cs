using Microsoft.AspNetCore.Mvc;
using ReportingServices.Api.Models;
using ExtRS.Portal.Models;
using Sonrai.ExtRS;
using Sonrai.ExtRS.Models;
using System.Diagnostics;
using System.Data;
using DataSet = ReportingServices.Api.Models.DataSet;

namespace ExtRS.Portal.Controllers
{
    public class DatasetsController : Controller
    {
        private readonly ILogger<DatasetsController> _logger;
        private readonly IConfiguration _configuration;
        private readonly SSRSConnection _connection;
        private readonly HttpClient _httpClient;
        private SSRSService _ssrs;

        public DatasetsController(ILogger<DatasetsController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            _httpClient = new HttpClient();
            _connection = new SSRSConnection(_configuration["ReportServerName"]!, "ExtRSAuth", AuthenticationType.ExtRSAuth);
            _connection.SqlAuthCookie = SSRSService.GetSqlAuthCookie(_httpClient, _connection.Administrator, _configuration["extrspassphrase"]!, _connection.ServerName).Result;
            _ssrs = new SSRSService(_connection, _configuration);
        }
        public async Task<IActionResult> Datasets()
        {
            List<DataSet> datasets = await _ssrs.GetDataSets();

            DatasetsView model = new DatasetsView() { CurrentTab = "Datasets", Datasets = datasets, ReportServerName = _configuration["ReportServerName"]! };
            return View(model);
        }

        public IActionResult Dataset()
        {
            DatasetsView model = new DatasetsView() { CurrentTab = "Datasets", ReportServerName = _configuration["ReportServerName"]! };
            return View("_Dataset");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorView { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
