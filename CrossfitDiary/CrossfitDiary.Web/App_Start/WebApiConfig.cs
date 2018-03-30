using System.Web.Http;
using System.Web.Http.Filters;


namespace CrossfitDiary.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Attribute routing.
            config.MapHttpAttributeRoutes();

            // Convention-based routing.
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }

        public static void RegisterWebApiFilters(HttpFilterCollection filters)
        {
            filters.Add(new AuthorizeAttribute());
        }
    }
}
