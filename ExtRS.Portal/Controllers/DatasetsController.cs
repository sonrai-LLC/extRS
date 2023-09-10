using Microsoft.AspNetCore.Mvc;
using ReportingServices.Api.Models;
using ExtRS.Portal.Models;
using Sonrai.ExtRS;
using Sonrai.ExtRS.Models;
using System.Diagnostics;

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
            _connection = new SSRSConnection("localhost", "ExtRSAuth", AuthenticationType.ExtRSAuth);
            _connection.SqlAuthCookie = SSRSService.GetSqlAuthCookie(_httpClient, _connection.Administrator, _configuration["passphrase"]!, _connection.ServerName).Result;
            _ssrs = new SSRSService(_connection);
        }
        public async Task<IActionResult> DataSets()
        {
            List<DataSet> datasets = await _ssrs.GetDataSets();

            DatasetsView model = new DatasetsView() { CurrentTab = "Datasets", Datasets = datasets };
            return View(model);
        }

        public IActionResult Dataset()
        {
            return View("_Datasets");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorView { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
