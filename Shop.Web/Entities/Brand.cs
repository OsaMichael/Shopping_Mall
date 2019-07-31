using Shop.Web.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Shopp.Data.Entities
{
    public class Brand
    {
        public int BrandId { get; set; }

        public int CategoryId { get; set; }

        public int CompanId { get; set; }
        public string BrandName { get; set; }

        public decimal Price { get; set; }

        public string BrandUrl { get; set; }
        public string ImageUrl { get; set; }
        public string BrandImageThumbnailUrl { get; set; }

        public virtual Category Categorys { get; set; }
        public virtual Company Companys { get; set; }
    }
}

