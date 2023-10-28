using Microsoft.Owin.Builder;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using Owin;
using System.Web;
using System.Web.Mvc;

namespace Avectra.netForum.iWeb.Controllers
{

    /// <summary>
    /// Manage OpenID Connect authentication.
    /// 
    /// Based on https://docs.microsoft.com/en-us/azure/active-directory/develop/tutorial-v2-asp-webapp
    /// </summary>
    public class AuthController : Controller
    {
        /// <summary>
        /// On first hit display the view only.
        /// On next attempt start authentication process.
        /// Send an OpenID Connect sign-in request.
        /// </summary>
        public ActionResult Index()
        {
            IAppBuilder app = new AppBuilder();
            if (!Session.IsNewSession)
            {
                HttpContext.GetOwinContext().Authentication.Challenge(
                        new AuthenticationProperties
                        {
                            RedirectUri = Request.ApplicationPath + "/Home.aspx?" + Request.QueryString.ToString()
                        },
                        OpenIdConnectAuthenticationDefaults.AuthenticationType);
            }
            return View();
        }

        /// <summary>
        /// Send an OpenID Connect sign-out request.
        /// </summary>
        public void SignOut()
        {
            HttpContext.GetOwinContext().Authentication.SignOut(
                    OpenIdConnectAuthenticationDefaults.AuthenticationType,
                    CookieAuthenticationDefaults.AuthenticationType);
        }

     
    }
}

   