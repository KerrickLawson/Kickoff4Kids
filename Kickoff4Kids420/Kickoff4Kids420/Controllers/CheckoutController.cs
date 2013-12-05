using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using Kickoff4Kids420.Models;
using Kickoff4Kids420.ViewModels;

namespace Kickoff4Kids420.Controllers
{
    [Authorize(Roles = "Student")]
    public class CheckoutController : Controller
    {
        Kickoff4KidsDb db = new Kickoff4KidsDb();
        
        //
        // GET: /Checkout/Confirmation
        public ActionResult Confirmation()
        {
            int userId = Convert.ToInt32(Membership.GetUser().ProviderUserKey.ToString());
            UserProfile user = db.UserProfiles.Find(userId);
            var cart = ShoppingCart.GetCart(this.HttpContext);
            var viewModel = new ConfirmationViewModel
            {
                CartItems = cart.GetCartItems(),
                CartTotal = cart.GetTotal(),
                PointTotal = (int)user.PointTotal,
                UserName = user.UserName,
                Balance = (int)user.PointTotal - cart.GetTotal()
            };
            //if (user.PointTotal < cart.GetTotal())
            //{
                return View(viewModel);
            //}
            //else
            //{
            //    return RedirectToActionPermanent("Index", "ShoppingCart");
            //}


        }
        //
        // POST: /Checkout/Confirmation

        public ActionResult CompletingResult()
        {
            int userId = Convert.ToInt32(Membership.GetUser().ProviderUserKey.ToString());
            UserProfile user = db.UserProfiles.Find(userId);
            var cart = ShoppingCart.GetCart(this.HttpContext);

            var order = new Order();
            TryUpdateModel(order);

            order.UserId = userId;
            order.UserName = User.Identity.Name;
            order.OrderDate = DateTime.Now;
            order.UserProfiles = user;
            order.FirstName = user.FirstName;
            order.LastName = user.LastName;
            order.IsFulfilled = false;
            
            //Save Order
            db.Orders.Add(order);
            user.PointTotal -= cart.GetTotal();
            db.SaveChanges();
            //Process Order
            cart.CreateOrder(order);

            return RedirectToAction("Complete", new {id = order.OrderId});
             
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


