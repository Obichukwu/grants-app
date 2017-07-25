using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using eWallet.Controllers;
using eWallet.Models;
using eWallet.ViewModels;
using eWallet.Areas.Farmers.Models;

namespace eWallet.Areas.Farmers.Controllers
{
    public class MyGrantsController : FarmerWalletController
    {
        public ActionResult Index() {
            var grants = EWalletContext.FarmerGrants.Include(el => el.Grant)
                .Include(el => el.Grant.Products).Where(el => el.FarmerId == LoggedInUser.Id);

            return View(grants);
        }

        public ActionResult OpenGrants()
        {
            var grants = EWalletContext.Grants
                .Where(el => !el.Farmers.Any() || el.Farmers.Any(pl => pl.FarmerId != LoggedInUser.Id))
                .Where(el => el.Status == GrantStatus.Published).ToList();

            return View(grants);
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

            var farmerGrant = new BidApplyModel
            {
                Title = grant.Title,
                Description = grant.Description,
                MonetaryValue = grant.MonetaryWorth,
                GrantId = grant.Id
            };
            return View(farmerGrant);

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
                var farmerGrant = new FarmerGrant
                {
                    GrantId = model.GrantId,
                    FarmerId = LoggedInUser.Id,
                    Status = FarmerGrantStatus.InProgress,
                    ApprovedAmount = model.Amount
                };
                EWalletContext.FarmerGrants.Add(farmerGrant);
                EWalletContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}
