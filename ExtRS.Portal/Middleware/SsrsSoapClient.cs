using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ExtRS.Portal
{
    public sealed class SsrsSoapClient
    {
        private readonly object _rs;           // generated ReportExecutionService proxy instance
        private readonly MethodInfo _load;    // LoadReport(reportPath, historyId)
        private readonly MethodInfo _setExec; // SetExecutionParameters(execId, names[], values[], culture)
        private readonly MethodInfo _render;  // Render(format, deviceInfo, out ...)

        public SsrsSoapClient(object generatedProxy)
        {
            _rs = generatedProxy;

            var t = generatedProxy.GetType();

            _load = t.GetMethod("LoadReport", new[] { typeof(string), typeof(string) })
                     ?? throw new InvalidOperationException("LoadReport(string reportPath, string historyId) not found.");

            _setExec = t.GetMethod("SetExecutionParameters", new[] {
                        typeof(string), typeof(string[]), typeof(string[]), typeof(string)
                   })
                       ?? throw new InvalidOperationException("SetExecutionParameters(string, string[], string[], string) not found.");

            // Render overload differs by SSRS version/proxy generation; pick by param count.
            _render = t.GetMethods().FirstOrDefault(m => m.Name == "Render" && m.GetParameters().Length >= 7)
                      ?? throw new InvalidOperationException("Render(...) not found.");
        }

        public byte[] RenderPdf(string reportPath, IDictionary<string, string> parameters, string culture = "en-us")
        {
            string execId = (string)_load.Invoke(_rs, new object?[] { reportPath, null });

            if (parameters != null && parameters.Count > 0)
            {
                var names = parameters.Keys.ToArray();
                var values = parameters.Values.ToArray();
                _setExec.Invoke(_rs, new object?[] { execId, names, values, culture });
            }

            // Expect Render("PDF", null, out extension, out mimeType, out encoding, out warnings, out streamIds)
            // Our reflection call needs to pass placeholders for out params.
            object[] args = new object?[_render.GetParameters().Length];

            // Fill in the input params for the common SSRS Render signature.
            // Common order: (string format, string deviceInfo, out string extension, out string mimeType, out string encoding, out Warning[] warnings, out string[] streamIds)
            var renderParams = _render.GetParameters();
            args[0] = "PDF"; // format
            args[1] = null; // deviceInfo (string)

            for (int i = 2; i < args.Length; i++) args[i] = null;

            var result = _render.Invoke(_rs, args);

            // In many generated proxies, Render returns byte[] (out params also exist).
            // If it returns byte[], you're done. If not, try to find a byte[] in return value.
            if (result is byte[] bytes) return bytes;

            // Some proxies return Stream instead; handle that.
            if (result is System.IO.Stream stream)
            {
                using var ms = new System.IO.MemoryStream();
                stream.CopyTo(ms);
                return ms.ToArray();
            }

            throw new InvalidOperationException($"Unexpected Render return type: {result?.GetType().FullName ?? "null"}");
        }
    }

    [ApiController]
    [Route("api/ssrs")]
    public class SsrsController : ControllerBase
    {
        [HttpGet("render")]
        public IActionResult Render([FromQuery] string reportPath)
        {
            // You still need a generated proxy instance (from the SSRS WSDL) to create the "web service client".
            // The wrapper below then calls it generically via reflection.

            var serviceUrl = Environment.GetEnvironmentVariable("SSRS_REPORTEXECUTION_URL")!;
            var user = Environment.GetEnvironmentVariable("SSRS_USER")!;
            var pass = Environment.GetEnvironmentVariable("SSRS_PASSWORD")!;
            var domain = Environment.GetEnvironmentVariable("SSRS_DOMAIN") ?? "";

            // ---- IMPORTANT ----
            // Replace this type with the one from your generated proxy.
            // Example common name: Microsoft.ReportingServices.ReportExecutionService.ReportExecutionService
            // ---------------------------------
            var proxyTypeName = "Microsoft.ReportingServices.ReportExecutionService.ReportExecutionService";
            var proxyType = Type.GetType(proxyTypeName, throwOnError: true)!;

            object proxy = Activator.CreateInstance(proxyType)!;

            // Set Url property (common name: Url)
            proxyType.GetProperty("Url", BindingFlags.Public | BindingFlags.Instance)!
                     .SetValue(proxy, serviceUrl);

            // Set Credentials (common name: Credentials)
            var cred = new NetworkCredential(user, pass, domain);
            proxyType.GetProperty("Credentials", BindingFlags.Public | BindingFlags.Instance)!
                     .SetValue(proxy, cred);

            var client = new SsrsSoapClient(proxy);

            // Example params (adjust to your report’s expected parameters)
            var parameters = new Dictionary<string, string>
            {
                ["StartDate"] = DateTime.UtcNow.AddDays(-30).ToString("yyyy-MM-dd"),
                ["EndDate"] = DateTime.UtcNow.ToString("yyyy-MM-dd")
            };

            byte[] pdf = client.RenderPdf(reportPath, parameters);

            return File(pdf, "application/pdf", "report.pdf");
        }
    }
}
