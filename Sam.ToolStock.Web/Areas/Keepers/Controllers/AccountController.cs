using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Sam.ToolStock.Logic.Interfaces;
using Sam.ToolStock.Model.ViewModels;

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

        public new ActionResult Profile()
        {
            var userId = User.Identity.GetUserId();
            var user = _userService.GetUserProfile(userId);

            return View(user);
        }

        [HttpPost]
        public ActionResult Update(UserProfileViewModel user)
        {
            if (!ModelState.IsValid)
            {
                return View("Profile", user);
            }

            _userService.Update(user);

            var message = new ModalMessageViewModel
            {
                Message = string.Format(Resources.Resource.ModalPageMessageUpdate, Resources.Resource.User, user.FullName),
                MessageType = "success",
                PageName = Resources.Resource.UserProfilePage,
                Action = "Profile",
                Controller = "Account"
            };
            return View("ModalMessage", message);
        }

        public ActionResult Reset(string userId)
        {
            return RedirectToAction("Profile");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            HttpContext.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", new { area = "", controller = "Home" });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _userService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}