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
    public class FarmersController : AdminWalletController
    {
        // GET: Admin/Farmers
        public ActionResult Index() {
            var farmers = EWalletContext.Farmers.Where(el => el.Status != MembershipStatus.Pending).ToList();
            return View(farmers);
        }

        public ActionResult PendingList()
        {
            var farmers = EWalletContext.Farmers.Where(el => el.Status == MembershipStatus.Pending).ToList();
            return View(farmers);
        }

        public ActionResult Approve(string id)
        {
            if (id == null)
            {
                AddNotification(Notification.GetError("Bad Request",
                    "No Valid Farmer was selected"));
                return RedirectToAction("PendingList");
            }
            var farmer = EWalletContext.Farmers.Find(id);
            if (farmer == null)
            {
                AddNotification(Notification.GetError("Bad Request",
                    "No Valid Farmer was selected"));
                return RedirectToAction("PendingList");
            }
            farmer.Status = MembershipStatus.Approved;
            EWalletContext.SaveChanges();

            return RedirectToAction("PendingList");
        }

        public ActionResult Details(string id)
        {
            if (id == null)
            {
                AddNotification(Notification.GetError("Bad Request",
                    "No Valid Farmer was selected"));
                return RedirectToAction("Index");
            }
            var farmer = EWalletContext.Farmers.Include(el => el.Grants)
                .Include(el => el.Grants.Select(cl => cl.Grant))
                .FirstOrDefault(el => el.Id== id);

            if (farmer == null)
            {
                AddNotification(Notification.GetError("Bad Request",
                    "No Valid Farmer was selected"));
                return RedirectToAction("Index");
            }
            return View(farmer);
        }

        // POST: Admin/Farmers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            var farmer = EWalletContext.Farmers.Find(id);
            if (farmer != null) {
                farmer.Status = MembershipStatus.Suspended;
            }
            EWalletContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
