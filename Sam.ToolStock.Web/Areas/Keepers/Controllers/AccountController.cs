using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Sam.ToolStock.Logic.Interfaces;

namespace Sam.ToolStock.Web.Areas.Keepers.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: Admin/Account
        public ActionResult UserInfo()
        {
            var profile = _userService.GetProfile(User.Identity.GetUserId());

            return PartialView("_ProfileBar", profile);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            HttpContext.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", new { area = "", controller = "Home" });
        }
    }
}