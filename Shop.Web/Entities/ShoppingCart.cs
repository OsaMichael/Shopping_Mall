﻿using Shop.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shop.Web.Entities
{
    public partial class ShoppingCart
    {
        ApplicationDbContext db = new ApplicationDbContext();

        string ShoppingCartId { get; set; }

        public const string CartSessionKey = "CartId";

        public static ShoppingCart GetCart(HttpContextBase context)
        {
            var cart = new ShoppingCart();
            cart.ShoppingCartId = cart.GetCartId(context);
            return cart;
        }

        // Helper method to simplify shopping cart calls 
        public static ShoppingCart GetCart(Controller controller)
        {
            return GetCart(controller.HttpContext);
        }

        public void AddToCart(BrandModel model)
        {
            // Get the matching cart and album instances
            var cartItem = db.Carts.SingleOrDefault(c => 
                    c.CartId == ShoppingCartId && 
                    c.BrandId == model.BrandId);

            if(cartItem == null)
            {
                // Create a new cart item if no cart item exists 
                cartItem = new Cart
                {

                      BrandId = model.BrandId,
                      CartId = ShoppingCartId,
                      Count = 1,
                      DateCreated = DateTime.Now
                };
                db.Carts.Add(cartItem);
            }
            else
            {
                // If the item does exist in the cart, then add one to the quantity          
                cartItem.Count++;
            }
            db.SaveChanges();
        }

        public int RemoveFromCart(int id)
        {
            // Get the cart    
            var cartItem = db.Carts.Single(cart => cart.CartId == ShoppingCartId && cart.RecordId == id);
            int itemCount = 0;

            if (cartItem != null)
            {
                if (cartItem.Count > 1)
                {
                    cartItem.Count--;
                    itemCount = cartItem.Count;
                }
                else
                {
                    db.Carts.Remove(cartItem);
                }
                // Save changes 
                db.SaveChanges();
            }

            return itemCount;
        }
        public void EmptyCart ()
        {
            var cartItems = db.Carts.Where(c => c.CartId == ShoppingCartId);

            foreach(var cartItem in cartItems)
            {
                db.Carts.Remove(cartItem);
            }

            db.SaveChanges();
        }

        public List<Cart> GetCartItems()
        {
            return db.Carts.Where(cart => cart.CartId == ShoppingCartId).ToList();
        }
        public int GetCount()
        {
            // Get the count of each item in the cart and sum them up

            int? count = (from cartItems in db.Carts
                          where cartItems.CartId == ShoppingCartId
                          select (int?)cartItems.Count).Sum();

            // Return 0 if all entries are null 
            return count ?? 0;
        }
        public decimal GetTotal()
        {
            // Multiply brand price by count of that brand to get
            // the current price for each of those brands in the cart  
            // sum all brand price totals to get the cart total 

            decimal? total = (from cartItems in db.Carts
                              where cartItems.CartId == ShoppingCartId
                              select (int?)cartItems.Count * cartItems.Brand.Price).Sum();
            return total ?? decimal.Zero;
        }

        public int CreateOrder(Order order)
        {
            decimal orderTotal = 0;

            var cartItems = GetCartItems();

            // Iterate over the items in the cart, adding the order details for each 
            foreach(var item in cartItems)
            {
                var orderDetal = new OrderDetail
                {
                   BrandId = item.BrandId,
                   OrderId = order.OrderId,
                   UnitPrice = item.Brand.Price,
                   Quantity = item.Count
                   
                };

                // Set the order total of the shopping cart  
                // orderTotal = orderTotal + (item.Count * item.Brand.Price);
                // orderTotal += (item.Count * item.Brand.Price);

                orderTotal = orderTotal + (item.Count * item.Brand.Price);
                db.OrderDetails.Add(orderDetal);

            }
            // Set the order's total to the orderTotal count  
            order.Total = orderTotal;
            // Save the order 
            db.SaveChanges();
            //Empty the shopping cart
            EmptyCart();

            //returning the OrderId as the confirmation number
            return order.OrderId;
        }

        // We're using HttpContextBase to allow access to cookies.     

        public string GetCartId (HttpContextBase context)
        {
            if(context.Session[CartSessionKey] == null)
            {
                if (!string.IsNullOrWhiteSpace(context.User.Identity.Name))
                {
                    context.Session[CartSessionKey] = context.User.Identity.Name;

                }
                else
                {
                    // Generate a new random GUID using System.Guid class 
                    Guid tempCartId = Guid.NewGuid();
                    // Send tempCartId back to client as a cookie

                    context.Session[CartSessionKey] = tempCartId.ToString();

                }            
            }
            return context.Session[CartSessionKey].ToString();
        }

        //when a user has logged in, migrate their shopping cart to
        //be associate with their username
        public void MigrateCart(string userName)
        {
            var shoppingCart = db.Carts.Where(c => c.CartId == ShoppingCartId);

            foreach(Cart item in shoppingCart)
            {
                item.CartId = userName;
            }
            db.SaveChanges();
        }
    }
}