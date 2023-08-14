using ExtRS.Portal.Models;
using ExtRS.Portal.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ExtRS.Portal.Controllers
{
    public class DatasetController : Controller
    {
        private readonly ILogger<DatasetController> _logger;

        public DatasetController(ILogger<DatasetController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View("Datasets");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorView { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
