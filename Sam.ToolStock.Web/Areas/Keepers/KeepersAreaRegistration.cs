using System.Web.Mvc;
using Sam.ToolStock.Web.Extensions;

namespace Sam.ToolStock.Web.Areas.Keepers
{
    public class KeepersAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Keepers";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapLocalizeRoute("Keepers_Default",
                url: "{culture}/Keepers/{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                constraints: new { culture = "[a-zA-Z]{2}-[a-zA-Z]{2}" },
                namespaces: new[] { "Sam.ToolStock.Web.Areas.Keepers.Controllers" }
            );

            context.MapRouteToLocalizeRedirect("Keepers_RedirectToLocalize",
                url: "{controller}/Keepers/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "Sam.ToolStock.Web.Areas.Keepers.Controllers" }
            );

            //context.MapRoute(
            //    "Keepers_default",
            //    "Keepers/{controller}/{action}/{id}",
            //    new { action = "Index", id = UrlParameter.Optional }
            //);
        }
    }
}