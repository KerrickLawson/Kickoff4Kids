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
    
    public class OrderDetailController : Controller
    {
        private Kickoff4KidsDb db = new Kickoff4KidsDb();

        //
        // GET: /OrderDetail/
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var orderdetails = db.OrderDetails.Include(o => o.Product).Include(o => o.Order);
            return View(orderdetails.ToList());
        }

        //
        // GET: /OrderDetail/Details/5

        public ActionResult Details(int id = 0)
        {
            //var orderDetails = new List<OrderDetail>();
            //foreach (var orderDetail in orderDetails.Where(orderDetail => orderDetail.OrderId == id))
            //{
            //    orderDetails.Add(orderDetail);
            //}
            //OrderDetail orderdetail = db.OrderDetails.Find(id);
            var orderdetails = db.OrderDetails.Where(orderDetail => orderDetail.OrderId == id).Include(o => o.Product).Include(o => o.Order);
            foreach (var order in orderdetails)
            {
                if (order.OrderId != id){}
            }
            if (orderdetails == null)
            {
                return HttpNotFound();
            }
            return View(orderdetails.ToList());
        }


        //
        // POST: /OrderDetail/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(OrderDetail orderdetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orderdetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "ProductName", orderdetail.ProductId);
            ViewBag.OrderId = new SelectList(db.Orders, "OrderId", "UserName", orderdetail.OrderId);
            return View(orderdetail);
        }

        //
        // GET: /OrderDetail/Delete/5

        public ActionResult Delete(int id = 0)
        {
            OrderDetail orderdetail = db.OrderDetails.Find(id);
            if (orderdetail == null)
            {
                return HttpNotFound();
            }
            return View(orderdetail);
        }

        //
        // POST: /OrderDetail/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OrderDetail orderdetail = db.OrderDetails.Find(id);
            db.OrderDetails.Remove(orderdetail);
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