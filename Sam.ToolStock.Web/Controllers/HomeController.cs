using System.Web;
using System.Web.Mvc;

namespace Sam.ToolStock.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated && User.IsInRole("Admin"))
                return RedirectToAction("Index", new {area = "Admin", controller = "Home"});
            if (User.Identity.IsAuthenticated && User.IsInRole("Stock keeper"))
                return RedirectToAction("Index", new {area = "Keepers", controller = "Home"});
            if (User.Identity.IsAuthenticated && User.IsInRole("User"))
                return RedirectToAction("Index", new {area = "Users", controller = "Home"});

            return View();
        }

        public ActionResult ChangeLang(string lang, string returnUrl)
        {
            var langCookie = new HttpCookie("locale", lang) { HttpOnly = true };
            Response.AppendCookie(langCookie);
            return Redirect(HttpUtility.UrlDecode(returnUrl));
        }
    }
}