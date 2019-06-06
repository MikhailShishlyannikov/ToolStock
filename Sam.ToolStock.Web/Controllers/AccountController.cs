using System;
using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Sam.ToolStock.Logic.Interfaces;
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

            var user = _userService.GetUser(loginViewModel);

            if (user != null && user.IsDeleted)
            {
                ModelState.AddModelError("", Resources.Resource.YourAccountHasBeenBlocked);
                return View(loginViewModel);
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = _signInService.PasswordSignIn(loginViewModel);
            switch (result)
            {
                case SignInStatus.Success:
                    var roles = _userService.GetRoles(user.Id);

                    if (roles.Contains("Admin"))
                        return RedirectToAction("Index", new {area = "Admin", controller = "Home"});
                    if (roles.Contains("Stock keeper"))
                        return RedirectToAction("Index", new { area = "Keepers", controller = "Home" });
                    if (roles.Contains("User"))
                        return RedirectToAction("Index", new { area = "Users", controller = "Home" });
                    return RedirectToAction("Index", "Home");
                case SignInStatus.LockedOut:
                    return View("_Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = redirectUrl, RememberMe = loginViewModel.RememberMe });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", Resources.Resource.LoginInvalidEmailOrPassword);
                    return View(loginViewModel);
            }
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            var departments = _departmentService.GetAll(false).ToList();
            departments.Insert(0, new DepartmentViewModel
            {
                Id = new Guid().ToString(),
                Name = Resources.Resource.ChooseDepartment
            });

            var registerViewModel = new RegisterViewModel
            {
                DepartmentId = new Guid().ToString(),
                Departments = departments
            };

            return View(registerViewModel);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid)
            {
                var departments = _departmentService.GetAll(false).ToList();
                departments.Insert(0, new DepartmentViewModel
                {
                    Id = new Guid().ToString(),
                    Name = Resources.Resource.ChooseDepartment
                });

                registerViewModel.Departments = departments;

                return View(registerViewModel);
            }

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

                return RedirectToAction("Index", new {controller = "Home", area = "Users"});

            }
            AddErrors(result);

            // If we got this far, something failed, redisplay form
            return View(registerViewModel);

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _departmentService.Dispose();
                _signInService.Dispose();
                _userService.Dispose();
            }
            base.Dispose(disposing);
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