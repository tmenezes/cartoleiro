using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Cartoleiro.Web.AppCode;
using Cartoleiro.Web.AppCode.ScoutsAoVivo;

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
            ScoutsAoVivoFacade.Iniciar();
            AtualizadorCartola.Iniciar();
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            ScoutsAoVivoFacade.SetHttpContext(HttpContext.Current);
        }

        protected void Application_EndRequest(object sender, EventArgs e)
        {
            CartoleiroKeepAlive.Iniciar(HttpContext.Current);
        }
    }
}
