using ExtRS.Portal.Domain.Models;
using ExtRS.Portal.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ExtRS.Portal.Controllers
{
    public class SSRSController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public SSRSController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View(new SSRSView { SelectedView = "Index" }); ;
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Users()
        {
            return View("Users");
        }

        public IActionResult Staff()
        {
            return View("Staff");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
