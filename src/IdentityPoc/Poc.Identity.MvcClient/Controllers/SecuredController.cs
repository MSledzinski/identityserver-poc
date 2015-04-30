using System.Linq;
using System.Security.Claims;
using System.Web.Mvc;

namespace Poc.Identity.MvcClient.Controllers
{
    public class SecuredController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            var calimedUser = User as ClaimsPrincipal;

            ViewBag.CurrentAuthData = calimedUser.Claims.Aggregate(string.Empty, (acc, c) => acc + " " + c.ToString());

            return View();
        }
    }
}