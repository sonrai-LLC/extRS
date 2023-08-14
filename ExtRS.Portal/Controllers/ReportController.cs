using ExtRS.Portal.Models;
using ExtRS.Portal.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ExtRS.Portal.Controllers
{
	public class ReportController : Controller
    {
        private readonly ILogger<ReportController> _logger;

		public ReportController(ILogger<ReportController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(ReportView view)
        {
            return View("Reports", view);
        }
    }
}
