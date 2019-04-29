using System;
using System.Collections.Generic;
using Microsoft.AspNet.Identity;
using Sam.ToolStock.DataProvider.Models;
using Sam.ToolStock.Model.ViewModels;

namespace Sam.ToolStock.Logic.Interfaces
{
    public interface IUserService : IDisposable
    {
        IdentityResult Create(RegisterViewModel registerViewModel);

        void Add(RegisterViewModel registerViewModel, UserModel user);

        IdentityResult AddToUserRole(RegisterViewModel registerViewModel);

        UserViewModel GetUser(LoginViewModel loginViewModel);

        UserViewModel GetUser(string userId);

        ProfileViewModel GetProfile(string userId);

        IEnumerable<TableUserViewModel> GetAllTableUser();

        IEnumerable<string> GetRoles(string userId);

        bool Update(UserViewModel user);

        void Delete(UserViewModel user);
    }
}
