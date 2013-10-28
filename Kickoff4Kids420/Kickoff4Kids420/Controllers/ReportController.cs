using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kickoff4Kids420.Models;

namespace Kickoff4Kids420.Controllers
{
    public class ReportController : Controller
    {
        //Mark, I already made the views in the Views>Report Folder. Feel free to test things out.
        // GET: /Report/
        //Stored the Database in "db" for you to use. Took care of the Dispose at the end. 
        //You are set for LINQ queries.
        private Kickoff4KidsDb db = new Kickoff4KidsDb();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult TestReport()
        {
            var model = db.Schools
                .OrderBy(x => x.SchoolName)
                .Take(10);

         return View(model);
        }
        //public ActionResult testreport2()
        //{
        //    var model = db.
        //}

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
