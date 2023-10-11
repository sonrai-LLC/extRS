using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ExtRS.Portal.Models;
using Sonrai.ExtRS.Models;
using System.ComponentModel.Design;

namespace ExtRS.Portal.Controllers
{
    public class CatalogItemsController : Controller
    {
        private readonly ILogger<CatalogItemsController> _logger;

        public CatalogItemsController(ILogger<CatalogItemsController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        //[DisableRequestSizeLimit]
        //[HttpPost]
        public async Task<IActionResult> GetManageResourceModal()
        {
            return PartialView("_ManageResource"); //model
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
