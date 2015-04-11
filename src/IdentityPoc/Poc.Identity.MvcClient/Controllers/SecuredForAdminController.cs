using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Poc.Identity.MvcClient.Controllers
{
    using Thinktecture.IdentityModel.Mvc;

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
            return Redirect("/");
        }
    }
}