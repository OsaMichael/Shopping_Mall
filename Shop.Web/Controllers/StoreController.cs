using Shop.Web.Models;
using Shop.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shop.Web.Controllers
{
    public class StoreController : Controller
    {
        ApplicationDbContext storedDb = new ApplicationDbContext();
        // GET: Store
        public ActionResult Index()
        {
            var listGenre = storedDb.Categories.ToList();

            var resultt = new CategoryViewModel
            {
                Categories = listGenre
            };

            return View(resultt);
        }

        public ActionResult Browse(string category)
        {
           // var brands = storedDb.Brands.ToList();

            var brand = storedDb.Categories.Include("Brands").SingleOrDefault(c => c.Name == "Electronics");

            var bdd = new CategoryViewModel
            {
               Categoree = brand
            };

            return View(bdd);
        }

        public ActionResult Details(int id)
        {
            var brand = storedDb.Brands.Find(id);

            var result = new BrandModel
            {
                BrandId = brand.BrandId,
                BrandName = brand.BrandName,
                Price = brand.Price,
                Categorys = brand.Categorys,
                CategoryId = brand.CategoryId,
                Companys = brand.Companys,
                CompanId = brand.CompanId
            };
            return View(result);
        }
    }
}