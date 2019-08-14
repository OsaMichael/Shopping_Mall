
using Shop.Web.Entities;
using Shop.Web.Models;
using Shop.Web.ViewModels;
using Shopp.Data.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shop.Web.Controllers
{
    //[Authorize(Roles = "Administrator")]
    public class AdminController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
       // GET: Admin
        public ActionResult Index()
        {

            ViewBag.brand = db.Brands.Count();
            ViewBag.category = db.Categories.Count();
            ViewBag.company = db.Companies.Count();
            return View();
            //var comp = db.Companies.ToList();
            //var compList = comp.Select(x => new CompanyModel
            //{
            //    CompanId = x.CompanId,
            //    CompName = x.CompName,

            //}).ToList();/*.GroupBy(v => new { v.CompanId, v.CompName }).Select(s => s.FirstOrDefault());*/

            //var vendList = (from album in db.Brands
            //                join cate in db.Categories on album.CategoryId equals cate.CategoryId
            //                join company in db.Companies on album.CompanId equals company.CompanId
            //                select new BrandViewModel()
            //                {
            //                    BrandName = album.BrandName,
            //                    Price = album.Price,
            //                    CompName = company.CompName,
            //                    Name = cate.Name,
            //                    BrandImageThumbnailUrl = album.BrandImageThumbnailUrl
            //                }).GroupBy(v => new { v.BrandName, v.CompName }).Select(s => s.FirstOrDefault());




            //var result = (from album in db.Brands
            //              join cate in db.Categories on album.CategoryId equals cate.CategoryId
            //              join company in db.Companies on album.CompanId equals company.CompanId

            //              select new BrandViewModel()
            //             {
                          
            //                   BrandName = album.BrandName,
            //                  Price = album.Price,
            //                  CompName = company.CompName,/*string.Join(", ", vendList.Where(a => a.BrandName == album.BrandName).Select(u => u.CompName)),*/
            //                  Name = cate.Name,                        
            //                  BrandImageThumbnailUrl = album.BrandImageThumbnailUrl
            //              });    
                        

            //return View(result);

        }
        public ActionResult CompanyList()
        {
            var result = db.Companies.ToList();

            var cc = new CompanyViewModel
            {
                Companies = result
            };
            return View(cc);
        }
        public ActionResult AddCompany()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddCompany(CompanyModel model)
        {
            if(ModelState.IsValid)
            {
                var company = db.Companies.Where(c => c.CompanId == model.CompanId).FirstOrDefault();
                if (company != null) throw new Exception("Company already exist");

                var newCompany = new Company
                {
                     CompName = model.CompName,
                    // CompanyImageUrl = model.CompanyImageUrl
                };

                db.Companies.Add(newCompany);
                db.SaveChanges();

                return RedirectToAction("CompanyList");
            }
            return View(model);
        }

        public ActionResult CategoryList()
        {
            var ct = db.Categories.ToList();
            var csd = new CategoryViewModel
            {
                Categories = ct
            };
            return View(csd);
        }
        public ActionResult AddCategory()
        {
        
            return View();
        }
        [HttpPost]
        public ActionResult AddCategory(CategoryModel model)
        {
            if (ModelState.IsValid)
            {

                var categ = db.Categories.Where(n => n.Name == model.Name.Trim()).FirstOrDefault();
                if (categ != null) throw new Exception("already exist");

                var newCat = new Category
                {
                    Name = model.Name,
                    Description = model.Description
                };
                db.Categories.Add(newCat);
                db.SaveChanges();
            
            return RedirectToAction("CategoryList");
            }
            else       
            {
                return View(model);
            }
           
        }
        public ActionResult BrandList()
        {
            var bd = db.Brands.ToList();

            var result = new BrandViewModel
            {
                Brands = bd

            };
            return View(result);
        }
        public ViewResult Details(int id)
        {
            var album = db.Brands.Find(id);
            return View(album);
        }
        public ActionResult AddBrand()
        {

            ViewBag.category = new SelectList(db.Categories, "CategoryId", "Name");
            ViewBag.company = new SelectList(db.Companies, "CompanId", "CompName");

            return View(new BrandModel());
        }
        [HttpPost]
        public ActionResult AddBrand(BrandModel model)
        {


            ViewBag.category = new SelectList(db.Categories, "CategoryId", "Name");
            ViewBag.company = new SelectList(db.Companies, "CompanId", "CompName");

            if (ModelState.IsValid)
            {
                var brand = db.Brands.Where(n => n.BrandId == model.BrandId).FirstOrDefault();

                string fileName = Path.GetFileNameWithoutExtension(model.BrandImageFile.FileName);
                string extension = Path.GetExtension(model.BrandImageFile.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssffff") + extension;
                model.ImageUrl = fileName;
                fileName = Path.Combine(Server.MapPath("~/Content/Images/"), fileName);
                model.BrandImageFile.SaveAs(fileName);

                if (brand == null)
                {
                    var newBrand = new Brand
                    {
                        BrandName = model.BrandName,
                        Price = model.Price,
                        BrandUrl = model.BrandUrl,
                        BrandImageThumbnailUrl = model.ImageUrl,
                        ImageUrl = model.ImageUrl,
                        CategoryId = model.CategoryId,
                          CompanId = model.CompanId
                    };

       

                    db.Brands.Add(newBrand);
                    db.SaveChanges();
                }
                // db.Brands.Add(brand);
       

                TempData["message"] = string.Format("{0} has been saved.", model.BrandName);

                return RedirectToAction("BrandList");
            }
            else
            {
                return View(model);
            }

        
            }
        }
    }
