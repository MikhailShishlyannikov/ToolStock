using System;
using System.Collections.Generic;
using Microsoft.AspNet.Identity;
using Sam.ToolStock.Model.Models;
using Sam.ToolStock.Model.ViewModels;

namespace Sam.ToolStock.Logic.Interfaces
{
    public interface IUserService : IDisposable
    {
        IdentityResult Create(RegisterViewModel registerViewModel);

        IdentityResult AddToUserRole(RegisterViewModel registerViewModel);

        User GetUser(LoginViewModel loginViewModel);

        IEnumerable<string> GetRoles(string userId);
    }
}
