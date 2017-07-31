using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using eWallet.Controllers;
using eWallet.Models;
using eWallet.ViewModels;

namespace eWallet.Areas.Admin.Controllers
{
    public class GrantsFarmerController : AdminWalletController
    {
        // GET: Admin/Grants
        public ActionResult Index() {
            var grantRequests = EWalletContext.FarmerGrants
                .Where(el => el.Status == FarmerGrantStatus.InProgress);

            return View(grantRequests.ToList());
        }

        public ActionResult Reject(int? grantId, string farmerId)
        {
            if (grantId == null || farmerId == null)
            {
                AddNotification(Notification.GetError("Bad Request",
                    "No Valid Grant Request was selected"));
                return RedirectToAction("Index");
            }

            var grantReq = EWalletContext.FarmerGrants
                .Include(g => g.Grant).Include(g => g.Farmer)
                .FirstOrDefault(g => g.GrantId == grantId && g.FarmerId == farmerId);

            if (grantReq == null)
            {
                AddNotification(Notification.GetError("Bad Request",
                    "No Valid Grant Request was selected"));
                return RedirectToAction("Index");
            }

            grantReq.Status = FarmerGrantStatus.Rejected;
            EWalletContext.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Approve(int? grantId, string farmerId)
        {
            if (grantId == null || farmerId == null)
            {
                AddNotification(Notification.GetError("Bad Request",
                    "No Valid Grant Request was selected"));
                return RedirectToAction("Index");
            }

            var grantReq = EWalletContext.FarmerGrants
                .Include(g => g.Grant).Include(g => g.Farmer)
                .FirstOrDefault(g => g.GrantId == grantId && g.FarmerId == farmerId);

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
        public ActionResult Approve(FarmerGrant model)
        {
            if (ModelState.IsValid)
            {
                var farmerGrant = EWalletContext.FarmerGrants
                    .FirstOrDefault(g => g.GrantId == model.GrantId
                        && g.FarmerId == model.FarmerId);
                if (farmerGrant == null)
                {
                    AddNotification(Notification.GetError("Bad Request",
                        "No Valid Grant Request was selected"));
                    return RedirectToAction("Index");
                }
                var approvedSum = farmerGrant.Grant.Farmers.Where(el => el.Status== FarmerGrantStatus.Approved).Sum(el => el.ApprovedAmount);
                if (approvedSum + model.ApprovedAmount > farmerGrant.Grant.MonetaryWorth)
                {
                    AddNotification(Notification.GetError("Error",
                        "Amount exceeds available monetary value"));
                    return View(model);
                }
                farmerGrant.ApprovedAmount = model.ApprovedAmount;
                farmerGrant.AvailableAmount = model.ApprovedAmount;
                farmerGrant.Status = FarmerGrantStatus.Approved;

                EWalletContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}
