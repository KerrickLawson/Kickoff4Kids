using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Kickoff4Kids420.Models;
using PagedList;
using PagedList.Mvc;

namespace Kickoff4Kids420.Controllers
{
    [RequireHttps]
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private Kickoff4KidsDb db = new Kickoff4KidsDb();

        //
        // GET: /User/

        public ActionResult Index(string studentUser, string sortOrder, string currentFilter, string searchString, int? page)
        {

            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "Student_Name_desc" : "";
            ViewBag.PointSortParm = sortOrder == "Point" ? "Point_desc" : "Point";

            // for drop down filter
            var StudentList = new List<string>();
            var StudentListQry = from s in db.Schools
                                 orderby s.SchoolName
                                 select s.SchoolName;

            StudentList.AddRange(StudentListQry.Distinct());
            ViewBag.studentUser = new SelectList(StudentList);
            //
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;

            }

            ViewBag.CurrentFilter = searchString;

            var students = from s in db.UserProfiles.Include(s => s.Schools)
                           select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                students = students.Where(s => s.UserName.ToUpper().Contains(searchString.ToUpper()));

            }
            // for dropdown filter
            if (!String.IsNullOrEmpty(studentUser))
            {
                students = students.Where(x => x.Schools.SchoolName == studentUser);
            }
            switch (sortOrder)
            {
                case "Student_Name_desc":
                    students = students.OrderByDescending(s => s.UserName);
                    break;
                case "Point":
                    students = students.OrderBy(s => s.PointTotal);
                    break;
                case "Point_desc":
                    students = students.OrderByDescending(s => s.PointTotal);
                    break;
                default:
                    students = students.OrderBy(s => s.UserName);
                    break;
            }

            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(students.ToPagedList(pageNumber, pageSize));

        }

        //
        // GET: /User/Details/5

        public ActionResult Details(int id = 0)
        {
            UserProfile userprofile = db.UserProfiles.Find(id);
            if (userprofile == null)
            {
                return HttpNotFound();
            }
            return View(userprofile);
        }

        //
        // GET: /User/Create

        public ActionResult Create()
        {
            ViewBag.SchoolId = new SelectList(db.Schools, "SchoolId", "SchoolName");
            return View();
        }

        //
        // POST: /User/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserProfile userprofile)
        {
            if (ModelState.IsValid)
            {
                db.UserProfiles.Add(userprofile);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SchoolId = new SelectList(db.Schools, "SchoolId", "SchoolName", userprofile.SchoolId);
            return View(userprofile);
        }

        //
        // GET: /User/Edit/5

        public ActionResult Edit(int id = 0)
        {
            UserProfile userprofile = db.UserProfiles.Find(id);
            if (userprofile == null)
            {
                return HttpNotFound();
            }
            ViewBag.SchoolId = new SelectList(db.Schools, "SchoolId", "SchoolName", userprofile.SchoolId);
            return View(userprofile);
        }

        //
        // POST: /User/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserProfile userprofile)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userprofile).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SchoolId = new SelectList(db.Schools, "SchoolId", "SchoolName", userprofile.SchoolId);
            return View(userprofile);
        }

        //
        // GET: /User/Delete/5

        public ActionResult Delete(int id = 0)
        {
            UserProfile userprofile = db.UserProfiles.Find(id);
            if (userprofile == null)
            {
                return HttpNotFound();
            }
            return View(userprofile);
        }

        //
        // POST: /User/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserProfile userprofile = db.UserProfiles.Find(id);
            string[] rolesArray = Roles.GetRolesForUser(userprofile.UserName);
            Roles.RemoveUserFromRoles(userprofile.UserName, rolesArray);
            db.UserProfiles.Remove(userprofile);
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