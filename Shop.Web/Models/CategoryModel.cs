using Shopp.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Web
{
    public class CategoryModel
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<BrandModel> Brands { get; set; }
    }
}
