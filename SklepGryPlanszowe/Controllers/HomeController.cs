using SklepGryPlanszowe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SklepGryPlanszowe.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string name)
        {
            ProductsList products = new ProductsList();
            using (Entities db = new Entities())
            {
                if (name == null)
                    products.Products = db.Products.ToList();
                else
                {
                    var searchResult = from m in db.Products
                                       where m.Name.Contains(name)
                                       select m;

                    Products product = new Products();
                    foreach (var s in searchResult)
                    {
                        product.Id = s.Id;
                        product.Description = s.Description;
                        product.ImageName = s.ImageName;
                        product.Name = s.Name;
                        product.Price = s.Price;

                        products.Products.Add(product);
                    }
                }
            }
            return View(products);
        }

        public ActionResult Koszyk()
        {
            ViewBag.Cart = ShoppingCart.Instance.Items;

            return View();
        }

        public ActionResult Kontakt()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Profil()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Search(string name)
        {
            ProductsList products = new ProductsList();
            using (Entities db = new Entities())
            {
                var searchResult = from m in db.Products
                                    where m.Name.Contains(name)
                                    select m;

                Products product = new Products();
                foreach (var s in searchResult)
                {
                    product.Id = s.Id;
                    product.Description = s.Description;
                    product.ImageName = s.ImageName;
                    product.Name = s.Name;
                    product.Price = s.Price;

                    products.Products.Add(product);
                }
            }
            return View("Index", products);
        }

    }
}