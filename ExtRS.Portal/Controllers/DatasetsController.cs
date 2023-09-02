using ExtRS.Portal.Models;
using Microsoft.AspNetCore.Mvc;
using Sonrai.ExtRS.Models;
using Sonrai.ExtRS;
using System.Diagnostics;
using IO.Swagger.Model;

namespace ExtRS.Portal.Controllers
{
    public class DatasetsController : Controller
    {
        private readonly ILogger<DatasetsController> _logger;
        private readonly IConfiguration _configuration;

        public DatasetsController(ILogger<DatasetsController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<IActionResult> DataSets()
        {
            var _httpClient = new HttpClient();
            SSRSConnection connection = new SSRSConnection("localhost", "ExtRSAuth", AuthenticationType.ExtRSAuth);
            connection.SqlAuthCookie = await SSRSService.GetSqlAuthCookie(_httpClient, connection.Administrator, _configuration["passphrase"]!, connection.ServerName);
            var ssrs = new SSRSService(connection);

            List<DataSet> datasets = await ssrs.GetDataSets();

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
