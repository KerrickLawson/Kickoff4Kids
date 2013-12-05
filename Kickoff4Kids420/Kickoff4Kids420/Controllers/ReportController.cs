using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kickoff4Kids420.Models;
using Kickoff4Kids420.ViewModels;
using WebMatrix.WebData;
using System.Web.Security;

namespace Kickoff4Kids420.Controllers
{
    [RequireHttps]
    [Authorize]
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
        //public ActionResult SchoolPointReport(string sortOrder, string currentFilter, string searchString, int? page)
        //{
        //    ViewBag.CurrentSort = sortOrder;
        //    ViewBag.FNameSortParm = String.IsNullOrEmpty(sortOrder) ? "First_Name_desc" : "";
        //    ViewBag.DateSortParm = sortOrder == "OrderDate" ? "OrderDate_desc" : "OrderDate";

        //    if (searchString != null)
        //    {
        //        page = 1;
        //    }
        //    else
        //    {
        //        searchString = currentFilter;

        //    }

        //    ViewBag.CurrentFilter = searchString;

        //    var orders = from o in db.Orders.Include(o => o.UserProfiles)
        //                 select o;

        //    if (!String.IsNullOrEmpty(searchString))
        //    {
        //        orders = orders.Where(p => p.FirstName.ToUpper().Contains(searchString.ToUpper()));

        //    }
        //    switch (sortOrder)
        //    {
        //        case "First_Name_desc":
        //            orders = orders.OrderByDescending(o => o.FirstName);
        //            break;
        //        case "OrderDate":
        //            orders = orders.OrderBy(o => o.OrderDate);
        //            break;
        //        case "OrderDate_desc":
        //            orders = orders.OrderByDescending(o => o.OrderDate);
        //            break;
        //        default:
        //            orders = orders.OrderBy(o => o.FirstName);
        //            break;
        //    }
        //    int pageSize = 3;
        //    int pageNumber = (page ?? 1);
        //    return View(orders.ToPagedList(pageNumber, pageSize));


        //    //var orders = db.Orders.Include(o => o.UserProfiles);
        //    //return View(orders.ToList());
        //}
        //Total current spent & unspent points for all students arranged by state, then school
        public ActionResult SchoolPointReport()
        {


            var model = from s in db.Schools
                        join u in db.UserProfiles on s.SchoolId equals u.SchoolId into su
                        orderby s.State, s.SchoolName
                        select new ReportViewModel
                        {
                            SchoolName = s.SchoolName,
                            State = s.State,
                            Total_Unspent_Student_Points = su.Sum(t => t.PointTotal) ?? 0,
                            Total_Accumulated_Student_Points = su.Sum(t => t.CumulativePointTotal) ?? 0
                        };

            return View(model);

        }

        public ActionResult Top10Students()
        {

            var model = (from u in db.UserProfiles
                         join s in db.Schools on u.SchoolId equals s.SchoolId
                         where u.CumulativePointTotal > 0
                         select new ReportViewModel
                         {
                             SchoolName = u.Schools.SchoolName,
                             StudentPoints = u.CumulativePointTotal,
                             StudentFirstName = u.FirstName,
                             StudentLastName = u.LastName,
                             UserName = u.UserName
                         }).OrderByDescending(x=>x.StudentPoints).Take(10);

            return View(model);
        }

        public ActionResult Top10Products()
        {

            var model = (from p in db.Products
                         join o in db.OrderDetails on p.ProductId equals o.ProductId into po
                         orderby p.Categories.CategoryName, p.ProductName, p.Price
                         select new ProductReportViewModel
                         {

                             CategoryName = p.Categories.CategoryName,
                             ProductName = p.ProductName,
                             Price = p.Price,
                             ProductOrderedCount = po.Count(c => c.ProductId != null)
                         }).OrderBy(x=>x.CategoryName).OrderBy(x=>x.ProductName).Take(10);

            return View(model);
        }

        //Top 10 students
        public ActionResult CurrentStudentSchoolTotal()
        {
            var model =
                from s in db.Schools
                join u in db.UserProfiles on s.SchoolId equals u.SchoolId into su
                select new ReportViewModel
                {
                    SchoolName = s.SchoolName,
                    State = s.State,
                    TotalPointsPerSchool = su.Sum(t => t.PointTotal) ?? 0
                };
            return View(model);
        }
        
        public ActionResult ParticipatingStudentCountPerSchool()
        {
            //Guid userGuid = (Guid)Membership.GetUser().ProviderUserKey;

            //var userID = Convert.ToInt32(userGuid);

           //var userSchool = (from s in db.Schools
           //              join u in db.UserProfiles on s.SchoolId equals u.SchoolId
           //              where u.UserId == userID
           //              select u.Schools.SchoolName).FirstOrDefault();

            var model = (from s in db.Schools
                        join u in db.UserProfiles on s.SchoolId equals u.SchoolId into su
                        //where s.SchoolName == userSchool
                        select new ReportViewModel
                        {
                            SchoolName = s.SchoolName,
                            State = s.State,
                            Total_Participating_Students = (from a in db.UserProfiles
                                                            where a.PointTotal >0 && a.Schools.SchoolName == s.SchoolName && a.Schools.State ==s.State
                                                            select new 
                                                            { 
                                                                Student_Count = a.UserId
                                                            }).Count()

                        }).OrderBy(x => x.State).OrderBy(x => x.SchoolName).OrderByDescending(x => x.Total_Participating_Students);
            return View(model);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
