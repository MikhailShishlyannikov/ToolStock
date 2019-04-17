using System.Linq;
using System.Web.Mvc;
using Sam.ToolStock.Logic.Interfaces;

namespace Sam.ToolStock.Web.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserService _userService;

        public HomeController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: Admin/Home
        public ActionResult Index()
        {
            var vms = _userService.GetAllTableUser().ToList();
            return View(vms);
        }
    }
}