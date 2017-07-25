using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using eWallet.Controllers;
using eWallet.Models;
using eWallet.ViewModels;

namespace eWallet.Areas.Admin.Controllers
{
    public class GrantsAgentController : AdminWalletController
    {
        // GET: Admin/Grants
        public ActionResult Index() {
            var grantRequests = EWalletContext.AgentGrants
                .Where(el => el.Status == AgentGrantStatus.InProgress);

            return View(grantRequests.ToList());
        }

        public ActionResult Reject(int? grantId, string agentId)
        {
            if (grantId == null || agentId == null)
            {
                AddNotification(Notification.GetError("Bad Request",
                    "No Valid Grant Request was selected"));
                return RedirectToAction("Index");
            }

            var grantReq = EWalletContext.AgentGrants
                .Include(g => g.Grant).Include(g => g.Agent)
                .FirstOrDefault(g => g.GrantId == grantId && g.AgentId == agentId);

            if (grantReq == null)
            {
                AddNotification(Notification.GetError("Bad Request",
                    "No Valid Grant Request was selected"));
                return RedirectToAction("Index");
            }

            grantReq.Status = AgentGrantStatus.Rejected;
            EWalletContext.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Approve(int? grantId, string agentId)
        {
            if (grantId == null || agentId == null)
            {
                AddNotification(Notification.GetError("Bad Request",
                    "No Valid Grant Request was selected"));
                return RedirectToAction("Index");
            }

            var grantReq = EWalletContext.AgentGrants
                .Include(g => g.Grant).Include(g => g.Agent)
                .FirstOrDefault(g => g.GrantId == grantId && g.AgentId == agentId);

            if (grantReq == null)
            {
                AddNotification(Notification.GetError("Bad Request",
                    "No Valid Grant Request was selected"));
                return RedirectToAction("Index");
            }

            return View(grantReq);
        }

        // POST: Admin/Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Approve(AgentGrant model)
        {
            if (ModelState.IsValid)
            {
                var farmerGrant = EWalletContext.AgentGrants
                    .FirstOrDefault(g => g.GrantId == model.GrantId
                        && g.AgentId == model.AgentId);
                if (farmerGrant == null)
                {
                    AddNotification(Notification.GetError("Bad Request",
                        "No Valid Grant Request was selected"));
                    return RedirectToAction("Index");
                }

                farmerGrant.Status = AgentGrantStatus.Active;

                EWalletContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}
