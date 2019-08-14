using Shop.Web.Atrributes;
using Shop.Web.Entities;
using Shopp.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Shop.Web.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Brand> Brands { get; set; }
        public IEnumerable<Company> Companies { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public Brand GetBrand { get; set; }
        public int Nextevent { get; set; }
        //  public IEnumerable<IDepartment> AllDepartment { get; set; } = Enumerable.Empty<IDepartment>();

        public string RecentSermon { get; set; }
        [Display(Name = "Image")]
        public string ImageUrl { get; set; }

        [FileTypes("jpg,jpeg,png")]
        public HttpPostedFileBase ImageFile { get; set; }
    }
}