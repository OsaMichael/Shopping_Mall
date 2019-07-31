using Shop.Web.Atrributes;
using Shop.Web.Entities;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace Shop.Web
{
    //[Bind(Exclude = "BrandId")]
    public  class BrandModel
    {
     //   [ScaffoldColumn(false)]
        public int BrandId { get; set; }
        [DisplayName("Category")]
        public int CategoryId { get; set; }
        [DisplayName("Company")]
        public int CompanId { get; set; }
        public string BrandName { get; set; }
        [Required(ErrorMessage = "Price is required")]
        [Range(0.01, 100.00, ErrorMessage = "Price must be between 0.01 and 100.00")]

        public decimal Price { get; set; }
        //[DisplayName("Brand Art URL")]
        //[StringLength(1024)]
        [FileTypes("jpg,jpeg,png")]
        public HttpPostedFileBase BrandImageFile { get; set; }
        public string BrandUrl { get; set; }
        public string ImageUrl { get; set; }
        public string BrandImageThumbnailUrl { get; set; }


        public virtual Category Categorys { get; set; }
        public virtual Company Companys { get; set; }
    }
}
