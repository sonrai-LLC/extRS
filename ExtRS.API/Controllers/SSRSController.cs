using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;

namespace Sonrai.ExtRS.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    [RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
    public class SSRSController : ControllerBase
    {
        private readonly ILogger<SSRSController> _logger;

        public SSRSController(ILogger<SSRSController> logger)
        {
            _logger = logger;
        }
    }
}
