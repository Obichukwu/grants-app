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
    public class GrantsController : AdminWalletController
    {
        // GET: Admin/Grants
        public ActionResult Index()
        {
            var grants = EWalletContext.Grants.Include(g => g.Products).Include(g => g.Agents).Include(g => g.Farmers).ToList();
            return View(grants);
        }

        // GET: Admin/Grants/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Grants/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Title,Description,MonetaryWorth")] Grant grant)
        {
            if (ModelState.IsValid)
            {
                grant.Status = GrantStatus.Published;
                EWalletContext.Grants.Add(grant);
                EWalletContext.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(grant);
        }

        // GET: Admin/Grants/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                AddNotification(Notification.GetError("Bad Request",
                    "No Valid Grant was selected"));
                return RedirectToAction("Index");
            }
            Grant grant = EWalletContext.Grants.Include(g => g.Products).Include(g => g.Agents).Include(g => g.Farmers).Single(g=> g.Id == id);
            if (grant == null)
            {
                AddNotification(Notification.GetError("Bad Request",
                    "No Valid Grant was selected"));
                return RedirectToAction("Index");
            }
            return View(grant);
        }

        // POST: Admin/Grants/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Description,MonetaryWorth")] Grant grant)
        {
            if (ModelState.IsValid)
            {
                var dbGrant = EWalletContext.Grants.Single(g => g.Id == grant.Id);
                if (dbGrant != null)
                {
                    dbGrant.Title = grant.Title;
                    dbGrant.Description = grant.Description;
                    dbGrant.MonetaryWorth = grant.MonetaryWorth;

                    EWalletContext.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            return View(grant);
        }


        // POST: Admin/Grants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Grant grant = EWalletContext.Grants.Find(id);
            grant.Status = GrantStatus.Terminated;
            EWalletContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
