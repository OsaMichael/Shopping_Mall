using Shop.Web.Entities;
using Shop.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shop.Web.Controllers
{
    [Authorize]
    public class CheckoutController : Controller
    {
       ApplicationDbContext db = new ApplicationDbContext();

        const string PromoCode = "FREE";
        // GET: Checkout
        public ActionResult AddressAndPayment()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddressAndPayment( FormCollection values)
        {
            var order = new Order();

            TryUpdateModel(order);
            try
            {
                if(string.Equals(values["PromoCode"], PromoCode,
                    StringComparison.OrdinalIgnoreCase) == false)
                {
                    return View(order);
                }
                else
                {
                    order.Email = User.Identity.Name;
                    order.OrderDate = DateTime.Now;

                    //save order

                    db.Orders.Add(order);
                    db.SaveChanges();

                    //process the order
                    var cart = ShoppingCart.GetCart(this.HttpContext);
                    cart.CreateOrder(order);

                    return RedirectToAction("Complete", new { id = order.OrderId});
                }
            }
            catch
            {
                //Invalid - redisplay with errors 
                return View(order);
            }
     
        }
        
        public ActionResult Complete(int id)
        {
            // validate customer owns this order
            bool isValid = db.Orders.Any(o => o.OrderId == id && o.Email == User.Identity.Name);
            if(isValid)
            {
                return View(id);
            }
            else
            {
                return View("error");
            }
        }
    }
}