using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using eWallet.Areas.Admin.Models;
using eWallet.Controllers;
using eWallet.Models;
using eWallet.ViewModels;
using Microsoft.AspNet.Identity;

namespace eWallet.Areas.Admin.Controllers
{
    public class UsersController : AdminWalletController
    {
        // GET: Admin/Administrators
        public ActionResult Index()
        {
            return View(EWalletContext.Administrators.ToList());
        }

        public ActionResult Details(string id)
        {
            var appUser = EWalletContext.Administrators
                .FirstOrDefault(el => el.Id == id);
            if (appUser == null)
            {
                AddNotification(Notification.GetError("Bad Request",
                    "No Valid User was selected to view"));
                return RedirectToAction("Index");
            }
            return View(appUser);
        }

        // GET: Admin/Users/Create
        public ActionResult Create()
        {
            var userVm = new UsersCreateViewModel();
            return View(userVm);
        }

        // POST: Admin/Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UsersCreateViewModel userCreateVm)
        {
            if (ModelState.IsValid)
            {
                var user = new Administrator
                {
                    UserName = userCreateVm.UserName,
                    Email = userCreateVm.Email,
                    UserRole = UserType.Admin
                };

                var result = UserManager.CreateAsync(user, userCreateVm.Password).Result;
                if (result.Succeeded)
                {
                    //UserManager.AddToRole(user.Id, nameof(UserType.Administrator));
                    //await SendEmailConfirmationTokenAsync(user.Id, user.Email, "Confirm your account");
                    return RedirectToAction("Index");
                }
                AddErrors(result);
            }
            return View(userCreateVm);
        }

        public ActionResult Reset(string id)
        {
            var user = UserManager.FindByName(id);
            if (user == null)
            {
                AddNotification(Notification.GetError("Bad Request",
                    "No Valid User was selected to reset password"));
                return RedirectToAction("Index");
            }
            var vm = new UsersUpdatePasswordViewModel { Username = user.UserName };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Reset(UsersUpdatePasswordViewModel updatePassword)
        {
            if (ModelState.IsValid)
            {
                var user = UserManager.FindByName(updatePassword.Username);
                if (user == null)
                {
                    AddNotification(Notification.GetError("Bad Request",
                        "No Valid User was selected to reset password"));
                    return RedirectToAction("Index");
                }

                var result = UserManager.RemovePassword(user.Id);
                if (result == IdentityResult.Success)
                {
                    UserManager.AddPassword(user.Id, updatePassword.Password);

                    AddNotification(Notification.GetSuccess("User Password Update Success", "User's password update successful."));
                    return RedirectToAction("Details", new { id = updatePassword.Username });
                }
                AddErrors(result);
            }
            AddNotification(Notification.GetError("User Password Update Failed", "User's password update failed, Cannot update Password."));
            return View(updatePassword);
        }

        // POST: Admin/Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            var user = UserManager.FindByName(id);
            if (user == null)
            {
                AddNotification(Notification.GetError("Bad Request",
                    "No Valid User was selected to delete"));
                return RedirectToAction("Index");
            }

            var result = UserManager.Delete(user);
            if (result == IdentityResult.Success)
            {
                AddNotification(Notification.GetSuccess("User Delete Success", "User deletion successful."));
                return RedirectToAction("Index");
            }
            AddNotification(Notification.GetError("User Delete Failed", "User deletion failed, Cannot delete user."));
            return RedirectToAction("Details", new { id });
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
    }
}
