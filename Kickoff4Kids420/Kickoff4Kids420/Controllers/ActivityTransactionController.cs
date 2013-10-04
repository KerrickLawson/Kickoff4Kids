using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kickoff4Kids420.Models;

namespace Kickoff4Kids420.Controllers
{
    public class ActivityTransactionController : Controller
    {
        private Kickoff4KidsDb db = new Kickoff4KidsDb();

        //
        // GET: /ActivityTransaction/

        public ActionResult Index()
        {
            var activitytransactions = db.ActivityTransactions.Include(a => a.UserProfiles).Include(a => a.Activity);
            return View(activitytransactions.ToList());
        }

        //
        // GET: /ActivityTransaction/Details/5

        public ActionResult Details(int id = 0)
        {
            ActivityTransaction activitytransaction = db.ActivityTransactions.Find(id);
            if (activitytransaction == null)
            {
                return HttpNotFound();
            }
            return View(activitytransaction);
        }

        //
        // GET: /ActivityTransaction/Create

        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.UserProfiles, "UserId", "UserName");
            ViewBag.ActivityId = new SelectList(db.Activities, "ActivityId", "ActivityName");
            return View();
        }

        //
        // POST: /ActivityTransaction/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ActivityTransaction activitytransaction)
        {
            if (ModelState.IsValid)
            {
                db.ActivityTransactions.Add(activitytransaction);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.UserProfiles, "UserId", "UserName", activitytransaction.UserId);
            ViewBag.ActivityId = new SelectList(db.Activities, "ActivityId", "ActivityName", activitytransaction.ActivityId);
            return View(activitytransaction);
        }

        //
        // GET: /ActivityTransaction/Edit/5

        public ActionResult Edit(int id = 0)
        {
            ActivityTransaction activitytransaction = db.ActivityTransactions.Find(id);
            if (activitytransaction == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.UserProfiles, "UserId", "UserName", activitytransaction.UserId);
            ViewBag.ActivityId = new SelectList(db.Activities, "ActivityId", "ActivityName", activitytransaction.ActivityId);
            return View(activitytransaction);
        }

        //
        // POST: /ActivityTransaction/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ActivityTransaction activitytransaction)
        {
            if (ModelState.IsValid)
            {
                db.Entry(activitytransaction).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.UserProfiles, "UserId", "UserName", activitytransaction.UserId);
            ViewBag.ActivityId = new SelectList(db.Activities, "ActivityId", "ActivityName", activitytransaction.ActivityId);
            return View(activitytransaction);
        }

        //
        // GET: /ActivityTransaction/Delete/5

        public ActionResult Delete(int id = 0)
        {
            ActivityTransaction activitytransaction = db.ActivityTransactions.Find(id);
            if (activitytransaction == null)
            {
                return HttpNotFound();
            }
            return View(activitytransaction);
        }

        //
        // POST: /ActivityTransaction/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ActivityTransaction activitytransaction = db.ActivityTransactions.Find(id);
            db.ActivityTransactions.Remove(activitytransaction);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}