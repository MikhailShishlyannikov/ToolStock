using System.Web.Routing;
using Sam.ToolStock.Web.Handlers;

namespace Sam.ToolStock.Web.Extensions
{
    public static class RouteCollectionExtensions
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings",
            Justification = "This is a URL template with special characters, not just a regular valid URL.")]
        public static Route MapRouteToLocalizeRedirect(this RouteCollection routes, string name, string url, object defaults)
        {
            var redirectRoute = new Route(url, new RouteValueDictionary(defaults), new LocalizationRedirectRouteHandler());
            routes.Add(name, redirectRoute);

            return redirectRoute;
        }

        public static Route MapRouteToLocalizeRedirect(this RouteCollection routes, string name, string url, object defaults, string[] namespaces)
        {
            var redirectRoute = new Route(
                url, 
                new RouteValueDictionary(defaults), 
                new RouteValueDictionary(new {}),
                new RouteValueDictionary(),
                new LocalizationRedirectRouteHandler()
                );
            if (namespaces != null && namespaces.Length != 0)
                redirectRoute.DataTokens["Namespaces"] = namespaces;

            routes.Add(name, redirectRoute);

            return redirectRoute;
        }

        public static Route MapLocalizeRoute(this RouteCollection routes, string name, string url, object defaults)
        {
            return routes.MapLocalizeRoute(name, url, defaults, new { });
        }

        public static Route MapLocalizeRoute(this RouteCollection routes, string name, string url, object defaults, object constraints)
        {
            var route = new Route(
                url,
                new RouteValueDictionary(defaults),
                new RouteValueDictionary(constraints),
                new LocalizedRouteHandler());

            routes.Add(name, route);

            return route;
        }

        public static Route MapLocalizeRoute(this RouteCollection routes, string name, string url, object defaults, object constraints, string[] namespaces)
        {
            var route = new Route(
                url,
                new RouteValueDictionary(defaults),
                new RouteValueDictionary(constraints),
                new RouteValueDictionary(),
                new LocalizedRouteHandler()
                );

            if (namespaces != null && namespaces.Length != 0)
                route.DataTokens["Namespaces"] = namespaces;

            routes.Add(name, route);

            return route;
        }
    }
}