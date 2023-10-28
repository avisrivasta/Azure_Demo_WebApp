using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using Owin;
using System;
using System.Web.Security;

[assembly: OwinStartup(typeof(Avectra.netForum.iWeb.App_Start.Startup))]

namespace Avectra.netForum.iWeb.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            OpenIDConnectSetup(app);
        }

        /// <summary>
        /// IF setup for OIDC is present, set it up
        /// </summary>
        private void OpenIDConnectSetup(IAppBuilder app)
        {
            try
            {
                // The Client ID is used by the application to uniquely identify itself to Microsoft identity platform.
                string clientId = System.Configuration.ConfigurationManager.AppSettings["ClientId"];

                // RedirectUri is the URL where the user will be redirected to after they sign in.
                string redirectUri = System.Configuration.ConfigurationManager.AppSettings["RedirectUri"];

                // Tenant is the tenant ID (e.g. contoso.onmicrosoft.com, or 'common' for multi-tenant)
                string tenant = System.Configuration.ConfigurationManager.AppSettings["Tenant"];

                // Authority is the URL for authority, composed of the Microsoft identity platform and the tenant name (e.g. https://login.microsoftonline.com/contoso.onmicrosoft.com/v2.0)
                string authority = String.Format(System.Globalization.CultureInfo.InvariantCulture, System.Configuration.ConfigurationManager.AppSettings["Authority"], tenant);

                app.SetDefaultSignInAsAuthenticationType(CookieAuthenticationDefaults.AuthenticationType);
                app.UseCookieAuthentication(new CookieAuthenticationOptions()
                {
                    CookieName = FormsAuthentication.FormsCookieName
                });

                app.UseOpenIdConnectAuthentication(
                    new OpenIdConnectAuthenticationOptions
                    {
                        // Sets the ClientId, authority, RedirectUri as obtained from web.config
                        ClientId = clientId,
                        Authority = authority,
                        RedirectUri = redirectUri,
                        // PostLogoutRedirectUri is the page that users will be redirected to after sign-out. In this case, it is using the home page
                        PostLogoutRedirectUri = redirectUri,
                        Scope = OpenIdConnectScope.OpenIdProfile,
                        // ResponseType is set to request the code id_token - which contains basic information about the signed-in user
                        ResponseType = OpenIdConnectResponseType.CodeIdToken,
                        // ValidateIssuer set to false to allow personal and work accounts from any organization to sign in to your application
                        // To only allow users from a single organizations, set ValidateIssuer to true and 'tenant' setting in web.config to the tenant name
                        // To allow users from only a list of specific organizations, set ValidateIssuer to true and use ValidIssuers parameter
                        TokenValidationParameters = new TokenValidationParameters()
                        {
                            ValidateIssuer = false // This is a simplification
                        }                   
                    }
                );
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
