using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using eWallet.Controllers;
using eWallet.Models;
using eWallet.ViewModels;
using eWallet.Areas.Agents.Models;

namespace eWallet.Areas.Agents.Controllers
{
    public class BidController : AgentWalletController
    {
        // GET: Admin/Grants
        public ActionResult Index() {
            var grants = EWalletContext.Grants
                .Where(el => !el.Agents.Any() || el.Agents.Any(pl => pl.AgentId != LoggedInUser.Id))
                .Where(el => el.Status == GrantStatus.Published);

            return View(grants.ToList());
        }

        public ActionResult Apply(int? id)
        {
            if (id == null)
            {
                AddNotification(Notification.GetError("Bad Request",
                    "No Valid Grant was selected"));
                return RedirectToAction("Index");
            }

            var grant = EWalletContext.Grants.Include(g => g.Products).Include(g => g.Agents).Include(g => g.Farmers).Single(g => g.Id == id);
            if (grant == null)
            {
                AddNotification(Notification.GetError("Bad Request",
                    "No Valid Grant was selected"));
                return RedirectToAction("Index");
            }

            var agentGrant = new BidApplyModel {
                Title = grant.Title,
                Description = grant.Description,
                MonetaryValue = grant.MonetaryWorth,
                GrantId = grant.Id };
            return View(agentGrant);

        }

        // POST: Admin/Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Apply(BidApplyModel model)
        {
            if (ModelState.IsValid)
            {
                var agentGrant = new AgentGrant {
                    GrantId = model.GrantId,
                    AgentId = LoggedInUser.Id,
                    Status = AgentGrantStatus.InProgress
                };
                EWalletContext.AgentGrants.Add(agentGrant);
                EWalletContext.SaveChanges();
                return RedirectToAction("Index", "MyGrants");
            }
            return View(model);
        }
    }
}
