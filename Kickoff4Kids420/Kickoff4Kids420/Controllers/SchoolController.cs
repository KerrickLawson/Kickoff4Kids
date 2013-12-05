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
    [RequireHttps]
    [Authorize(Roles = "Admin")]
    public class SchoolController : Controller
    {
        private Kickoff4KidsDb db = new Kickoff4KidsDb();

        //
        // GET: /School/

        public ActionResult Index(string currentSchool, string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "School_Name_desc" : "";
            ViewBag.CodeSortParm = sortOrder == "School_Address" ? "School_Address_desc" : "School_Address";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;

            }

            ViewBag.CurrentFilter = searchString;

            var schools = from s in db.Schools
                          select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                schools = schools.Where(s => s.SchoolName.ToUpper().Contains(searchString.ToUpper()));

            }
            switch (sortOrder)
            {
                case "School_Name_desc":
                    schools = schools.OrderByDescending(s => s.SchoolName);
                    break;
                case "School_Address":
                    schools = schools.OrderBy(s => s.SchoolAddress);
                    break;
                case "School_Address_desc":
                    schools = schools.OrderByDescending(s => s.SchoolAddress);
                    break;
                default:
                    schools = schools.OrderBy(s => s.SchoolName);
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(schools.ToPagedList(pageNumber, pageSize));


        }

        //
        // GET: /Default1/Details/5

        public ActionResult Details(int id = 0)
        {
            School school = db.Schools.Find(id);
            if (school == null)
            {
                return HttpNotFound();
            }
            return View(school);
        }

        //
        // GET: /Default1/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Default1/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(School school)
        {
            if (ModelState.IsValid)
            {
                db.Schools.Add(school);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(school);
        }

        //
        // GET: /Default1/Edit/5

        public ActionResult Edit(int id = 0)
        {
            School school = db.Schools.Find(id);
            if (school == null)
            {
                return HttpNotFound();
            }
            return View(school);
        }

        //
        // POST: /Default1/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(School school)
        {
            if (ModelState.IsValid)
            {
                db.Entry(school).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(school);
        }

        //
        // GET: /Default1/Delete/5

        public ActionResult Delete(int id = 0)
        {
            School school = db.Schools.Find(id);
            if (school == null)
            {
                return HttpNotFound();
            }
            return View(school);
        }

        //
        // POST: /Default1/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            School school = db.Schools.Find(id);
            db.Schools.Remove(school);
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