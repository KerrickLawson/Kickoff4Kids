using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kickoff4Kids420.Models;

namespace Kickoff4Kids420.Controllers
{
    [RequireHttps]
    [Authorize]
    
    public class HomeController : Controller
    {
        Kickoff4KidsDb db = new Kickoff4KidsDb();

        [AllowAnonymous]
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

        [AllowAnonymous]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [AllowAnonymous]
        public ActionResult Gallery()
        {

            return View();
        }

        [AllowAnonymous]
        public ActionResult Help()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
        public ActionResult AdminPanel()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult Terms()
        {
            return View();
        }
        [AllowAnonymous]
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
