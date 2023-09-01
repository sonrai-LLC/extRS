using Microsoft.AspNetCore.Mvc;
using DataTables;
using ExtRS.Portal.Models;
using Azure;
using Sonrai.ExtRS.Models;
using Sonrai.ExtRS;
using ExtRS.Portal.Controllers;
using IO.Swagger.Model;

namespace EditorNetCoreDemo.Controllers
{
    public class UserSettingsController : Controller
    {
        private readonly ILogger<UserSettingsController> _logger;
        private readonly IConfiguration _configuration;

        public UserSettingsController(ILogger<UserSettingsController> logger, IConfiguration configuration)
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

        public ActionResult UserSettings()
        {
            return View("Users", new UserSettingsView() { CurrentTab = "UserSettings" });
            //var httpClient = new HttpClient();
            //SSRSConnection connection = new SSRSConnection("localhost", "ExtRSAuth", AuthenticationType.ExtRSAuth);
            //connection.SqlAuthCookie = await SSRSService.GetSqlAuthCookie(httpClient, connection.Administrator, _configuration["passphrase"], connection.ServerName);
            //var ssrs = new SSRSService(connection);

            //Report report = await ssrs.GetReport("path='/Reports/Team'");
            //ReportView model = new ReportView { Report = report, CurrentTab = "Users" };

            //return RedirectToAction("Index", "Dataset", model);
        }
    }
}
