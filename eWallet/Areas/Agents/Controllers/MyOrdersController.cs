using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using eWallet.Controllers;
using eWallet.Models;
using eWallet.ViewModels;
using eWallet.Areas.Farmers.Models;

namespace eWallet.Areas.Agents.Controllers
{
    public class MyOrdersController : AgentWalletController
    {
        public ActionResult Index(int? grantId) {
            var orders = EWalletContext.Orders.Include(el => el.Product)
                .Include(el => el.Farmer).Include(el => el.Agent)
                .Where(el => el.AgentId == LoggedInUser.Id)
                .Where(el => el.Status != OrderStatus.Completed &&
                             el.Status != OrderStatus.Failed);

            if (grantId != null) {
                orders = orders.Where(el => el.Product.GrantId == grantId);
            }

            var grants = EWalletContext
                .AgentGrants.Where(el => el.AgentId == LoggedInUser.Id)
                .Select(el => el.Grant);
            ViewBag.GrantId = new SelectList(grants, "Id", "Title", grantId);
            return View(orders);
        }

        public ActionResult Completed(int? grantId)
        {
            var orders = EWalletContext.Orders.Include(el => el.Product)
                .Include(el => el.Farmer).Include(el => el.Agent)
                .Where(el => el.AgentId == LoggedInUser.Id)
                .Where(el => el.Status == OrderStatus.Completed ||
                             el.Status == OrderStatus.Failed);

            if (grantId != null)
            {
                orders = orders.Where(el => el.Product.GrantId == grantId);
            }

            var grants = EWalletContext
                .AgentGrants.Where(el => el.AgentId == LoggedInUser.Id)
                .Select(el => el.Grant);
            ViewBag.GrantId = new SelectList(grants, "Id", "Title", grantId);
            return View(orders);
        }

        // GET: Admin/Products/Edit/5
        public ActionResult Update(int? id)
        {
            if (id == null)
            {
                AddNotification(Notification.GetError("Bad Request",
                    "No Valid Order was selected"));
                return RedirectToAction("Index");
            }

            var order = EWalletContext.Orders.Find(id);
            if (order == null)
            {
                AddNotification(Notification.GetError("Bad Request",
                    "No Valid Order was selected"));
                return RedirectToAction("Index");
            }

            if (order.Status == OrderStatus.Completed) {
                AddNotification(Notification.GetError("Bad Request",
                    "Cannot update a completed order."));
                return RedirectToAction("Index");
            }
            if (order.Status != OrderStatus.AwaitingAgentDisbursal) {
                AddNotification(Notification.GetError("Bad Request",
                    "Agent's action not required!"));
                return RedirectToAction("Index");
            }
            order.Status = OrderStatus.AwaitingFarmerConfirmation;
            EWalletContext.SaveChanges();

            AddNotification(Notification.GetSuccess("Disbursed",
                "Order successfully disbursed!"));

            return RedirectToAction("Index");
        }
    }
}
