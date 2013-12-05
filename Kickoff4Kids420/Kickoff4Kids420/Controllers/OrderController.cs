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
    
    public class OrderController : Controller
    {
        private Kickoff4KidsDb db = new Kickoff4KidsDb();

        //
        // GET: /Order/
        [Authorize(Roles = "Admin")]
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.FNameSortParm = String.IsNullOrEmpty(sortOrder) ? "First_Name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "OrderDate" ? "OrderDate_desc" : "OrderDate";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;

            }

            ViewBag.CurrentFilter = searchString;

            var orders = from o in db.Orders.Include(o => o.UserProfiles)
                         select o;

            if (!String.IsNullOrEmpty(searchString))
            {
                orders = orders.Where(p => p.FirstName.ToUpper().Contains(searchString.ToUpper()));

            }
            switch (sortOrder)
            {
                case "First_Name_desc":
                    orders = orders.OrderByDescending(o => o.FirstName);
                    break;
                case "OrderDate":
                    orders = orders.OrderBy(o => o.OrderDate);
                    break;
                case "OrderDate_desc":
                    orders = orders.OrderByDescending(o => o.OrderDate);
                    break;
                default:
                    orders = orders.OrderBy(o => o.FirstName);
                    break;
            }
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(orders.ToPagedList(pageNumber, pageSize));


            //var orders = db.Orders.Include(o => o.UserProfiles);
            //return View(orders.ToList());
        }
        [Authorize(Roles = "Admin")]
        public ActionResult FulfilledOrders(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.FNameSortParm = String.IsNullOrEmpty(sortOrder) ? "First_Name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "OrderDate" ? "OrderDate_desc" : "OrderDate";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;

            }

            ViewBag.CurrentFilter = searchString;

            var orders = from o in db.Orders.Include(o => o.UserProfiles)
                         select o;

            if (!String.IsNullOrEmpty(searchString))
            {
                orders = orders.Where(p => p.FirstName.ToUpper().Contains(searchString.ToUpper()));

            }
            switch (sortOrder)
            {
                case "First_Name_desc":
                    orders = orders.OrderByDescending(o => o.FirstName);
                    break;
                case "OrderDate":
                    orders = orders.OrderBy(o => o.OrderDate);
                    break;
                case "OrderDate_desc":
                    orders = orders.OrderByDescending(o => o.OrderDate);
                    break;
                default:
                    orders = orders.OrderBy(o => o.FirstName);
                    break;
            }
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(orders.ToPagedList(pageNumber, pageSize));
            //var orders = db.Orders.Include(o => o.UserProfiles);
            //return View(orders.ToList());
        }
        [Authorize(Roles = "Admin")]
        public ActionResult UnFulfilledOrders(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.FNameSortParm = String.IsNullOrEmpty(sortOrder) ? "First_Name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "OrderDate" ? "OrderDate_desc" : "OrderDate";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;

            }

            ViewBag.CurrentFilter = searchString;

            var orders = from o in db.Orders.Include(o => o.UserProfiles)
                         select o;

            if (!String.IsNullOrEmpty(searchString))
            {
                orders = orders.Where(p => p.FirstName.ToUpper().Contains(searchString.ToUpper()));

            }
            switch (sortOrder)
            {
                case "First_Name_desc":
                    orders = orders.OrderByDescending(o => o.FirstName);
                    break;
                case "OrderDate":
                    orders = orders.OrderBy(o => o.OrderDate);
                    break;
                case "OrderDate_desc":
                    orders = orders.OrderByDescending(o => o.OrderDate);
                    break;
                default:
                    orders = orders.OrderBy(o => o.FirstName);
                    break;
            }
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(orders.ToPagedList(pageNumber, pageSize));
            //var orders = db.Orders.Include(o => o.UserProfiles);
            //return View(orders.ToList());
        }

        //
        // GET: /Order/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int id = 0)
        {
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        //


        //
        // POST: /Order/Create



        //
        // GET: /Order/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id = 0)
        {
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.UserProfiles, "UserId", "UserName", order.UserId);
            return View(order);
        }

        //
        // POST: /Order/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.UserProfiles, "UserId", "UserName", order.UserId);
            return View(order);
        }

        //
        // GET: /Order/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id = 0)
        {
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        //
        // POST: /Order/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult OrderFulfilled(int orderId, bool? ischecked)
        {
            Order order = db.Orders.Find(orderId);
            if (ischecked.HasValue && ischecked.Value)
            {
                order.IsFulfilled = true;
            }
            else
            {
                order.IsFulfilled = false;
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Student")]
        public ActionResult StudentOrdersResult()
        {
            var orders = from o in db.Orders.Include(o => o.UserProfiles)
                         select o;
            return View(orders);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}