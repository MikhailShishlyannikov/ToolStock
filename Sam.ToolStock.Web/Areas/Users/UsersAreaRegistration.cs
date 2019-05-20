using System.Web.Mvc;
using Sam.ToolStock.Web.Extensions;

namespace Sam.ToolStock.Web.Areas.Users
{
    public class UsersAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Users";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapLocalizeRoute("Users_Default",
                url: "{culture}/Users/{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                constraints: new { culture = "[a-zA-Z]{2}-[a-zA-Z]{2}" },
                namespaces: new[] { "Sam.ToolStock.Web.Areas.Users.Controllers" }
            );

            context.MapRouteToLocalizeRedirect("Users_RedirectToLocalize",
                url: "{controller}/Users/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "Sam.ToolStock.Web.Areas.Users.Controllers" }
            );

            //context.MapRoute(
            //    "Users_default",
            //    "Users/{controller}/{action}/{id}",
            //    new { action = "Index", id = UrlParameter.Optional }
            //);
        }
    }
}