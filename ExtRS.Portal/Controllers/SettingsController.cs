using Microsoft.AspNetCore.Mvc;
using DataTables;
using ExtRS.Portal.Models;
using Microsoft.AspNetCore.Authorization;

namespace ExtRS.Portal.Controllers
{
    [AllowAnonymous]
    public class SettingsController : Controller
    {
        private readonly ILogger<SettingsController> _logger;
        private readonly IConfiguration _configuration;

        public SettingsController(ILogger<SettingsController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        [Route("Settings/SettingsJson")]
        [HttpGet]
        public ActionResult SettingsJson()
        {
            try
            {
                var dbType = "sqlserver";
                var dbConnection = _configuration["defaultConnection"];
                using var db = new Database(dbType, dbConnection);
                var response = new Editor(db, "ConfigurationInfo", "ConfigInfoID")
                    .Model<ConfigurationInfoView>()
                    .Field(new Field("ConfigInfoID")
                        .SetFormatter(Format.IfEmpty(null))
                    )
                    .Field(new Field("Name")
                        .SetFormatter(Format.IfEmpty(null))
                    )
                    .Field(new Field("Value")
                        .SetFormatter(Format.IfEmpty(null))
                    )
                    .TryCatch(true)
                    .Process(Request)
                    .Data();

                return Json(response);
            }
            catch(Exception)
            {

            }

            return Json(null);
        }

        public ActionResult Settings()
        {
            return View("Settings", new SettingsView() { CurrentTab = "Settings" });
        }

        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }
    }
}
