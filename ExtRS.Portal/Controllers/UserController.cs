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
    public class UserController : Controller
    {
        //private readonly ILogger<SubscriptionController> _logger;
        //private readonly IConfiguration _configuration;

        //public SubscriptionController(ILogger<SubscriptionController> logger, IConfiguration configuration)
        //{
        //    _logger = logger;
        //    _configuration = configuration;
        //}
        //[Route("api/UserSettings")]
        [HttpGet]
        [HttpPost]
        public ActionResult Users()
        {
            try
            {
                var dbType = "sqlserver";
                var dbConnection = "Data Source=localhost;TrustServerCertificate=True;Initial Catalog=ReportServer;Integrated Security=True";
                using var db = new Database(dbType, dbConnection);
                var response = new Editor(db, "Users", "Users.UserID")
                    .Field(new Field("Users.UserType")
                        //.Validator(Validation.Numeric())
                        .SetFormatter(Format.IfEmpty(null))
                    )
                    .Field(new Field("Users.AuthType")
                        //.Validator(Validation.Numeric())
                        .SetFormatter(Format.IfEmpty(null))
                    )
                    .Field(new Field("Users.UserName")
                        //.Validator(Validation.Numeric())
                        .SetFormatter(Format.IfEmpty(null))
                    )
                    .LeftJoin("UserContactInfo", "UserContactInfo.UserID", "=", "Users.UserID")
                    .Field(new Field("UserContactInfo.DefaultEmailAddress")
                    .Validator(Validation.NotEmpty()))
                    .TryCatch(true)
                    .Process(Request)
                    .Data();

                return Json(response);
            }
            catch(Exception ex)
            {

            }

            return Json(null);
        }

        public async Task<IActionResult> UserSettings2()
        {

            var httpClient = new HttpClient();
            SSRSConnection connection = new SSRSConnection("localhost", "ExtRSAuth", AuthenticationType.ExtRSAuth);
            connection.SqlAuthCookie = await SSRSService.GetSqlAuthCookie(httpClient, connection.Administrator, "", connection.ServerName);
            var ssrs = new SSRSService(connection);

            Report report = await ssrs.GetReport("path='/Reports/Team'");
            ReportView viewModel = new ReportView { Report = report, CurrentTab = "Users" };

            return RedirectToAction("Index", "Dataset", viewModel);
        }
    }
}
