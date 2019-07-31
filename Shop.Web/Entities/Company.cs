using Shopp.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Shop.Web.Entities
{
    public class Company
    {
        [Key]
        public int CompanId { get; set; }
        public string CompName { get; set; }
       // public List<Brand> Brands { get; set; }
    }
}
