using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using Sam.ToolStock.Web.Handlers;

namespace Sam.ToolStock.Web.Extensions
{
    public static class AreaRegistrationContextExtensions
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings",
            Justification = "This is a URL template with special characters, not just a regular valid URL.")]
        public static Route MapRouteToLocalizeRedirect(this AreaRegistrationContext context, string name, string url, object defaults)
        {
            //if (namespaces == null && this.Namespaces != null)
            //    namespaces = this.Namespaces.ToArray<string>();
            //Route route = this.Routes.MapRoute(name, url, defaults, constraints, namespaces);
            //route.DataTokens["area"] = (object)this.AreaName;
            //route.DataTokens["UseNamespaceFallback"] = (object)(bool)(namespaces == null ? (true ? 1 : 0) : (namespaces.Length == 0 ? 1 : 0));
            //return route;

            Route route = context.Routes.MapRoute(name, url, defaults);
            route.DataTokens["area"] = (object)context.AreaName;
            route.DataTokens["UseNamespaceFallback"] = (object)true;
            route.RouteHandler = new LocalizationRedirectRouteHandler();

            return route;

            //var redirectRoute = new Route(url, new RouteValueDictionary(defaults), new LocalizationRedirectRouteHandler());
            //context.Routes.Add(name, redirectRoute);

            //return redirectRoute;
        }

        public static Route MapRouteToLocalizeRedirect(this AreaRegistrationContext context, string name, string url, object defaults, string[] namespaces)
        {
            //if (namespaces == null && this.Namespaces != null)
            //    namespaces = this.Namespaces.ToArray<string>();
            //Route route = this.Routes.MapRoute(name, url, defaults, constraints, namespaces);
            //route.DataTokens["area"] = (object)this.AreaName;
            //route.DataTokens["UseNamespaceFallback"] = (object)(bool)(namespaces == null ? (true ? 1 : 0) : (namespaces.Length == 0 ? 1 : 0));
            //return route;
            if (namespaces == null && context.Namespaces != null)
                namespaces = context.Namespaces.ToArray<string>();
            Route route = context.Routes.MapRoute(name, url, defaults, null, namespaces);
            route.DataTokens["area"] = (object)context.AreaName;
            route.DataTokens["UseNamespaceFallback"] = (namespaces == null || (namespaces.Length == 0 ? true : false));
            route.RouteHandler = new LocalizationRedirectRouteHandler();

            return route;

            //var redirectRoute = new Route(
            //    url,
            //    new RouteValueDictionary(defaults),
            //    new RouteValueDictionary(new { }),
            //    new RouteValueDictionary(),
            //    new LocalizationRedirectRouteHandler()
            //);
            //if (namespaces != null && namespaces.Length != 0)
            //    redirectRoute.DataTokens["Namespaces"] = namespaces;

            //context.Routes.Add(name, redirectRoute);

            //return redirectRoute;
        }

        public static Route MapLocalizeRoute(this AreaRegistrationContext context, string name, string url, object defaults)
        {
            return context.Routes.MapLocalizeRoute(name, url, defaults, new { });
        }

        public static Route MapLocalizeRoute(this AreaRegistrationContext context, string name, string url, object defaults, object constraints)
        {
            //if (namespaces == null && this.Namespaces != null)
            //    namespaces = this.Namespaces.ToArray<string>();
            //Route route = this.Routes.MapRoute(name, url, defaults, constraints, namespaces);
            //route.DataTokens["area"] = (object)this.AreaName;
            //route.DataTokens["UseNamespaceFallback"] = (object)(bool)(namespaces == null ? (true ? 1 : 0) : (namespaces.Length == 0 ? 1 : 0));
            //return route;

            Route route = context.Routes.MapRoute(name, url, defaults, constraints, null);
            route.DataTokens["area"] = (object)context.AreaName;
            route.DataTokens["UseNamespaceFallback"] = (object)true;
            route.RouteHandler = new LocalizedRouteHandler();

            return route;

            //var route = new Route(
            //    url,
            //    new RouteValueDictionary(defaults),
            //    new RouteValueDictionary(constraints),
            //    new LocalizedRouteHandler());

            //context.Routes.Add(name, route);

            //return route;
        }

        public static Route MapLocalizeRoute(this AreaRegistrationContext context, string name, string url, object defaults, object constraints, string[] namespaces)
        {
            if (namespaces == null && context.Namespaces != null)
                namespaces = context.Namespaces.ToArray<string>();
            Route route = context.Routes.MapRoute(name, url, defaults, null, namespaces);
            route.DataTokens["area"] = (object)context.AreaName;
            route.DataTokens["UseNamespaceFallback"] = (namespaces == null || (namespaces.Length == 0 ? true : false));
            route.RouteHandler = new LocalizedRouteHandler();

            return route;

            //var route = new Route(
            //    url,
            //    new RouteValueDictionary(defaults),
            //    new RouteValueDictionary(constraints),
            //    new RouteValueDictionary(),
            //    new LocalizedRouteHandler()
            //);

            //if (namespaces != null && namespaces.Length != 0)
            //    route.DataTokens["Namespaces"] = namespaces;

            //context.Routes.Add(name, route);

            //return route;
        }
    }
}