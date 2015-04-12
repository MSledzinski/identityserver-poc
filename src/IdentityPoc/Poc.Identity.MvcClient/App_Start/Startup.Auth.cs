namespace Poc.Identity.MvcClient
{
    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using System.Web.Helpers;

    using Microsoft.IdentityModel.Protocols;
    using Microsoft.Owin.Security;
    using Microsoft.Owin.Security.Cookies;
    using Microsoft.Owin.Security.Notifications;
    using Microsoft.Owin.Security.OpenIdConnect;

    using Owin;

    using Thinktecture.IdentityModel.Client;

    public partial class Startup
    {
        public void ConfigureAuth(IAppBuilder app)
        {
            // anti CRFS
            AntiForgeryConfig.UniqueClaimTypeIdentifier = "sub";
  
            // if prop name is in red - it is just VS messing with you
            JwtSecurityTokenHandler.InboundClaimTypeMap = new Dictionary<string, string>();
        
            app.UseCookieAuthentication(new CookieAuthenticationOptions() { AuthenticationType = "Cookies" });
           
            app.UseOpenIdConnectAuthentication(
                new OpenIdConnectAuthenticationOptions()
                    {
                        ClientId = "implicitclient",
                        Authority = "https://localhost:44333/identity",
                        RedirectUri = "http://localhost:2671/",
                        // id - OIDC token, token - for oauth to webapi
                        ResponseType = "id_token token",
                        // openid - auth token, email - claim, roles -all claim fo role assignement
                        Scope = "openid email roles webApiBack",
                        SignInAsAuthenticationType = "Cookies",

                        Notifications = new OpenIdConnectAuthenticationNotifications()
                                            {
                                                // to store only needed claims in cookie
                                                SecurityTokenValidated = async data => await ExtractNameAndRoleFromValidatedTokenAsync(data),

                                                // for logout purposes, when we are sending the logout req
                                                // we need to pass our id token to prove we are not spammer
                                                RedirectToIdentityProvider = async data => RedirectToIdentityProvider(data)
                                            }
                    });

            app.UseResourceAuthorization(new CustomResourceAuthManager());
        }

        private void RedirectToIdentityProvider(
            RedirectToIdentityProviderNotification<OpenIdConnectMessage, OpenIdConnectAuthenticationOptions> notification)
        {
            if (notification.ProtocolMessage.RequestType
                != OpenIdConnectRequestType.LogoutRequest)
            {
                return;
            }

            notification.ProtocolMessage.IdTokenHint =
                notification.OwinContext.Authentication.User.FindFirst("id_token").Value;
        }

        private async Task ExtractNameAndRoleFromValidatedTokenAsync(
            SecurityTokenValidatedNotification<OpenIdConnectMessage, OpenIdConnectAuthenticationOptions> notification)
        {
            var identity = notification.AuthenticationTicket.Identity;

            var simplifiedClaimSet = new ClaimsIdentity(identity.AuthenticationType, "email", "role");

            // as id and access do not have claims - we have to fetch them 
            var userInfoEndpointUri = new Uri(
                notification.Options.Authority + "/connect/userinfo");
            var userInfoClient = new UserInfoClient(
                userInfoEndpointUri,
                notification.ProtocolMessage.AccessToken);

            var userInfo = await userInfoClient.GetAsync();
            simplifiedClaimSet.AddClaims(userInfo.Claims.Select(tc => new Claim(tc.Item1, tc.Item2)));

            simplifiedClaimSet.AddClaim(
               new Claim("id_token", notification.ProtocolMessage.IdToken));

            simplifiedClaimSet.AddClaim(
               new Claim("access_token", notification.ProtocolMessage.AccessToken));

            simplifiedClaimSet.AddClaim(
                new Claim(
                    "expires_at",
                    DateTimeOffset.Now.AddSeconds(int.Parse(notification.ProtocolMessage.ExpiresIn)).ToString()));

            //simplifiedClaimSet.AddClaim(identity.FindFirst("email"));
            //simplifiedClaimSet.AddClaims(identity.FindAll("role"));

            notification.AuthenticationTicket = new AuthenticationTicket(
                simplifiedClaimSet,
                notification.AuthenticationTicket.Properties );
        }
    }
}