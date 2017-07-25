using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Newtonsoft.Json;
using eWallet.Models;
using eWallet.ViewModels;

namespace eWallet.Controllers
{
    [Authorize(Roles =nameof(UserType.Agent))]
    public class AgentWalletController : WalletController
    { }
    [Authorize(Roles = nameof(UserType.Farmer))]
    public class FarmerWalletController : WalletController
    { }
    [Authorize(Roles = nameof(UserType.Admin))]
    public class AdminWalletController : WalletController
    { }

    public class WalletController : Controller
    {
        public eWalletContext EWalletContext
        {
            get
            {
                return _ewalletContext ?? HttpContext.GetOwinContext().GetUserManager<eWalletContext>();
            }
            set
            {
                _ewalletContext = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            set
            {
                _userManager = value;
            }
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.IsChildAction)
                return;
            //Log.Information("Executing request to {0}-{1} from {2} on {3}",
            //        filterContext.ActionDescriptor.ControllerDescriptor.ControllerName,
            //        filterContext.ActionDescriptor.ActionName,
            //        filterContext.HttpContext.Request.UserHostAddress,
            //        filterContext.HttpContext.Timestamp);

            base.OnActionExecuting(filterContext);
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (filterContext.IsChildAction)
                return;
            //Log.Information("Finished executing request to {0}-{1} from {2} on {3}",
            //        filterContext.ActionDescriptor.ControllerDescriptor.ControllerName,
            //        filterContext.ActionDescriptor.ActionName,
            //        filterContext.HttpContext.Request.UserHostAddress,
            //        filterContext.HttpContext.Timestamp);

            base.OnActionExecuted(filterContext);
        }

        private User _loggedInUser;
        private ApplicationUserManager _userManager;
        private eWalletContext _ewalletContext;
        protected User LoggedInUser => _loggedInUser ?? (_loggedInUser = UserManager.FindById(User.Identity.GetUserId()));

        protected void AddNotification(Notification notification)
        {
            if (TempData["Notifications"] == null)
            {
                TempData["Notifications"] = new List<Notification>();
            }
            (TempData["Notifications"] as List<Notification>)?.Add(notification);
        }

        public static string GetJson(object value)
        {
            var serializer = new JsonSerializer();
            var stringWriter = new System.IO.StringWriter();
            using (var writer = new JsonTextWriter(stringWriter))
            {
                writer.QuoteName = false;
                serializer.Serialize(writer, value);
            }
            return stringWriter.ToString();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_ewalletContext != null)
                {
                    _ewalletContext.Dispose();
                    _ewalletContext = null;
                }
            }

            base.Dispose(disposing);
        }
    }

    public class WalletSigningController :WalletController
    {
        private ApplicationSignInManager _signInManager;

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            set
            {
                _signInManager = value;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }
    }
}