﻿using Microsoft.AspNetCore.Mvc;
using ExtRS.Portal.Models;
using ReportingServices.Api.Models;
using Sonrai.ExtRS;
using Sonrai.ExtRS.Models;

namespace ExtRS.Portal.Controllers
{
    public class DataSourcesController : Controller
    {
        private readonly ILogger<DataSourcesController> _logger;
        private readonly IConfiguration _configuration;
        private readonly SSRSConnection _connection;
        private readonly HttpClient _httpClient;
        private SSRSService _ssrs;

        public DataSourcesController(ILogger<DataSourcesController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            _httpClient = new HttpClient();
            _connection = new SSRSConnection(_configuration["ReportServerName"]!, _configuration["User"]!, AuthenticationType.extRSAuth);
            _connection.SqlAuthCookie = SSRSService.GetSqlAuthCookie(_httpClient, _connection.Administrator, _configuration["extrspassphrase"]!, _connection.ReportServerName).Result;
            _ssrs = new SSRSService(_connection, _configuration);
        }

        public async Task<IActionResult> DataSources()
        {
            List<DataSource> dataSources = await _ssrs.GetDataSources();
            foreach (var dataSource in dataSources)
            {
                string uri = string.Format(Url.ActionLink("DataSource", "DataSources", new { dataSourceName = dataSource.Name })!);
                dataSource.Uri = uri + "&Qs=" + EncryptionService.Encrypt(uri, _configuration["cle"]!);
            }

            DataSourcesView model = new DataSourcesView { CurrentTab = "DataSources", DataSources = dataSources, ReportServerName = _configuration["ReportServerName"]! };

            return View(model);
        }

        public async Task<IActionResult> DataSource(string dataSourceName, string id)
        {
            DataSource dataSource;
            if (dataSourceName is not null)
            {
                dataSource = await _ssrs.GetDataSource(string.Format("path='/Data Sources/{0}'", dataSourceName));
            }
            else
            {
                dataSource = await _ssrs.GetDataSource(id);
            }

            string uri = string.Format("https://{0}/Reportserver/Data+Sources?%2fData+Sources/{1}", _ssrs._conn.ReportServerName, dataSource.Name);
            dataSource.Uri = uri + "&Qs=" + EncryptionService.Encrypt(uri, _configuration["cle"]!);

            DataSourceView view = new DataSourceView { CurrentTab = "DataSources", SelectedDataSource = dataSource, ReportServerName = _configuration["ReportServerName"]! };
            return View("_DataSource", view);
        }
    }
}
