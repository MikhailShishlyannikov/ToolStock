using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Sam.ToolStock.DataProvider.Interfaces;
using Sam.ToolStock.DataProvider.Models;
using Sam.ToolStock.Logic.Interfaces;
using Sam.ToolStock.Logic.Services;
using Sam.ToolStock.Model.ViewModels;

namespace Sam.ToolStock.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly ISignInService _signInService;
        private readonly IUserService _userService;
        private readonly IDepartmentService _departmentService;

        public AccountController(ISignInService signInService, IUserService userService, IDepartmentService departmentService)
        {
            _signInService = signInService;
            _userService = userService;
            _departmentService = departmentService;
        }

        
        
        // GET: Account
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel loginViewModel, string redirectUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(loginViewModel);
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = _signInService.PasswordSignIn(loginViewModel);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToAction("Index", "Home");
                case SignInStatus.LockedOut:
                    return View("_Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = redirectUrl, RememberMe = loginViewModel.RememberMe });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(loginViewModel);
            }
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            var departments = new SelectList(_departmentService.GetAll(), "Id", "Name");
            ViewBag.Departments = departments;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid) return View(registerViewModel);

            var result = _userService.Create(registerViewModel);

            if (result.Succeeded)
            {
                _userService.AddToUserRole(registerViewModel);
                _signInService.SignIn(registerViewModel);

                // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                return RedirectToAction("555", "Home");

            }
            AddErrors(result);

            // If we got this far, something failed, redisplay form
            return View(registerViewModel);

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