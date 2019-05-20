using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sam.ToolStock.Web.Areas.Keepers.Controllers
{
    public class HomeController : Controller
    {
        // GET: Keepers/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}