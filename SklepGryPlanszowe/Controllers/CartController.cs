using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SklepGryPlanszowe.Models;

namespace SklepGryPlanszowe.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        public ActionResult AddToCart(int productId, string returnUrl)
        {
            ShoppingCart.Instance.AddItem(productId);

            return RedirectToAction("Koszyk", "Home");
        }
        public ActionResult RemoveItem(int productId)
        {
            ShoppingCart.Instance.RemoveItem(productId);

            return RedirectToAction("Koszyk", "Home");
        }

        public ActionResult PlaceOrder()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View("OrderPlaced");
            }
            else
                return View("UserNotAuthorized");
        }

    }
}