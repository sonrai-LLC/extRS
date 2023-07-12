using Microsoft.AspNetCore.Mvc;
using DataTables;
using ExtRS.Portal.Models;
using Microsoft.Data.SqlClient;

namespace EditorNetCoreDemo.Controllers
{
    public class StaffController : Controller
    {
        private readonly IConfiguration _configuration;

        public StaffController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [Route("api/staff")]
        [HttpGet]
        [HttpPost]
        public ActionResult Staff()
        {
            using var db = new Database(_configuration["dbType"], new SqlConnection(_configuration["ConnectionStrings:sqlServerLocal"]));
            var response = new Editor(db, "datatables_demo")
                .Model<StaffModel>()

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
