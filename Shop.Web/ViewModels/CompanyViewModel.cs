using Shop.Web.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop.Web.ViewModels
{
    public class CompanyViewModel
    {
        public IEnumerable<Company> Companies { get; set; }
    }
}