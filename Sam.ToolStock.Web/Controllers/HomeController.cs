using System.Web.Mvc;

namespace Sam.ToolStock.Web.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
            
        }

        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated && User.IsInRole("Admin"))
                return RedirectToAction("Index", new {area = "Admin", controller = "Home"});
            if (User.Identity.IsAuthenticated && User.IsInRole("Stock keeper"))
                return RedirectToAction("Index", new {area = "Keeper", controller = "Home"});
            if (User.Identity.IsAuthenticated && User.IsInRole("User"))
                return RedirectToAction("Index", new {area = "User", controller = "Home"});

            return View();
        }
    }
}