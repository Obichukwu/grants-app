using System.Data.Entity;
using eWallet.Areas.Farmers.Models;
using eWallet.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eWallet.Areas.Farmers.Controllers
{
    public class DashboardController : FarmerWalletController
    {
        // GET: Farmers/Dashboard
        public ActionResult Index()
        {
            var grants = EWalletContext.FarmerGrants
                .Include(el => el.Grant)
                .Where(el => el.FarmerId == LoggedInUser.Id).ToList();

            var approvedGrants = grants
                .Where(el => el.Status == eWallet.Models.FarmerGrantStatus.Approved).ToList();
            var pendingGrants = grants
                .Where(el => el.Status == eWallet.Models.FarmerGrantStatus.InProgress).ToList();

            var amtApproved = approvedGrants.Sum(el => el.ApprovedAmount);
            var amtAvailable = approvedGrants.Sum(el => el.AvailableAmount);

            var farmer = EWalletContext.Farmers.Find(LoggedInUser.Id);

            var dashModel = new DashboardModel {
                GrantCount = approvedGrants.Count,
                PendingGrantCount = pendingGrants.Count,
                GrantBalance =amtAvailable,
                GrantSpent = amtApproved-amtAvailable,
                GeneralBalance = farmer.GeneralWalletBalance,
                CompletedOrdersCount =1,

                ApprovedGrants =approvedGrants,
                PendingGrants = pendingGrants
            };
            return View(dashModel);
        }
    }
}