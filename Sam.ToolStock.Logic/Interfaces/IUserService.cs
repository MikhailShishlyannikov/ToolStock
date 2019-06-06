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

        IEnumerable<UserViewModel> GetAll();

        IEnumerable<UserViewModel> GetAll(bool showDeleted);

        UserViewModel GetUser(LoginViewModel loginViewModel);

        UserViewModel GetUser(string userId);

        UserProfileViewModel GetUserProfile(string id);

        ProfileViewModel GetProfile(string userId);

        IEnumerable<TableUserViewModel> GetAllTableUser();

        UserInfoViewModel GetUserInfo(string id);

        IEnumerable<string> GetRoles(string userId);

        bool Update(UserViewModel user);

        void Update(UserProfileViewModel user);

        void Delete(UserViewModel user);
    }
}
