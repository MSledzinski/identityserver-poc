using System.Web;
using System.Web.Mvc;
using Thinktecture.IdentityModel.Mvc;

namespace Poc.Identity.MvcClient.Controllers
{
    public class SecuredForAdminController : Controller
    {
        [Authorize]
        [ResourceAuthorize("Read", "SomeInfo")]
        [HandleForbidden]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Logout()
        {
            Request.GetOwinContext().Authentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}