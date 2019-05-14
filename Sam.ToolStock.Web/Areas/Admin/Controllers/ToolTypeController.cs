using System.Web.Mvc;

namespace Sam.ToolStock.Web.Areas.Admin.Controllers
{
    public class ToolTypeController : Controller
    {


        public ActionResult Create()
        {
            return View("ModalMessage");
        }
    }
}