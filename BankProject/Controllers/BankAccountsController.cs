using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BankProject.Models;
using Microsoft.AspNet.Identity;

namespace BankProject.Controllers
{
    public class BankAccountsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: BankAccounts
        [Authorize]
        public ActionResult List()  // Also the Index page
        {
            string userID = User.Identity.GetUserId();

            var query = from accounts in db.BankAccounts
                       where string.Equals(accounts.UserId, userID) && accounts.Status == true
                       select accounts;

            ViewBag.UserName = db.Users.Find(userID).UserName;

            return View(query.ToList());
        }

        // GET: BankAccounts/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BankAccount bankAccount = db.BankAccounts.Find(id);
            if (bankAccount == null)
            {
                return HttpNotFound();
            }
            return View(bankAccount);
        }

        // GET: BankAccounts/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: BankAccounts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "Id,AccountNumber,Name,Balance")] BankAccount bankAccount)
        {
            if (ModelState.IsValid)
            {
                // check if Account Number already exists in the database
                var accs = from acc in db.BankAccounts
                           where acc.AccountNumber == bankAccount.AccountNumber
                           select acc;
                if (accs.FirstOrDefault() != null)
                {
                    Response.Redirect("/Bank/Account/Create");
                }
                // populate the other properties of bank account
                bankAccount.Balance = 0;
                bankAccount.Status = true;
                bankAccount.UserId = User.Identity.GetUserId();   // !!! workaround to setup foreign key since it was not automatically generated


                db.BankAccounts.Add(bankAccount);
                db.SaveChanges();
                return RedirectToAction("List");
            }

            return View(bankAccount);
        }


        // GET: BankAccounts/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BankAccount bankAccount = db.BankAccounts.Find(id);
            if (bankAccount == null)
            {
                return HttpNotFound();
            }
            return View(bankAccount);
        }

        // POST: BankAccounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            BankAccount bankAccount = db.BankAccounts.Find(id);
            bankAccount.Status = false;
            db.SaveChanges();
            return RedirectToAction("List");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
