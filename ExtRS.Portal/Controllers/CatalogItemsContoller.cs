using ExtRS.Portal.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ExtRS.Portal.Controllers
{
    public class CatalogItemsContoller : Controller
    {
        private readonly ILogger<CatalogItemsContoller> _logger;

        public CatalogItemsContoller(ILogger<CatalogItemsContoller> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
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
