using Microsoft.AspNetCore.Mvc;
using DataTables;
using ExtRS.Portal.Models;
using Azure;

namespace EditorNetCoreDemo.Controllers
{
    public class UserController : Controller
    {
        [Route("api/users")]
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
                    //.Field(new Field("Users.UserID")
                    //    //.Validator(Validation.Numeric())
                    //    .SetFormatter(Format.IfEmpty(null))
                    //)
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
                    //.Field(new Field("ServiceToken")
                    //    //.Validator(Validation.Numeric())
                    //    .SetFormatter(Format.IfEmpty(null))
                    //)
                    //.Field(new Field("Setting")
                    //    //.Validator(Validation.Numeric())
                    //    .SetFormatter(Format.IfEmpty(null))
                    //)
                    //.Field(new Field("ModifiedDate")
                    //.Validator(Validation.DateFormat(
                    //        Format.DATE_ISO_8601,
                    //        new ValidationOpts { Message = "Please enter a date in the format yyyy-mm-dd" }
                    //    ))
                    //    .GetFormatter(Format.DateSqlToFormat(Format.DATE_ISO_8601))
                    //    .SetFormatter(Format.DateFormatToSql(Format.DATE_ISO_8601))
                    //)
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
    }
}
