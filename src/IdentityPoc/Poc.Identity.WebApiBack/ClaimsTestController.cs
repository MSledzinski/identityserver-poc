namespace Poc.Identity.WebApiBack
{
    using System.Linq;
    using System.Security.Claims;
    using System.Web.Http;

    [Route("claims")]
    [Authorize]
    public class ClaimsTestController : ApiController
    {
        public IHttpActionResult Get()
        {
            var data = (User as ClaimsPrincipal).Claims.Select(c => new { type = c.Type, value = c.Value });

            return Json(data);
        }
    }
}