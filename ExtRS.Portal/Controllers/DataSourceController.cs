using ExtRS.Portal.Models;
using ExtRS.Portal.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ExtRS.Portal.Controllers
{
    public class DataSourceController : Controller
    {
        private readonly ILogger<DataSourceController> _logger;

        public DataSourceController(ILogger<DataSourceController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(ReportView view)
        {
            return View("DataSources", view);
        }
    }
}
