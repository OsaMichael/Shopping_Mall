using Shopp.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop.Web.ViewModels
{
    public class BrandViewModel
    {
        public IEnumerable<Brand> Brands { get; set; }

        //the below was added to enable group in admin index

     
        public string BrandName { get; set; }
        public decimal Price { get; set; }
        public string BrandUrl { get; set; }
        public string ImageUrl { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CompName { get; set; }
        public string CompanyImageUrl { get; set; }
        public string BrandImageThumbnailUrl { get; set; }
    }
}