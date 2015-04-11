namespace Poc.Identity.MvcClient.Controllers
{
    using System.Net.Http;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using System.Web.Mvc;

    using Newtonsoft.Json.Linq;

    public class SecuredApiCallController : AsyncController
    {
        [Authorize]
        public async Task<ActionResult> Index()
        {
            var accessToken = (User as ClaimsPrincipal).FindFirst("access_token").Value;

            var httpClient = new HttpClient();
            httpClient.SetBearerToken(accessToken);

            var jsonData = await httpClient.GetStringAsync("http://localhost:2762/claims");
            ViewBag.WebApiData = JArray.Parse(jsonData).ToString();

            return View();
        }
    }
}