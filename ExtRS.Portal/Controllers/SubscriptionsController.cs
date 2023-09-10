using Microsoft.AspNetCore.Mvc;
using ExtRS.Portal.Models;

namespace ExtRS.Portal.Controllers
{
    public class SubscriptionsController : Controller
    {
        private readonly ILogger<SubscriptionsController> _logger;
        private readonly IConfiguration _configuration;

        public SubscriptionsController(ILogger<SubscriptionsController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<IActionResult> Subscriptions(SubscriptionsView view)
        {
            SubscriptionsView model = new SubscriptionsView { CurrentTab = "Subscriptions" };

            return View(model);
        }

        public IActionResult Subscription(ReportsView view)
        {
            return View("Subscription", view);
        }
    }
}
