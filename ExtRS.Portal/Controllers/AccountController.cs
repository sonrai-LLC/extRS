using Microsoft.AspNetCore.Mvc;
using ExtRS.Portal.Models;
using ReportingServices.Api.Models;
using Sonrai.ExtRS;
using Sonrai.ExtRS.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace ExtRS.Portal.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IConfiguration _configuration;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(ILogger<AccountController> logger, IConfiguration configuration, SignInManager<IdentityUser> signInManager)
        {
            _logger = logger;
            _configuration = configuration;
            _signInManager = signInManager;
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
           // return View("/Shared/Areas/Identity/Pages/Account/Login.cshtml");
           return View("Login");
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");

            return RedirectToAction("Dashboard", "Dashboard");
        }
    }
}
