using System.Web.Mvc;
using Sam.ToolStock.Web.Extensions;

namespace Sam.ToolStock.Web.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {


            context.MapLocalizeRoute("Admin_Default",
                url: "{culture}/Admin/{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                constraints: new { culture = "[a-zA-Z]{2}-[a-zA-Z]{2}" },
                namespaces: new[] { "Sam.ToolStock.Web.Areas.Admin.Controllers" }
                );

            context.MapRouteToLocalizeRedirect("Admin_RedirectToLocalize",
                url: "{controller}/Admin/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "Sam.ToolStock.Web.Areas.Admin.Controllers" }
                );

            //context.MapRoute(
            //    "Admin_default",
            //    "{culture}/Admin/{controller}/{action}/{id}",
            //    new { action = "Index", id = UrlParameter.Optional }
            //);
        }
    }
}