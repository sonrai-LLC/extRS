using Microsoft.AspNetCore.Mvc;
using ExtRS.Portal.Models;
using ReportingServices.Api.Models;
using Sonrai.ExtRS;
using Sonrai.ExtRS.Models;
using System.Security.Policy;
using Microsoft.AspNetCore.Cors;
using System;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using static Microsoft.EntityFrameworkCore.Metadata.Internal.EntityType;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace ExtRS.Portal.Controllers
{
    public class InteractivesController : Controller
    {
        private readonly ILogger<ReportsController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public InteractivesController(ILogger<ReportsController> logger, IConfiguration configuration, IHttpContextAccessor httpContextAccessor, SignInManager<ApplicationUser> signInManager)
        {
            _logger = logger;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _signInManager = signInManager;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Interactives(InteractivesView view)
        {
            InteractivesView model = new InteractivesView { Interactives = new List<InteractiveView>(), CurrentTab = "Interactives" };
            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> Interactive(string interactiveName, string id)
        {
            InteractiveView chart = new InteractiveView();
            return View("_Interactive", chart);
        }
    }
}
