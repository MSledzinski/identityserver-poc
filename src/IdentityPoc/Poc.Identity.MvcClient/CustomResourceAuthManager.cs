namespace Poc.Identity.MvcClient
{
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Thinktecture.IdentityModel.Owin.ResourceAuthorization;

    public class CustomResourceAuthManager : ResourceAuthorizationManager
    {
        public override Task<bool> CheckAccessAsync(ResourceAuthorizationContext context)
        {
            if (!context.Resource.Any())
            {
                return Nok();
            }

            return AuthorizeResourceAccess(
                context.Action.First().Value,
                context.Resource.First().Value, 
                context.Principal) ? Ok() :  Nok();
        }

        private bool AuthorizeResourceAccess(string action, string resourceName, ClaimsPrincipal principal)
        {
            // very dumb, just a PoC :)
            return resourceName.Equals("SomeInfo") && action.Equals("Read") && principal.HasClaim("role", "Admin");
        }
    }
}