using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using eWallet.Models;
using eWallet.Controllers;
using eWallet.ViewModels;

namespace eWallet.Areas.Admin.Controllers
{
    public class ProductsController : AdminWalletController
    {
        // GET: Admin/Products/Create
        public ActionResult Create(int? id)
        {
            if (id == null)
            {
                AddNotification(Notification.GetError("Bad Request",
                    "No Valid Grant was selected"));
                return RedirectToAction("Index", "Grants");
            }
            var grant = EWalletContext.Grants.Find(id);
            if (grant == null)
            {
                AddNotification(Notification.GetError("Bad Request",
                    "No Valid Grant was selected"));
                return RedirectToAction("Edit", "Grants", new { Id = id });
            }
            var prod = new Product { GrantId = grant.Id };
            return View(prod);
        }

        // POST: Admin/Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Title,Code,Amount,Description,GrantId")] Product product)
        {
            if (ModelState.IsValid)
            {
                EWalletContext.Products.Add(product);
                EWalletContext.SaveChanges();
                return RedirectToAction("Edit", "Grants", new { Id = product.GrantId});
            }
            return View(product);
        }

        // GET: Admin/Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                AddNotification(Notification.GetError("Bad Request",
                    "No Valid Grant was selected"));
                return RedirectToAction("Index", "Grants");
            }
            Product product = EWalletContext.Products.Find(id);
            if (product == null)
            {
                AddNotification(Notification.GetError("Bad Request",
                    "No Valid Product was selected"));
                return RedirectToAction("Index", "Grants");
            }
            return View(product);
        }

        // POST: Admin/Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Code,Amount,Description,GrantId")] Product product)
        {
            if (ModelState.IsValid)
            {
                EWalletContext.Entry(product).State = EntityState.Modified;
                EWalletContext.SaveChanges();
                return RedirectToAction("Edit", "Grants", new { Id = product.GrantId});
            }
            return View(product);
        }

        // POST: Admin/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = EWalletContext.Products.Find(id);
            EWalletContext.Products.Remove(product);
            EWalletContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
