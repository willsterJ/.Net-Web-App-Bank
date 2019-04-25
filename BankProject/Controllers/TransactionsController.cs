using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BankProject.Models;

namespace BankProject.Controllers
{
    public class TransactionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Transactions
        [Authorize]
        public ActionResult Index(int accountid)
        {
            var bankaccount = db.BankAccounts.Find(accountid);

            ViewBag.accountnumber = bankaccount.AccountNumber;

            var query = from trans in db.Transactions
                        where trans.BankAccountId == accountid   // select transactions belonging to current account
                        select trans;

            return View(query.ToList());
        }

        // GET: Transactions/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        // GET: Transactions/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Transactions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create(int accountid,[Bind(Include = "Description,Amount,CheckOrDeposit")] Transaction transaction)
        {
            ViewBag.accountid = accountid;

            if (ModelState.IsValid)
            {
                var bankaccount = db.BankAccounts.Find(accountid); // get current bank account
                // get transaction
                if (transaction.CheckOrDeposit == CheckOrDepositEnum.Check)
                {
                    transaction.Balance = bankaccount.Balance - transaction.Amount;
                    // format string
                    transaction.AmountStringFormat = string.Format("{0:0.00}", transaction.Amount);
                    transaction.AmountStringFormat = "(" + transaction.AmountStringFormat + ")";
                }
                else
                {
                    transaction.Balance = bankaccount.Balance + transaction.Amount;
                    transaction.AmountStringFormat = string.Format("{0:0.00}", transaction.Amount);
                }

                // update balance
                bankaccount.Balance = transaction.Balance;

                transaction.Date = DateTime.Now;
                transaction.DateStringFormat = transaction.Date.ToString("MM/dd/yyyy");
                transaction.Status = true;
                transaction.BankAccountId = bankaccount.Id;

                db.Transactions.Add(transaction);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(transaction);
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
