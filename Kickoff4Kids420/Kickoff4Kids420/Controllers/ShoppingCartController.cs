using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Kickoff4Kids420.Models;
using Kickoff4Kids420.ViewModels;

namespace Kickoff4Kids420.Controllers
{
    public class ShoppingCartController : Controller
    {
        Kickoff4KidsDb db = new Kickoff4KidsDb();
        //
        // GET: /ShoppingCart/
        public ActionResult Index()
        {
            int userId = Convert.ToInt32(Membership.GetUser().ProviderUserKey.ToString());
            UserProfile user = db.UserProfiles.Find(userId);          
            var cart = ShoppingCart.GetCart(this.HttpContext);
            if (user.PointTotal == null)
            {
                user.PointTotal = 0;
            }
            var viewModel = new ShoppingCartViewModel
            {
                CartItems = cart.GetCartItems(),
                CartTotal = cart.GetTotal(),
                PointTotal = (int)user.PointTotal,
                UserName = user.UserName

            };
            return View(viewModel);
            
        }

        public ActionResult AddToCart(int id)
        {
            var addedProduct = db.Products
                .Single(p => p.ProductId == id);

            var cart = ShoppingCart.GetCart(this.HttpContext);

            cart.AddToCart(addedProduct);
            return RedirectToAction("Index");
        }
        //
        // AJAX: /ShoppingCart/RemoveFromCart/5
        [HttpPost]
        public ActionResult RemoveFromCart(int id)
        {
            // Remove the item from the cart
            var cart = ShoppingCart.GetCart(this.HttpContext);

            // Get the name of the product to display confirmation
            string productName = db.Carts
                .Single(item => item.RecordId == id).Product.ProductName;

            // Remove from cart
            int itemCount = cart.RemoveFromCart(id);

            // Display the confirmation message
            var results = new ShoppingCartRemoveViewModel
            {
                Message = Server.HtmlEncode(productName) +
                    " has been removed from your shopping cart.",
                CartTotal = cart.GetTotal(),
                CartCount = cart.GetCount(),
                ItemCount = itemCount,
                DeleteId = id
            };
            return Json(results);
        }
        //
        // GET: /ShoppingCart/CartSummary
        [ChildActionOnly]
        public ActionResult CartSummary()
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);

            ViewData["CartCount"] = cart.GetCount();
            return PartialView("CartSummary");
        }

    }
}
