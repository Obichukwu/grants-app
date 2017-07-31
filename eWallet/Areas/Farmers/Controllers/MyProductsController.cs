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
    public class MyProductsController : FarmerWalletController
    {
        public ActionResult Index(int? grantId) {
            List<Product> products;
            if (grantId == null) {
                products = EWalletContext.Products.Include(el => el.Grant)
                    .Where(el =>
                        el.Grant.Farmers.Any(pl=> pl.FarmerId == LoggedInUser.Id))
                    .ToList();
            }
            else {
                products = EWalletContext.Products.Include(el => el.Grant).Where(el => el.GrantId == grantId).ToList();
            }
            var grants = EWalletContext
                .FarmerGrants.Where(el => el.FarmerId == LoggedInUser.Id)
                .Select(el =>el.Grant);
            ViewBag.GrantId = new SelectList(grants, "Id", "Title", grantId);
            return View(products);
        }

        // GET: Admin/Products/Edit/5
        public ActionResult Buy(int? id)
        {
            if (id == null)
            {
                AddNotification(Notification.GetError("Bad Request",
                    "No Valid Product was selected"));
                return RedirectToAction("Index");
            }

            var product = EWalletContext.Products.Find(id);
            if (product == null)
            {
                AddNotification(Notification.GetError("Bad Request",
                    "No Valid Product was selected"));
                return RedirectToAction("Index");
            }

            var grantWallet= EWalletContext.FarmerGrants
                .FirstOrDefault(el => el.FarmerId == LoggedInUser.Id &&
                             el.Status == eWallet.Models.FarmerGrantStatus.Approved &&
                             el.GrantId == product.GrantId);
            var grantWalletBalance = grantWallet?.AvailableAmount ?? 0m;

            var farmer = EWalletContext.Farmers.Find(LoggedInUser.Id);
            var farmerWalletBalance = farmer.GeneralWalletBalance;

            var productModel = new ProductBuyModel {
                GrantId = product.GrantId,
                ProductId = product.Id,
                ProductTitle = product.Title,
                ProductDescription = product.Description,
                ProductCode = product.Code,
                GrantBalance = grantWalletBalance,
                MultiPurposeWalletBalance = farmerWalletBalance,
                ProductQuantity = 1
            };

            var agents = EWalletContext.AgentGrants.Include(el => el.Agent)
                .Where(el => el.Status == AgentGrantStatus.Active && el.GrantId == product.GrantId)
                .Select(el => el.Agent);
            ViewBag.AgentId = new SelectList(agents, "Id", "Name");
            return View(productModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Buy(ProductBuyModel productModel)
        {
            if (ModelState.IsValid)
            {
                if (productModel.ProductQuantity < 1) {
                    AddNotification(Notification.GetError("Bad Request",
                        "Please, enter the number of items you want to buy"));

                    var agents = EWalletContext.AgentGrants.Include(el => el.Agent)
                        .Where(el => el.Status == AgentGrantStatus.Active && el.GrantId == productModel.GrantId)
                        .Select(el => el.Agent);
                    ViewBag.AgentId = new SelectList(agents, "Id", "Name");
                    return View(productModel);
                }
                var product = EWalletContext.Products.Find(productModel.ProductId);
                if (product == null)
                {
                    AddNotification(Notification.GetError("Bad Request",
                        "Please, select a valid product to buy."));

                    var agents = EWalletContext.AgentGrants.Include(el => el.Agent)
                        .Where(el => el.Status == AgentGrantStatus.Active && el.GrantId == productModel.GrantId)
                        .Select(el => el.Agent);
                    ViewBag.AgentId = new SelectList(agents, "Id", "Name");
                    return View(productModel);
                }

                var agent = EWalletContext.Agents.Find(productModel.AgentId);
                if (agent == null)
                {
                    AddNotification(Notification.GetError("Bad Request",
                        "Please, select a valid agent to buy from."));

                    var agents = EWalletContext.AgentGrants.Include(el => el.Agent)
                        .Where(el => el.Status == AgentGrantStatus.Active && el.GrantId == productModel.GrantId)
                        .Select(el => el.Agent);
                    ViewBag.AgentId = new SelectList(agents, "Id", "Name");
                    return View(productModel);
                }

                var grantWallet = EWalletContext.FarmerGrants
                    .FirstOrDefault(el => el.FarmerId == LoggedInUser.Id &&
                                          el.Status == eWallet.Models.FarmerGrantStatus.Approved &&
                                          el.GrantId == product.GrantId);
                var grantWalletBalance = grantWallet?.AvailableAmount ?? 0m;

                var farmer = EWalletContext.Farmers.Find(LoggedInUser.Id);
                var farmerWalletBalance = farmer.GeneralWalletBalance;

                var price = product.Amount * productModel.ProductQuantity;
                if (price > grantWalletBalance + farmerWalletBalance) {
                    AddNotification(Notification.GetError("Bad Request",
                        "Insufficient Fund."));
                    return View(productModel);
                }

                if (price > grantWalletBalance && price > farmerWalletBalance) {
                    var bal = price - grantWalletBalance;

                    if (grantWallet != null) {
                        grantWallet.AvailableAmount -= price;
                        if (grantWallet.AvailableAmount < 0) {
                            grantWallet.AvailableAmount = 0;
                        }
                    }
                    farmer.GeneralWalletBalance -= bal;
                }
                else if (price < farmerWalletBalance){
                    if (grantWallet != null)
                    {
                        grantWallet.AvailableAmount -= price;
                    }
                }
                else // (price < grantWalletBalance)
                {
                    farmer.GeneralWalletBalance -= price;
                }

                var order = new Order {
                    ProductId = productModel.ProductId,
                    AgentId =  productModel.AgentId,
                    FarmerId = LoggedInUser.Id,
                    Quantity = productModel.ProductQuantity,
                    UnitPrice = product.Amount,
                    DateOrdered = DateTimeOffset.Now,
                    Status = OrderStatus.AwaitingAgentDisbursal
                };

                EWalletContext.Orders.Add(order);
                EWalletContext.SaveChanges();

                AddNotification(Notification.GetSuccess("Item Ordered",
                    "An order was successfully created for this purchase."));

                return RedirectToAction("Index");
            }
            return View(productModel);
        }
    }
}
