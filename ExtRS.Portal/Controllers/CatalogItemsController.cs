﻿using ExtRS.Portal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReportingServices.Api.Models;
using Sonrai.ExtRS;
using Sonrai.ExtRS.Models;
using System.Diagnostics;
using System.Text;

namespace ExtRS.Portal.Controllers
{
    [Authorize]
    public class CatalogItemsController : Controller
    {
        private readonly ILogger<CatalogItemsController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly SSRSConnection _connection;
        private readonly HttpClient _httpClient;
        private SSRSService _ssrs;

        public CatalogItemsController(ILogger<CatalogItemsController> logger, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _httpClient = new HttpClient();
            _connection = new SSRSConnection(_configuration["ReportServerName"]!, _httpContextAccessor.HttpContext!.User!.Identity!.Name!, AuthenticationType.ExtRSAuth);
            _ssrs = new SSRSService(_connection, _configuration, _httpContextAccessor);
            _ssrs._conn.SqlAuthCookie = _ssrs.GetSqlAuthCookie(_httpClient, _httpContextAccessor.HttpContext.User.Identity.Name!, _configuration["extrspassphrase"]!, _connection.ReportServerName).Result;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetManageCatalogItemModal(string id)
        {
            CatalogItem catalogItem = await _ssrs.GetCatalogItem(id);
            
            return PartialView("_ManageCatalogItem", catalogItem);
        }

        [HttpGet]
        public async Task<IActionResult> Open(string id)
        {
            CatalogItem item = await _ssrs.GetCatalogItem(id);
            string openUrl = "";
            switch (item.Type)
            {
                case "DataSource":
                    return Redirect(Url.Action("DataSource", "DataSources", new { id = id })!);
                case "DataSet":
                    return Redirect(Url.Action("Dataset", "Datasets", new { id = id })!);
                case "Report":
                    return Redirect(Url.Action("Report", "Reports", new { id = id })!);
                default:
                    openUrl = "unknown.htm";
                    break;
            }

            return Redirect(openUrl);
        }

        [HttpGet]
        public async Task<IActionResult> Download(string id)
        {
            CatalogItem item = await _ssrs.GetCatalogItem(id);
            string fileName;
            switch (item.Type)
            {
                case "DataSource":
                    fileName = item.Name + ".rds";
                    break;
                case "DataSet":
                    fileName = item.Name + ".rsd";
                    break;
                case "Report":
                    fileName = item.Name + ".rdl";
                    break;
                default:
                    fileName = "unknown.xml";
                    break;
            }

            string contentAsString = await _ssrs.GetCatalogItemContent(id);
            byte[] content = Encoding.ASCII.GetBytes(contentAsString);
            var contentType = "APPLICATION/octet-stream";
            return File(new MemoryStream(content), contentType, fileName);
        }

        public async Task<IActionResult> Delete(string id)
        {
            CatalogItem item = await _ssrs.GetCatalogItem(id);
            string type = item.Type!;
            bool isDeleted = await _ssrs.DeleteCatalogItem(id);

            switch (type)
            {
                case "DataSource":
                    return RedirectToAction("DataSources", "DataSources");
                case "DataSet":
                    return RedirectToAction("DataSets", "DataSets");
                case "Report":
                    return RedirectToAction("Reports", "Reports");
                default:
                    return RedirectToAction("Reports", "Reports");
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Users()
        {
            return View("Users");
        }

        public IActionResult Reports()
        {
            return View("Reports");
        }

        public IActionResult Staff()
        {
            return View("Staff");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorView { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
