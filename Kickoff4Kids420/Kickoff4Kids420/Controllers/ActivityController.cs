using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kickoff4Kids420.Models;
using PagedList;
using PagedList.Mvc;

namespace Kickoff4Kids420.Controllers
{
    [Authorize]
    public class ActivityController : Controller
    {
        private Kickoff4KidsDb db = new Kickoff4KidsDb();

        //
        // GET: /Activity/

        public ActionResult Index(string currentActivity, string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "Activity_Name_desc" : "";
            ViewBag.PointSortParm = sortOrder == "Points" ? "Points_desc" : "Points";


            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;

            }

            ViewBag.CurrentFilter = searchString;

            var activities = from a in db.Activities
                             select a;

            if (!String.IsNullOrEmpty(searchString))
            {
                activities = activities.Where(a => a.ActivityName.ToUpper().Contains(searchString.ToUpper()));

            }
            switch (sortOrder)
            {
                case "Activity_Name_desc":
                    activities = activities.OrderByDescending(p => p.ActivityName);
                    break;
                case "Points":
                    activities = activities.OrderBy(p => p.PointValue);
                    break;
                case "Points_desc":
                    activities = activities.OrderByDescending(p => p.PointValue);
                    break;
                default:
                    activities = activities.OrderBy(p => p.ActivityName);
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(activities.ToPagedList(pageNumber, pageSize));
        }

        //
        // GET: /Activity/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Activity/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Activity activity)
        {
            if (ModelState.IsValid)
            {
                db.Activities.Add(activity);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(activity);
        }

        //
        // GET: /Activity/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Activity activity = db.Activities.Find(id);
            if (activity == null)
            {
                return HttpNotFound();
            }
            return View(activity);
        }

        //
        // POST: /Activity/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Activity activity)
        {
            if (ModelState.IsValid)
            {
                db.Entry(activity).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(activity);
        }

        //
        // GET: /Activity/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Activity activity = db.Activities.Find(id);
            if (activity == null)
            {
                return HttpNotFound();
            }
            return View(activity);
        }

        //
        // POST: /Activity/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Activity activity = db.Activities.Find(id);
            db.Activities.Remove(activity);
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