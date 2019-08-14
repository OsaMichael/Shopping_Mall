using Shop.Web.Entities;
using Shop.Web.Models;
using Shop.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shop.Web.Controllers
{
    public class ShoppingCartController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: ShoppingCart
        public ActionResult Index()
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);

            //set up our ViewModel 
            var viewModel = new ShoppingCartViewModel
            {
                CartItems = cart.GetCartItems(),
                 CartTotal = cart.GetTotal()

            };
            return View(viewModel);
        }
        //Get: /Store/AddToCart
        public ActionResult AddToCart(int id)
        {
            //retrieve the brand from the database
            var addedbrand = db.Brands.SingleOrDefault(b => b.BrandId == id);

            var result = new BrandModel
            {
                BrandId = addedbrand.BrandId,
                 BrandImageThumbnailUrl = addedbrand.BrandImageThumbnailUrl,
                  BrandName = addedbrand.BrandName,
                   Price = addedbrand.Price
            };
            // Add it to the shopping cart
            var cart = ShoppingCart.GetCart(this.HttpContext);
            cart.AddToCart(result);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult RemoveFromCart(int id)
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);

            string brandName = db.Carts.SingleOrDefault(n => n.RecordId == id).Brand.BrandName;

            // Remove from cart
            int itemCount = cart.RemoveFromCart(id);

            //Dislpay the confirmation message
            var results = new ShoppingCartRemoveViewModel
            {
                Message = Server.HtmlEncode(brandName) + "has been removed from your shopping cart.",
                CartTotal = cart.GetTotal(),
                CartCount = cart.GetCount(),
                ItemCount = itemCount,
                DeleteId = id
            };
            return Json(results);
        }
        [ChildActionOnly]
        public ActionResult CartSummary()
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);
            ViewData["CartCount"] = cart.GetCount();
            return PartialView("CartSummary");
        }
    }
}