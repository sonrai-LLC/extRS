﻿using Microsoft.AspNetCore.Mvc;
using ReportingServices.Api.Models;
using ExtRS.Portal.Models;
using Sonrai.ExtRS;
using Sonrai.ExtRS.Models;
using System.Diagnostics;
using System.Data;
using DataSet = ReportingServices.Api.Models.DataSet;

namespace ExtRS.Portal.Controllers
{
    public class DatasetsController : Controller
    {
        private readonly ILogger<DatasetsController> _logger;
        private readonly IConfiguration _configuration;
        private readonly SSRSConnection _connection;
        private readonly HttpClient _httpClient;
        private SSRSService _ssrs;

        public DatasetsController(ILogger<DatasetsController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            _httpClient = new HttpClient();
            _connection = new SSRSConnection(_configuration["ReportServerName"]!, _configuration["User"]!, AuthenticationType.ExtRSAuth);
            _connection.SqlAuthCookie = SSRSService.GetSqlAuthCookie(_httpClient, _configuration["User"]!, _configuration["extrspassphrase"]!, _connection.ReportServerName).Result;
            _ssrs = new SSRSService(_connection, _configuration);
        }
        public async Task<IActionResult> Datasets()
        {
            List<DataSet> datasets = await _ssrs.GetDataSets();
            foreach (var dataset in datasets)
            {
                string uri = string.Format(Url.ActionLink("Dataset", "Datasets", new { dataSetName = dataset.Name })!);
                dataset.Uri = uri + "&Qs=" + EncryptionService.Encrypt(uri, _configuration["cle"]!);
            }       

            DatasetsView model = new DatasetsView() { CurrentTab = "Datasets", Datasets = datasets, ReportServerName = _configuration["ReportServerName"]! };
            return View(model);
        }

        public async Task<IActionResult> Dataset(string dataSetName, string id)
        {
            DataSet dataset;
            if (dataSetName is not null)
            {
                dataset = await _ssrs.GetDataSet(string.Format("path='/Datasets/{0}'", dataSetName));
            }
            else
            {
                dataset = await _ssrs.GetDataSet(id);
            }

            string uri = string.Format("https://{0}/Reportserver/Datasets?%2fDatasets/{1}", _ssrs._conn.ReportServerName, dataset.Name);
            dataset.Uri = uri + "&Qs=" + EncryptionService.Encrypt(uri, _configuration["cle"]!);

            DatasetView view = new DatasetView { CurrentTab = "Datasets", SelectedDataSet = dataset, ReportServerName = _configuration["ReportServerName"]! };
            return View("_Dataset", view);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorView { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
