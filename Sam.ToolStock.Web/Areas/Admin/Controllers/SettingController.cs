using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Sam.ToolStock.Logic.Services;
using Sam.ToolStock.Model.ViewModels;

namespace Sam.ToolStock.Web.Areas.Admin.Controllers
{
    public class SettingController : Controller
    {
        private readonly ApplicationUserManager _userManager;
        private readonly ApplicationSignInManager _signInManager;

        public SettingController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // GET: Admin/Setting
        public ActionResult Index()
        {
            return View(new ChangePasswordViewModel());
        }

        public ActionResult ChangeLang(string lang, string returnUrl)
        {
            var langCookie = new HttpCookie("locale", lang) { HttpOnly = true };
            Response.AppendCookie(langCookie);
            return Redirect(HttpUtility.UrlDecode(returnUrl));
        }

        //
        // POST: /Manage/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", model);
            }
            var result = _userManager.ChangePassword(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                var user = _userManager.FindById(User.Identity.GetUserId());
                if (user != null)
                {
                    _signInManager.SignIn(user, isPersistent: false, rememberBrowser: false);
                }

                var message = new ModalMessageViewModel
                {
                    Message = string.Format(
                        Resources.Resource.ModalPageMessageChange,
                        Resources.Resource.Your,
                        Resources.Resource.Password

                    ),
                    MessageType = "success",
                    PageName = Resources.Resource.SettingPage,
                    Action = "Index",
                    Controller = "Setting"
                };

                return View("ModalMessage", message);
            }
            AddErrors(result);
            return View("Index", model);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
    }
}