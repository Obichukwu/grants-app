using System.Data.Entity;
using eWallet.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eWallet.Areas.Agents.Models;

namespace eWallet.Areas.Agents.Controllers
{
    public class DashboardController : AgentWalletController
    {
        // GET: Agents/Dashboard
        public ActionResult Index()
        {
            var grants = EWalletContext.AgentGrants
                .Include(el => el.Grant)
                .Where(el => el.AgentId == LoggedInUser.Id).ToList();

            var approvedGrants = grants
                .Where(el => el.Status == eWallet.Models.AgentGrantStatus.Active).ToList();
            var pendingGrants = grants
                .Where(el => el.Status == eWallet.Models.AgentGrantStatus.InProgress).ToList();

            var dashModel = new DashboardModel
            {
                GrantCount = approvedGrants.Count,
                PendingGrantCount = pendingGrants.Count,
                PendingWithdrawal = 0,
                GrantSold = approvedGrants.Sum(el => el.Balance),
                CompletedOrdersCount = 1,

                ApprovedGrants = approvedGrants,
                PendingGrants = pendingGrants
            };
            return View(dashModel);
        }
    }
}