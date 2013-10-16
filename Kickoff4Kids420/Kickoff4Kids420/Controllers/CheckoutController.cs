using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kickoff4Kids420.Models;

namespace Kickoff4Kids420.Controllers
{
    [Authorize]
    public class CheckoutController : Controller
    {
        Kickoff4KidsDb db = new Kickoff4KidsDb();
        
        //
        // GET: /Checkout/Confirmation
        public ActionResult Confirmation(int id)
        {
            return View();
        }
        //
        // POST: /Checkout/Confirmation
        [HttpPost]
        public ActionResult Confirmation()
        {
            //var order = new Order();
            //TryUpdateModel(order);
            
            //        order.UserName = User.Identity.Name;
            //        order.OrderDate = DateTime.Now;
 
            //        //Save Order
            //        db.Orders.Add(order);
            //        db.SaveChanges();
            //        //Process the order
            //        var cart = ShoppingCart.GetCart(this.HttpContext);
            //        cart.CreateOrder(order);

            return View("Complete");
             
        }
        //
        // GET: /Checkout/Complete
        public ActionResult Complete(int id)
        {
            // Validate customer owns this order
            bool isValid = db.Orders.Any(
                o => o.OrderId == id &&
                o.UserName == User.Identity.Name);
 
            if (isValid)
            {
                return View(id);
            }
            else
            {
                return View("Error");
            }
        }
    }
}

