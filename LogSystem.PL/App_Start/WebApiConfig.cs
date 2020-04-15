using LogSystem.PL.Utils;
using System.Web.Http;

namespace LogSystem.PL
{
    public static class WebApiConfig
    {
        public static string UrlPrefix { get { return "api"; } }
        public static string UrlPrefixRelative { get { return "~/api"; } }

        public static void Register(HttpConfiguration config)
        {
            config.DependencyResolver = new NinjectDependencyResolver();
            config.MapHttpAttributeRoutes();
            config.Filters.Add(new AuthorizeAttribute());

            config.Routes.MapHttpRoute(
               name: "Actions",
               routeTemplate: WebApiConfig.UrlPrefix + "/{controller}/{action}"
           );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: WebApiConfig.UrlPrefix + "/{controller}/{id}",
                defaults: new {  id = RouteParameter.Optional }
            );
        }
    }
}
