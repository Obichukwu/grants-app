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

namespace eWallet.Areas.Farmers.Controllers
{
    public class MyOrdersController : FarmerWalletController
    {
        public ActionResult Index(int? grantId) {
            var orders = EWalletContext.Orders.Include(el => el.Product)
                .Include(el => el.Farmer).Include(el => el.Agent)
                .Where(el => el.FarmerId == LoggedInUser.Id)
                .Where(el => el.Status != OrderStatus.Completed &&
                             el.Status != OrderStatus.Failed);

            if (grantId != null) {
                orders = orders.Where(el => el.Product.GrantId == grantId);
            }

            var grants = EWalletContext
                .FarmerGrants.Where(el => el.FarmerId == LoggedInUser.Id)
                .Select(el => el.Grant);
            ViewBag.GrantId = new SelectList(grants, "Id", "Title", grantId);
            return View(orders);
        }

        public ActionResult Completed(int? grantId)
        {
            var orders = EWalletContext.Orders.Include(el => el.Product)
                .Include(el => el.Farmer).Include(el => el.Agent)
                .Where(el => el.FarmerId == LoggedInUser.Id)
                .Where(el => el.Status == OrderStatus.Completed ||
                             el.Status == OrderStatus.Failed);

            if (grantId != null)
            {
                orders = orders.Where(el => el.Product.GrantId == grantId);
            }

            var grants = EWalletContext
                .FarmerGrants.Where(el => el.FarmerId == LoggedInUser.Id)
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
            if (order.Status == OrderStatus.AwaitingAgentDisbursal) {
                AddNotification(Notification.GetError("Bad Request",
                    "Agent's action required!"));
                return RedirectToAction("Index");
            }

            return View(order);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(Order orderModel)
        {
            if (ModelState.IsValid)
            {
                if (orderModel.Status != OrderStatus.Failed &&
                    orderModel.Status != OrderStatus.Completed)
                {
                    AddNotification(Notification.GetError("Bad Request",
                        "You can only update an order to completed or to failure!"));
                    return View(orderModel);
                }

                var order = EWalletContext.Orders.Find(orderModel.Id);
                if (order == null)
                {
                    AddNotification(Notification.GetError("Bad Request",
                        "Please, select a valid order to buy."));
                    return View(orderModel);
                }

                if (order.Status != OrderStatus.AwaitingFarmerConfirmation)
                {
                    AddNotification(Notification.GetError("Bad Request",
                        "Farmer's action not required!"));
                    return RedirectToAction("Index");
                }


                var agent = EWalletContext.Agents.Find(order.AgentId);
                if (agent == null)
                {
                    AddNotification(Notification.GetError("Bad Request",
                        "Please, select a valid agent to buy from."));
                    return View(orderModel);
                }

                var farmer = EWalletContext.Farmers.Find(LoggedInUser.Id);
                var farmerGrant = farmer.Grants
                    .FirstOrDefault(el => el.GrantId == order.Product.GrantId);

                var agentGrant = agent.Grants
                    .FirstOrDefault(el => el.GrantId == order.Product.GrantId);
                var amount = order.Quantity * order.UnitPrice;
                if (orderModel.Status == OrderStatus.Completed) {
                    agentGrant.PendingBalance -= amount;
                    agentGrant.Balance += amount;
                }
                else {
                    agentGrant.PendingBalance -= amount;
                    farmerGrant.AvailableAmount += amount;
                }

                order.Status = orderModel.Status;
                EWalletContext.SaveChanges();

                AddNotification(Notification.GetSuccess("Order Updated",
                    "You order was updated successfully"));

                return RedirectToAction("Index");
            }
            return View(orderModel);
        }
    }
}
