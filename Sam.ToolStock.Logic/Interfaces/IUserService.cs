using System;
using Microsoft.AspNet.Identity;
using Sam.ToolStock.Model.ViewModels;

namespace Sam.ToolStock.Logic.Interfaces
{
    public interface IUserService : IDisposable
    {
        IdentityResult Create(RegisterViewModel registerViewModel);

        IdentityResult AddToUserRole(RegisterViewModel registerViewModel);

        
    }
}
