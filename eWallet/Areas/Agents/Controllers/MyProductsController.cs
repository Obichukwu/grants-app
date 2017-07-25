using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using eWallet.Controllers;
using eWallet.Models;

namespace eWallet.Areas.Agents.Controllers
{
    public class MyProductsController : AgentWalletController
    {
        public ActionResult Index(int? grantId) {
            List<Product> products;
            if (grantId == null) {
                products = EWalletContext.Products.Include(el => el.Grant)
                    .Where(el =>
                        el.Grant.Agents.Any(pl=> pl.AgentId == LoggedInUser.Id))
                    .ToList();
            }
            else {
                products = EWalletContext.Products.Include(el => el.Grant).Where(el => el.GrantId == grantId).ToList();
            }
            var grants = EWalletContext
                .AgentGrants.Where(el => el.AgentId == LoggedInUser.Id)
                .Select(el =>el.Grant);
            ViewBag.GrantId = new SelectList(grants, "Id", "Title", grantId);
            return View(products);
        }

        // GET: Admin/Products/Edit/5
        public ActionResult Detail(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = EWalletContext.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }
    }
}
