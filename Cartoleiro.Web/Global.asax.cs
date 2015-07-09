using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Cartoleiro.Web.AppCode;
using Cartoleiro.Web.Models;
using Cartoleiro.Web;

namespace Cartoleiro.Web
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            CartoleiroApp.Iniciar();
        }

        protected void Application_EndRequest(object sender, EventArgs e)
        {
            CartoleiroKeepAlive.Iniciar(HttpContext.Current);
        }
    }
}
