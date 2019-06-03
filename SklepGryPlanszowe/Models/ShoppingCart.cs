using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SklepGryPlanszowe.Models
{
    public class ShoppingCart
    {
        public List<CartItem> Items { get; private set; }

        //Singleton
        public static readonly ShoppingCart Instance;

        static ShoppingCart()
        {
            if (HttpContext.Current.Session["ASPNETShoppingCart"] == null)
            {
                Instance = new ShoppingCart();
                Instance.Items = new List<CartItem>();
                HttpContext.Current.Session["ASPNETShoppingCart"] = Instance;
            }
            else {
                Instance = (ShoppingCart)HttpContext.Current.Session["ASPNETShoppingCart"];
            }
        }

        protected ShoppingCart() { }

        public void AddItem(int productId)
        {
            using (Entities db = new Entities())
            {
                CartItem newItem = new CartItem();
                newItem.Product = db.Products.Find(productId);


                foreach (CartItem item in Items)
                {
                    if (item.Product.Id == newItem.Product.Id)
                    {
                        item.Quantity++;
                        return;
                    }
                }

                newItem.Quantity = 1;
                Items.Add(newItem);
            }
        }
        public void IncreaseItemQuantity (int productId)
        {
            using (Entities db = new Entities())
            {
                CartItem newItem = new CartItem();
                newItem.Product = db.Products.Find(productId);

                if (Items.Contains(newItem))
                {
                    foreach (CartItem item in Items)
                    {
                        if (item.Equals(newItem))
                        {
                            item.Quantity++;
                            return;
                        }
                    }
                }
            }
        }
        public void DecreaseItemQuantity(int productId)
        {
            using (Entities db = new Entities())
            {
                CartItem newItem = new CartItem();
                newItem.Product = db.Products.Find(productId);

                if (Items.Contains(newItem))
                {
                    foreach (CartItem item in Items)
                    {
                        if (item.Equals(newItem))
                        {
                            item.Quantity--;
                            if (item.Quantity < 1)
                                RemoveItem(productId);
                            return;
                        }
                    }
                }
            }
        }
        public void RemoveItem(int productId)
        {
            Items.Remove(Items.Find(X => X.Product.Id == productId));
        }
            public decimal GetSubTotal()
        {
            decimal subTotal = 0;
            foreach (CartItem item in Items)
                subTotal += item.Product.Price;

            return subTotal;
        }
    }

}
