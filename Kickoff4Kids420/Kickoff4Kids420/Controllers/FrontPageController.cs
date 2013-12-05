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
    [RequireHttps]
    [Authorize(Roles = "Admin")]
    public class FrontPageController : Controller
    {
        private Kickoff4KidsDb db = new Kickoff4KidsDb();

        //
        // GET: /FrontPage/

        public ActionResult Index()
        {
            FrontPage fp = new FrontPage();
            fp = db.FrontPage.Find(1);
            return View(fp);
        }

        //
        // GET: /FrontPage/Details/5

        //
        // GET: /FrontPage/Edit/5

        public ActionResult Edit(int id = 0)
        {
            FrontPage frontpage = db.FrontPage.Find(id);
            if (frontpage == null)
            {
                return HttpNotFound();
            }
            return View(frontpage);
        }

        //
        // POST: /FrontPage/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FrontPage frontpage)
        {
            if (ModelState.IsValid)
            {
                db.Entry(frontpage).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(frontpage);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}