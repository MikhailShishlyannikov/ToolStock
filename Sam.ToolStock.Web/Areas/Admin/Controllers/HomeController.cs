using System.Web.Mvc;

namespace Sam.ToolStock.Web.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        // GET: Admin/Home
        public ActionResult Index()
        {

            return View();
        }
    }
}