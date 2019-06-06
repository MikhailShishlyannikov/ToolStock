using System;
using System.Linq;
using Microsoft.AspNet.Identity.Owin;
using Sam.ToolStock.Logic.Interfaces;
using Sam.ToolStock.Model.ViewModels;

namespace Sam.ToolStock.Logic.Services
{
    public class SignInService : ISignInService
    {
        private bool _disposed;
        private readonly ApplicationSignInManager _signInManager;
        private readonly ApplicationUserManager _userManager;

        public SignInService(ApplicationSignInManager signInManager, ApplicationUserManager userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public SignInStatus PasswordSignIn(LoginViewModel loginViewModel)
        {
            return _signInManager.PasswordSignIn(loginViewModel.Email, loginViewModel.Password,
                loginViewModel.RememberMe, shouldLockout: false);
        }

        public void SignIn(RegisterViewModel registerViewModel)
        {
            var user = _userManager.Users.FirstOrDefault(u => u.UserName == registerViewModel.Email);
            _signInManager.SignIn(user, isPersistent: false, rememberBrowser: false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;
            if (disposing)
            {
                _signInManager.Dispose();
                _userManager.Dispose();
            }

            _disposed = true;
        }
    }
}
