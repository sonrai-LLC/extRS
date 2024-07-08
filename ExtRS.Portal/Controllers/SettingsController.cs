using Microsoft.AspNetCore.Mvc;
using DataTables;
using ExtRS.Portal.Models;

namespace EditorNetCoreDemo.Controllers
{
    public class SettingsController : Controller
    {
        private readonly ILogger<SettingsController> _logger;
        private readonly IConfiguration _configuration;

        public SettingsController(ILogger<SettingsController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        //[Route("api/users")]
        [HttpGet]
        [HttpPost]
        public ActionResult Users()
        {
            try
            {
                var dbType = "sqlserver";
                var dbConnection = _configuration["ConnectionStrings:sqlServerLocal"];
                using var db = new Database(dbType, dbConnection);
                var response = new Editor(db, "Users", "Id")
                    .Model<UserView2>()
                    .Field(new Field("Id")
                        //.Validator(Validation.Numeric())
                        .SetFormatter(Format.IfEmpty(null))
                    )
                    .Field(new Field("Type")
                        //.Validator(Validation.Numeric())
                        .SetFormatter(Format.IfEmpty(null))
                    )
                    .Field(new Field("UserName")
                        //.Validator(Validation.Numeric())
                        .SetFormatter(Format.IfEmpty(null))
                    )

                    .Field(new Field("Email"))  
                    .TryCatch(true)
                    .Process(Request)
                    .Data();  // .Validator(Validation.NotEmpty()))

                return Json(response);
            }
            catch(Exception ex)
            {

            }

            return Json(null);
        }

        public ActionResult Settings()
        {
            return View("Users", new SettingsView() { CurrentTab = "Settings" });
            //var _httpClient = new _httpClient();
            //SSRSConnection connection = new SSRSConnection(_configuration["ReportServerName"]!, _configuration["User"]!, AuthenticationType.ExtRSAuth);
            //connection.SqlAuthCookie = await SSRSService.GetSqlAuthCookie(_httpClient, connection.Administrator, _configuration["extrspassphrase"], connection.ServerName);
            //var ssrs = new SSRSService(connection);

            //Report report = await ssrs.GetReport("path='/Reports/Team'");
            //ReportView model = new ReportView { Report = report, CurrentTab = "Users" };

            //return RedirectToAction("Index", "Dataset", model);
        }

        public async Task<IActionResult> Login()
        {
            ViewData.Clear();
            return View("_LoginPartial");
        }

        //[Authorize]
        public IActionResult Privacy()
        {
            return View();
        }
    }
}
