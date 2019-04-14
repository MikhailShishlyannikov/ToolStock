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
            return View();
        }
    }
}