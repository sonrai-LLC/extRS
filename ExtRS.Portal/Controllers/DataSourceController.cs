using ExtRS.Portal.Models;
using ExtRS.Portal.Models;
using Microsoft.AspNetCore.Mvc;
using Sonrai.ExtRS.Models;
using Sonrai.ExtRS;
using System.Diagnostics;
using IO.Swagger.Model;

namespace ExtRS.Portal.Controllers
{
    public class DataSourceController : Controller
    {
        private readonly ILogger<DataSourceController> _logger;
        private readonly IConfiguration _configuration;

        public DataSourceController(ILogger<DataSourceController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<IActionResult> DataSources()
        {
            var httpClient = new HttpClient();
            SSRSConnection connection = new SSRSConnection("localhost", "ExtRSAuth", AuthenticationType.ExtRSAuth);
            connection.SqlAuthCookie = await SSRSService.GetSqlAuthCookie(httpClient, connection.Administrator, _configuration["passphrase"]!, connection.ServerName);
            var ssrs = new SSRSService(connection);

            DataSource dataSources = await ssrs.GetDataSource("path='/Data Sources/localhost'");
            DataSourceView model = new DataSourceView { CurrentTab = "DataSource" };

            return View(model);
        }

        public IActionResult DataSource(ReportView view)
        {
            return View(view);
        }
    }
}
