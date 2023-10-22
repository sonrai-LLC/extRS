using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using DataTables;
using ExtRS.Portal.Models;
using Sonrai.ExtRS;
using Sonrai.ExtRS.Models;

namespace EditorNetCoreDemo.Controllers
{
    public class StatsController : Controller
    {
        private readonly ILogger<StatsController> _logger;
        private readonly IConfiguration _configuration;
        private readonly SSRSConnection _connection;
        private readonly HttpClient _httpClient;
        private SSRSService _ssrs;

        public StatsController(ILogger<StatsController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            _httpClient = new HttpClient();
            _connection = new SSRSConnection(_configuration["ReportServerName"]!, _configuration["User"]!, AuthenticationType.ExtRSAuth);
            _connection.SqlAuthCookie = SSRSService.GetSqlAuthCookie(_httpClient, _connection.Administrator, _configuration["extrspassphrase"]!, _connection.ReportServerName).Result;
            _ssrs = new SSRSService(_connection, _configuration);
        }

        public ActionResult StatsLink() 
        {
            return View("Stats", new StatsView2() { CurrentTab = "Stats" });
        }

        //[Route("api/stats")]
        [HttpGet]
        [HttpPost]
        public ActionResult Stats()
        {
            using var db = new Database(_configuration["dbType"], new SqlConnection(_configuration["ConnectionStrings:sqlServerLocal"]));
            var response = new Editor(db, "datatables_demo")
                .Model<StatsView>()

                .Field(new Field("first_name")
                    .Validator(Validation.NotEmpty())
                )
                .Field(new Field("last_name"))
                .Field(new Field("extn")
                    .Validator(Validation.Numeric())
                )
                .Field(new Field("age")
                    .Validator(Validation.Numeric())
                    .SetFormatter(Format.IfEmpty(null))
                )
                .Field(new Field("salary")
                    .Validator(Validation.Numeric())
                    .SetFormatter(Format.IfEmpty(null))
                )
                .Field(new Field("start_date")
                    .Validator(Validation.DateFormat(
                        Format.DATE_ISO_8601,
                        new ValidationOpts { Message = "Please enter a date in the format yyyy-mm-dd" }
                    ))
                    .GetFormatter(Format.DateSqlToFormat(Format.DATE_ISO_8601))
                    .SetFormatter(Format.DateFormatToSql(Format.DATE_ISO_8601))
                )
                .TryCatch(false)
                .Process(Request)
                .Data();

            return Json(response);
        }
    }
}
