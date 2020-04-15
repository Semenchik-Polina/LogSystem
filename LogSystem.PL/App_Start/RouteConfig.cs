using System.Web.Mvc;
using System.Web.Routing;

namespace LogSystem.PL
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "CatchAll", 
                url: "{*path}", 
                defaults: new { controller = "Main", action = "Index"}
            );
        }
    }
}
