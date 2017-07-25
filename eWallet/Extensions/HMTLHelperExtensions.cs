using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using eWallet.Models;
using Microsoft.AspNet.Identity.Owin;

namespace eWallet.Extensions
{
    public static class HmtlHelperExtensions
    {
        public static string IsSelected(this HtmlHelper html, string controller = null, string action = null, string area = null, string cssClass = null)
        {
            if (string.IsNullOrEmpty(cssClass))
                cssClass = "active";

            var currentAction = (string)html.ViewContext.RouteData.Values["action"];
            var currentController = (string)html.ViewContext.RouteData.Values["controller"];
            var currentArea = (string)html.ViewContext.RouteData.DataTokens["area"];

            if (string.IsNullOrEmpty(area))
                area = currentArea;

            if (string.IsNullOrEmpty(controller))
                controller = currentController;

            if (string.IsNullOrEmpty(action))
                action = currentAction;

            return area == currentArea && controller == currentController && action == currentAction ?
                cssClass : string.Empty;
        }

        public static bool CurrentUserIsAdmin(this HtmlHelper html) {
            if (!HttpContext.Current.Request.IsAuthenticated) {
                return false;
            }
            var userManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var user = userManager.FindById(HttpContext.Current.User.Identity.GetUserId());
            return userManager.IsInRole(user.Id,nameof(UserType.Admin));
        }
        public static bool CurrentUserIsFarmer(this HtmlHelper html)
        {
            if (!HttpContext.Current.Request.IsAuthenticated)
            {
                return false;
            }
            var userManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var user = userManager.FindById(HttpContext.Current.User.Identity.GetUserId());
            return userManager.IsInRole(user.Id, nameof(UserType.Farmer));
        }

        public static bool CurrentUserIsAgent(this HtmlHelper html)
        {
            if (!HttpContext.Current.Request.IsAuthenticated)
            {
                return false;
            }
            var userManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var user = userManager.FindById(HttpContext.Current.User.Identity.GetUserId());
            return userManager.IsInRole(user.Id, nameof(UserType.Agent));
        }
    }
}
