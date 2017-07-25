using System.Data.Entity;
using eWallet.Areas.Admin.Models;
using eWallet.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eWallet.Models;

namespace eWallet.Areas.Admin.Controllers
{
    public class DashboardController : AdminWalletController
    {
        // GET: Farmers/Dashboard
        public ActionResult Index()
        {
            var grants = EWalletContext.Grants.Include(el => el.Farmers).ToList();
            var agents = EWalletContext.Agents.ToList();
            var farmers = EWalletContext.Farmers.ToList();

            var approvedGrants = grants
                .Where(el => el.Status != GrantStatus.Terminated).ToList();
            var terminatedGrants = grants
                .Where(el => el.Status == GrantStatus.Terminated).ToList();

            var approvedAgents = agents.Count(el => el.Status == MembershipStatus.Approved);
            var pendingAgents = agents.Count(el => el.Status != MembershipStatus.Approved);

            var approvedFarmers = farmers.Count(el => el.Status == MembershipStatus.Approved);
            var pendingFarmers = farmers.Count(el => el.Status != MembershipStatus.Approved);

            var dashModel = new DashboardModel
            {
                GrantCount = approvedGrants.Count,
                TerminatedGrantCount = terminatedGrants.Count,

                AgentCount = approvedAgents,
                PendingAgentCount = pendingAgents,
                FarmerCount = approvedFarmers,
                PendingFarmerCount = pendingFarmers,

                ApprovedGrants = approvedGrants,
                TerminatedGrants = terminatedGrants
            };

            return View(dashModel);
        }
    }
}