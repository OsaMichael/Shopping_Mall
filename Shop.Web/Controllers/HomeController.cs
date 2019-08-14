using Shop.Web.Models;
using Shop.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shop.Web.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            var brands = db.Brands.ToList();
            var category = db.Categories.ToList();
            var company = db.Companies.ToList();

            var result = new HomeViewModel
            {
                Brands = brands,
                Categories = category,
                Companies = company,
                GetBrand = brands.FirstOrDefault()
                 
            };
            return View(result);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}