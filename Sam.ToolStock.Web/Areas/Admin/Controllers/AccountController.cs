using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Sam.ToolStock.Logic.Interfaces;

namespace Sam.ToolStock.Web.Areas.Admin.Controllers
{
    public class AccountController : Controller
    {
        private IUserInfoService _userInfoService;

        public AccountController(IUserInfoService userInfoService)
        {
            _userInfoService = userInfoService;
        }

        // GET: Admin/Account
        public ActionResult UserInfo()
        {
            var profile = _userInfoService.GetProfile(User.Identity.GetUserId());

            return PartialView("_ProfileBar", profile);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            HttpContext.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", new {area = "", controller = "Home"});
        }
    }
}