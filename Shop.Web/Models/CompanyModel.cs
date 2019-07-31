using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Shop.Web
{
    public class CompanyModel
    {
     
        public int CompanId { get; set; }
        public string CompName { get; set; }
        public string CompanyImageUrl { get; set; }
        public List<BrandModel> Brands { get; set; }
        // [FileTypes("jpg,jpeg,png")]

        // public HttpPostedFileBase CompanyImageThumbnailUrl { get; set; }

    }
}
