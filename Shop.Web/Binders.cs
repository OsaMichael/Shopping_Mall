using Ninject.Modules;
using Ninject.Web.Common;
using Shop.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Shop.Web
{
    public class Binders: NinjectModule
    {
        public override void Load()
        {
            //Kernel.Bind<DbContext>().ToSelf().InRequestScope();
            Kernel.Bind<DbContext>().To<ApplicationDbContext>().InRequestScope();

          //  Bind<IDepartmentService>().To<DepartmentService>().InRequestScope();
   
        }
    }
}