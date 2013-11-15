﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
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
        // GET: /ActivityTransaction/Create

        public ActionResult Create()
        {
            //Mark, get all user names of students.
            var usernames = Roles.GetUsersInRole("Student");
            //Fetch all of these profiles.
            var studentUsers = db.UserProfiles
                 .Where(x => usernames.Contains(x.UserName)).ToList();
            //Pass this subset into the select list, good to go! :) Now we need to LINQ query! 
            ViewBag.UserId = new SelectList(studentUsers, "UserId", "UserName");
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
                //Fetch User Profile
                UserProfile updUserProfile = db.UserProfiles.Find(activitytransaction.UserId);
                //Fetch Activity
                Activity act = db.Activities.Find(activitytransaction.ActivityId);
                //Fetch point value for completed activity
                int points = act.PointValue;
                //Add Points
                updUserProfile.PointTotal += points;
                updUserProfile.CumulativePointTotal += points;

                db.ActivityTransactions.Add(activitytransaction);
                db.SaveChanges();
                
                //AddStudentPoints(updUserProfile, points);
                
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.UserProfiles, "UserId", "UserName", activitytransaction.UserId);
            ViewBag.ActivityId = new SelectList(db.Activities, "ActivityId", "ActivityName", activitytransaction.ActivityId);
            return View(activitytransaction);
        }
        //public void AddStudentPoints(UserProfile user, int points)
        //{
        //    var stud = db.UserProfiles.FirstOrDefault(c => c.UserId == user.UserId);
        //    stud.PointTotal += points;
        //    db.SaveChanges();
        //}
        //public void SubtractStudentPoints(UserProfile user, int points)
        //{
        //    var stud = db.UserProfiles.FirstOrDefault(c => c.UserId == user.UserId);
        //    stud.PointTotal -= points;
        //    db.SaveChanges();
        //}
        //public void AddPoints(int userId, int activityId)
        //{
        //    UserProfile profile = db.UserProfiles.Find(userId);
        //    Activity activity = db.Activities.Find(activityId);
        //    profile.PointTotal += activity.PointValue;
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(profile).State = EntityState.Modified;
        //        db.SaveChanges();
        //    }

        //}
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
            //Fetch User Profile
            UserProfile updUserProfile = db.UserProfiles.Find(activitytransaction.UserId);
            //Fetch Activity
            Activity act = db.Activities.Find(activitytransaction.ActivityId);
            //Fetch Points for that activity
            int points = act.PointValue;
            updUserProfile.PointTotal -= points;
            db.ActivityTransactions.Remove(activitytransaction);
            db.SaveChanges();
            
            return RedirectToAction("Index");
        }
        public ActionResult StudentActiviesResult([Bind(Prefix = "id")]int userId)
        {
            var student = db.UserProfiles.Find(userId);
            if (student != null)
            {
                return View(student);
            }
            return HttpNotFound();
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}