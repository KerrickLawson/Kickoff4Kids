using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kickoff4Kids420.Models;

namespace Kickoff4Kids420.Controllers
{
    public class HomeController : Controller
    {
        Kickoff4KidsDb db = new Kickoff4KidsDb();

        public ActionResult Index()
        {
            var fp = new FrontPage();
            fp = db.FrontPage.Find(1);           
            return View(fp);
            
            
        }
        public ActionResult About()  
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Gallery()
        {

            return View();
        }

        public ActionResult Help()
        {
            return View();
        }

        public ActionResult AdminPanel()
        {
            return View();
        }
        public ActionResult Terms()
        {
            return View();
        }
        public ActionResult Privacy()
        {
            return View();
        }
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }



        
    }
}
