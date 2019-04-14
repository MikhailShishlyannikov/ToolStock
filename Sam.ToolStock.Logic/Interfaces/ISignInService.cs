using System;
using Microsoft.AspNet.Identity.Owin;
using Sam.ToolStock.Model.ViewModels;

namespace Sam.ToolStock.Logic.Interfaces
{
    public interface ISignInService : IDisposable
    {
        SignInStatus PasswordSignIn(LoginViewModel loginViewModel);

        void SignIn(RegisterViewModel registerViewModel);
    }
}
