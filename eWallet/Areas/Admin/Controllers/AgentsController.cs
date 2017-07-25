using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using eWallet.Controllers;
using eWallet.Models;
using eWallet.ViewModels;

namespace eWallet.Areas.Admin.Controllers
{
    public class AgentsController : AdminWalletController
    {
        public ActionResult Index() {
            var agents = EWalletContext.Agents.Where(el => el.Status != MembershipStatus.Pending).ToList();
            return View(agents);
        }

        public ActionResult PendingList()
        {
            var agents = EWalletContext.Agents.Where(el => el.Status == MembershipStatus.Pending).ToList();
            return View(agents);
        }

        public ActionResult Approve(string id)
        {
            if (id == null)
            {
                AddNotification(Notification.GetError("Bad Request",
                    "No Valid Agent was selected"));
                return RedirectToAction("PendingList");
            }
            var agent = EWalletContext.Agents.Find(id);
            if (agent == null)
            {
                AddNotification(Notification.GetError("Bad Request",
                    "No Valid Agent was selected"));
                return RedirectToAction("PendingList");
            }
            agent.Status = MembershipStatus.Approved;
            EWalletContext.SaveChanges();

            return RedirectToAction("PendingList");
        }

        public ActionResult Details(string id)
        {
            if (id == null)
            {
                AddNotification(Notification.GetError("Bad Request",
                    "No Valid Agent was selected"));
                return RedirectToAction("Index");
            }
            var agent = EWalletContext.Agents.Include(el => el.Grants)
                .Include(el => el.Grants.Select(cl => cl.Grant))
                .FirstOrDefault(el => el.Id== id);

            if (agent == null)
            {
                AddNotification(Notification.GetError("Bad Request",
                    "No Valid Agent was selected"));
                return RedirectToAction("Index");
            }
            return View(agent);
        }

        // POST: Admin/Agents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            var agent = EWalletContext.Agents.Find(id);
            if (agent != null) {
                agent.Status = MembershipStatus.Suspended;
            }
            EWalletContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
