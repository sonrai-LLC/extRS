using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;

namespace Sonrai.ExtRS.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    [RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
    public class FormattingController : ControllerBase
    {
        private readonly ILogger<FormattingController> _logger;

        public FormattingController(ILogger<FormattingController> logger)
        {
            _logger = logger;
        }
    }
}
