using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using eWallet.Controllers;
using eWallet.Models;
using eWallet.ViewModels;

namespace eWallet.Areas.Agents.Controllers
{
    public class MyGrantsController : AgentWalletController
    {
        // GET: Admin/Grants
        public ActionResult Index() {
            var grants = EWalletContext.AgentGrants.Include(el => el.Grant)
                .Include(el => el.Grant.Products).Where(el => el.AgentId == LoggedInUser.Id);

            return View(grants);
        }
    }
}
