using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;

namespace Sonrai.ExtRS.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    [RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
    public class GISController : ControllerBase
    {
        private readonly ILogger<GISController> _logger;

        public GISController(ILogger<GISController> logger)
        {
            _logger = logger;
        }
    }
}
