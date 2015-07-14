using System.Web.Mvc;
using System.Web.Routing;

namespace Cartoleiro.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}/{detalhe}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional, detalhe = UrlParameter.Optional }
            );
        }
    }
}
