using ExtRS.Portal.Models;
using Microsoft.AspNetCore.Mvc;
using Sonrai.ExtRS.Models;
using Sonrai.ExtRS;
using System.Diagnostics;
using IO.Swagger.Model;

namespace ExtRS.Portal.Controllers
{
    public class DatasetController : Controller
    {
        private readonly ILogger<DatasetController> _logger;
        private readonly IConfiguration _configuration;

        public DatasetController(ILogger<DatasetController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<IActionResult> DataSets()
        {
            var httpClient = new HttpClient();
            SSRSConnection connection = new SSRSConnection("localhost", "ExtRSAuth", AuthenticationType.ExtRSAuth);
            connection.SqlAuthCookie = await SSRSService.GetSqlAuthCookie(httpClient, connection.Administrator, _configuration["passphrase"]!, connection.ServerName);
            var ssrs = new SSRSService(connection);

            Report report = await ssrs.GetReport("path='/Reports/Team'");

            DatasetView model = new DatasetView() { CurrentTab = "Dataset" };
            return View();
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
