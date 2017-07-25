using eWallet.Areas.Farmers.Models;
using eWallet.Controllers;
using eWallet.Extensions;
using System.Data.Entity;
using eWallet.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eWallet.Areas.Farmers.Controllers
{
    public class MyWalletController : FarmerWalletController
    {
        // GET: Farmers/MyWallet
        public ActionResult Index()
        {
            var walletBalances = EWalletContext.FarmerGrants.Include(el => el.Grant)
                .Include(el => el.Grant.Products)
                .Where(el => el.FarmerId == LoggedInUser.Id &&
                    el.Status == eWallet.Models.FarmerGrantStatus.Approved)
                .Select(el => new MyWalletIndexModel
                {
                    Title= el.Grant.Title,
                    ApprovedAmount = el.ApprovedAmount,
                    AvailableAmount = el.AvailableAmount
                }).ToList();
            var farmer = EWalletContext.Farmers.Find(LoggedInUser.Id);
            walletBalances.Insert(0, new MyWalletIndexModel
            {
                Title = "General Wallet",
                ApprovedAmount = farmer.GeneralWalletBalance,
                AvailableAmount = farmer.GeneralWalletBalance
            });
            return View(walletBalances);
        }

        public ActionResult FundWallet()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FundWallet(decimal amount)
        {
            var eTranzactPayment = GetETranzactPaymentInfo(amount);
            return View("FundWalletButton", eTranzactPayment);
        }

        public ActionResult FundWalletResponse(int id)
        {
            var farmer = EWalletContext.Farmers.Find(id);
            if (farmer == null)
            {
                AddNotification(Notification.GetError("Bad Request",
                    "No Valid farmer was selected to make fund wallet!"));
                return RedirectToAction("Index", "MyWallet", new { Id = id });
            }

            var success = Request.QueryString["Success"];
            if (success == "0")
            {
                //Transaction Successful
                var amountStr = Request.QueryString["DEBITED_AMOUNT"];
                decimal amount;
                decimal.TryParse(amountStr, out amount);
                var eTranzactPayment = GetETranzactPaymentInfo(amount);

                var finalCheckSum =
                        OnlinePaymenttHelper.GetEtranzactFinalCheckSum(success,
                            eTranzactPayment.TotalAmount.ToString(CultureInfo.InvariantCulture),
                            eTranzactPayment.TerminalId,
                            eTranzactPayment.TransactionId,
                            eTranzactPayment.ResponseUrl,
                            eTranzactPayment.SecretKey);

                if (string.Equals(finalCheckSum,
                    Request.QueryString["FINAL_CHECKSUM"], StringComparison.Ordinal))
                {
                    //invoice.History.Add(new InvoiceLog
                    //{
                    //    Date = DateTimeOffset.Now,
                    //    Title = "Payment Success",
                    //    Message = "Payment via eTranzact Successful. " +
                    //              $"Using CardHolder: {Request.QueryString["CARD_HOLDER_NAME"]}" +
                    //              $"Using CardNo: {Request.QueryString["CARD_NO"]}" +
                    //              $"Using Debit Amount: {Request.QueryString["DEBITED_AMOUNT"]}" +
                    //              $"Using Debit Currency: {Request.QueryString["DEBIT_CURRENCY"]}"
                    //});
                    farmer.GeneralWalletBalance += amount;

                    EWalletContext.SaveChanges();

                    AddNotification(Notification.GetSuccess("Payment Successful", $"Your account was successfully funded."));
                    return RedirectToAction("Index");
                }
            }

            //Transaction Not Successful
            AddNotification(Notification.GetError("Transaction not Authorised",
                $"Error while requesting for transaction authorisation: {GetErrorDescription(success)}"));

            return RedirectToAction("FundWallet");
        }

        private EtranzactPayment GetETranzactPaymentInfo(decimal amount)
        {
            var eTranzactPayment = new EtranzactPayment
            {
                ResponseUrl = Url.Action("FundWalletResponse", "MyWallet", new { LoggedInUser.Id }, Request.Url.Scheme),
                Email = LoggedInUser.Email,
                TransactionId = Guid.NewGuid().ToString(),
                InvoiceTitle = "Wallet Funding For Farmer",
                TotalAmount = amount
            };

            eTranzactPayment.TerminalId = ConfigurationManager.AppSettings["etranzact:test:terminal-id"];
            eTranzactPayment.SecretKey = ConfigurationManager.AppSettings["etranzact:test:secret-key"];
            eTranzactPayment.Url = ConfigurationManager.AppSettings["etranzact:test:url"];

            eTranzactPayment.CheckSum =
                OnlinePaymenttHelper.GetEtranzactCheckSum(
                    eTranzactPayment.TotalAmount.ToString(CultureInfo.InvariantCulture),
                    eTranzactPayment.TerminalId,
                    eTranzactPayment.TransactionId,
                    eTranzactPayment.ResponseUrl,
                    eTranzactPayment.SecretKey);

            return eTranzactPayment;
        }

        private static string GetErrorDescription(string errorcode)
        {
            int code;
            var status = int.TryParse(errorcode, out code);
            if (!status)
            {
                return string.Equals(errorcode, "CA", StringComparison.OrdinalIgnoreCase) ? "Transaction Cancelled" : "";
            }
            switch (code)
            {
                case 1:
                    return "Destination Card Not Found";
                case 2:
                    return " Card Number Not Found";
                case 3:
                    return " Invalid Card PIN";
                case 4:
                    return " Card Expiration Incorrect";
                case 5:
                    return " Insufficient balance";
                case 6:
                    return " Spending Limit Exceeded";
                case 7:
                    return " Internal System Error Occurred, please contact the service provider";
                case 8:
                    return " Financial Institution cannot authorize transaction, Please try later";
                case 9:
                    return " PIN tries Exceeded";
                case 10:
                    return " Card has been locked";
                case 11:
                    return " Invalid Terminal Id";
                case 12:
                    return " Payment Timeout";
                case 13:
                    return " Destination card has been locked";
                case 14:
                    return " Card has expired";
                case 15:
                    return " PIN change required";
                case 16:
                    return " Invalid Amount";
                case 17:
                    return " Card has been disabled";
                case 18:
                    return " Unable to credit this account immediately, credit will be done later";
                case 19:
                    return " Transaction not permitted on terminal";
                case 20:
                    return " Exceeds withdrawal frequency";
                case 21:
                    return " Destination Card has expired";
                case 22:
                    return " Destination Card Disabled";
                case 23:
                    return " Source Card Disabled";
                case 24:
                    return " Invalid Bank Account";
                case 25:
                    return " Insufficient Balance";
                case 1002:
                    return " CHECKSUM / FINAL_CHECKSUM error";
                case 100:
                    return " Duplicate session id";
                case 200:
                    return " Invalid client id";
                case 300:
                    return " Invalid mac";
                case 400:
                    return " Expired session";
                case 500:
                    return
                        " You have entered an account number that is not tied to your phone number with bank.Pls contact your bank for assistance.";
                case 600:
                    return " Invalid account id";
                case 700:
                    return " Security violation Please contact support@etranzact.com";
                case 800:
                    return " Invalid esa code";
                case 900:
                    return " Transaction limit exceeded";
                default:
                    return "";
            }
        }
    }
}