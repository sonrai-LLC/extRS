using ExtRS.Portal.Models;
using ExtRS.Portal.Models;
using Microsoft.AspNetCore.Mvc;
using Sonrai.ExtRS.Models;
using Sonrai.ExtRS;
using System.Diagnostics;
using IO.Swagger.Model;

namespace ExtRS.Portal.Controllers
{
    public class DataSourcesController : Controller
    {
        private readonly ILogger<DataSourcesController> _logger;
        private readonly IConfiguration _configuration;

        public DataSourcesController(ILogger<DataSourcesController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<IActionResult> DataSources()
        {
            var HttpClient = new HttpClient();
            SSRSConnection connection = new SSRSConnection("localhost", "ExtRSAuth", AuthenticationType.ExtRSAuth);
            connection.SqlAuthCookie = await SSRSService.GetSqlAuthCookie(HttpClient, connection.Administrator, _configuration["passphrase"]!, connection.ServerName);
            var ssrs = new SSRSService(connection);

            List<DataSource> dataSources = await ssrs.GetDataSources();
            DataSourcesView model = new DataSourcesView { CurrentTab = "DataSources", DataSources = dataSources };

            return View(model);
        }

        public IActionResult DataSource(ReportsView view)
        {
            return View(view);
        }
    }
}
