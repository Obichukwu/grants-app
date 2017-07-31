using System.Data.Entity;
using eWallet.Areas.Farmers.Models;
using eWallet.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eWallet.Models;

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

            var orders = EWalletContext.Orders
                .Count(el => el.FarmerId == LoggedInUser.Id && el.Status== OrderStatus.Completed);
            var farmer = EWalletContext.Farmers.Find(LoggedInUser.Id);

            var dashModel = new DashboardModel {
                GrantCount = approvedGrants.Count,
                PendingGrantCount = pendingGrants.Count,
                GrantBalance = amtAvailable,
                GrantSpent = amtApproved-amtAvailable,
                GeneralBalance = farmer.GeneralWalletBalance,
                CompletedOrdersCount = orders,

                ApprovedGrants =approvedGrants,
                PendingGrants = pendingGrants
            };
            return View(dashModel);
        }
    }
}