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
        // GET: /Checkout/Payment
        public ActionResult Payment()
        {
            return View();
        }
        //
        // POST: /Checkout/Payment
        [HttpPost]
        public ActionResult Payment(FormCollection values)
        {
            var order = new Order();
            TryUpdateModel(order);
            const string PromoCode = "hello";
 
            try
            {
                if (string.Equals(values["PromoCode"], PromoCode,
                    StringComparison.OrdinalIgnoreCase) == false)
                {
                    return View("Error");
                }
                else
                {
                    order.UserName = User.Identity.Name;
                    order.OrderDate = DateTime.Now;
 
                    //Save Order
                    db.Orders.Add(order);
                    db.SaveChanges();
                    //Process the order
                    var cart = ShoppingCart.GetCart(this.HttpContext);
                    cart.CreateOrder(order);
 
                    return RedirectToAction("Complete",
                        new { id = order.OrderId });
                }
            }
            catch
            {
                //Invalid - redisplay with errors
                return View("Error");
            }
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

