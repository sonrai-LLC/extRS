//using System;
//using System.Collections.Generic;

//using Microsoft.AspNetCore.Mvc;
//using DataTables;
//using ExtRS.Portal.Models;

//namespace EditorNetCoreDemo.Controllers
//{
//    /// <summary>
//    /// This example is used only for the DOM sourced table example where the
//    /// initial table is read from HTML rather than by Ajax. A `GetFormatter`
//    /// is used for the `salary` field to convert the data into the currency
//    /// format for the end user to view.
//    /// </summary>
//    public class StaffHtmlController : Controller
//    {
//        [Route("api/staff-html")]
//        [HttpGet]
//        [HttpPost]
//        public ActionResult StaffHtml()
//        {
//            var dbType = "sqlserver"; //Environment.GetEnvironmentVariable("DBTYPE");
//            var dbConnection = "Data Source=localhost;Initial Catalog=Datatables;TrustServerCertificate=True;Integrated Security=true;"; //Environment.GetEnvironmentVariable("DBCONNECTION"); 


//            using (var db = new Database(dbType, dbConnection))
//            {
//                var response = new Editor(db, "datatables_demo")
//                    .Model<StaffModel>()
//                    .Field(new Field("first_name").Validator(Validation.NotEmpty()))
//                    .Field(new Field("last_name").Validator(Validation.NotEmpty()))
//                    .Field(new Field("salary")
//                        .Validator(Validation.Numeric())
//                        .GetFormatter((val, row) => "$" + ((Int32)val).ToString("N0"))
//                    )
//                    .Process(Request)
//                    .Data();

//                return Json(response);
//            }
//        }
//    }
//}
