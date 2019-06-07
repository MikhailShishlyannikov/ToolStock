using System.Web.Mvc;

namespace Sam.ToolStock.Web.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult Error404()
        {
            return View();
        }

        public ActionResult Error500()
        {
            return View();
        }

    }
}