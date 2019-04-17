using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Sam.ToolStock.DataProvider.Models;
using Sam.ToolStock.Logic.Interfaces;
using Sam.ToolStock.Model.Models;
using Sam.ToolStock.Model.ViewModels;

namespace Sam.ToolStock.Logic.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly ApplicationUserManager _userManager;
        private readonly ApplicationRoleManager _roleManager;
        private readonly IUserInfoService _userInfoService;

        public UserService(
            IMapper mapper, 
            ApplicationUserManager userManager, 
            ApplicationRoleManager roleManager, 
            IUserInfoService userInfoService)
        {
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
            _userInfoService = userInfoService;
        }
        
        public IdentityResult Create(RegisterViewModel registerViewModel)
        {
            var user = _mapper.Map<UserModel>(registerViewModel);

            var result = _userManager.Create(user, registerViewModel.Password);
            _userInfoService.Add(registerViewModel, user);
            return result;
        }

        public IdentityResult AddToUserRole(RegisterViewModel registerViewModel)
        {
            return _userManager.AddToRole(_userManager.Users.First(u => u.UserName == registerViewModel.Email).Id,
                _roleManager.Roles.First(r => r.Name == "User").Name);
        }

        public User GetUser(LoginViewModel loginViewModel)
        {
            return _mapper.Map<User>(_userManager.Find(loginViewModel.Email, loginViewModel.Password));
        }

        public IEnumerable<TableUserViewModel> GetAllTableUser()
        {
            var tableUsers = _mapper.Map<IEnumerable<TableUserViewModel>>(_userInfoService.GetAll()).ToList();
            foreach (var tableUser in tableUsers)
            {
                tableUser.Role = GetRoles(tableUser.Id).Single();
            }

            return tableUsers;
        }

        public IEnumerable<string> GetRoles(string userId)
        {
            return _userManager.GetRoles(userId);
        }

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }
    }
}
