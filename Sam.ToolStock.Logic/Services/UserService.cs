using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Sam.ToolStock.DataProvider.Interfaces;
using Sam.ToolStock.DataProvider.Models;
using Sam.ToolStock.Logic.Interfaces;
using Sam.ToolStock.Model.ViewModels;

namespace Sam.ToolStock.Logic.Services
{
    public class UserService : IUserService
    {
        private bool _disposed;
        private readonly IMapper _mapper;
        private readonly ApplicationUserManager _userManager;
        private readonly IRoleService _roleService;
        private readonly IDepartmentService _departmentService;
        private readonly IStockService _stockService;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IMapper mapper, ApplicationUserManager userManager, IRoleService roleService, IDepartmentService departmentService, IStockService stockService, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _userManager = userManager;
            _roleService = roleService;
            _departmentService = departmentService;
            _stockService = stockService;
            _unitOfWork = unitOfWork;
        }

        public void Add(RegisterViewModel registerViewModel, UserModel user)
        {
            var userInfo = _mapper.Map<UserInfoModel>(registerViewModel);
            userInfo.Id = user.Id;

            _unitOfWork.UserInfoRepository.Create(userInfo);
            _unitOfWork.Save();
        }

        public IdentityResult Create(RegisterViewModel registerViewModel)
        {
            var user = _mapper.Map<UserModel>(registerViewModel);

            var result = _userManager.Create(user, registerViewModel.Password);
            Add(registerViewModel, user);
            return result;
        }

        public IdentityResult AddToUserRole(RegisterViewModel registerViewModel)
        {
            var userId = _userManager.Users.First(u => u.UserName == registerViewModel.Email).Id;
            var roleName = _roleService.GetAll().First(r => r.Name == "User").Name;

            return _userManager.AddToRole(userId, roleName);
        }

        public IEnumerable<UserViewModel> GetAll()
        {
            return _mapper.Map<IEnumerable<UserViewModel>>(_unitOfWork.UserInfoRepository.GetAll());
        }

        public IEnumerable<UserViewModel> GetAll(bool showDeleted)
        {
            if (showDeleted) return GetAll();

            return _mapper.Map<IEnumerable<UserViewModel>>(_unitOfWork.UserInfoRepository.GetAll()
                .Where(ui => ui.IsDeleted == false));
        }

        public UserViewModel GetUser(LoginViewModel loginViewModel)
        {
            return _mapper.Map<UserViewModel>(_userManager.Find(loginViewModel.Email, loginViewModel.Password));
        }

        public UserViewModel GetUser(string userId)
        {
            var userModel = _unitOfWork.UserInfoRepository.GetById(userId);

            var user = _mapper.Map<UserViewModel>(userModel);
            user.Role = _userManager.GetRoles(userId).First();

            if (user.DepartmentId == null)
            {
                user.DepartmentId = new Guid().ToString();
            }

            if (user.StockId == null)
            {
                user.StockId = new Guid().ToString();
            }

            return user;
        }

        public UserProfileViewModel GetUserProfile(string userId)
        {
            var userModel = _unitOfWork.UserInfoRepository.GetById(userId);
            var user = _mapper.Map<UserProfileViewModel>(userModel);
            user.Role = _userManager.GetRoles(userId).First();
            user.Department = _departmentService.Get(userModel.User.DepartmentId)?.Name;
            user.Stock = _stockService.Get(userModel.User.StockId)?.Name;

            return user;
        }

        public ProfileViewModel GetProfile(string userId)
        {
            return _mapper.Map<ProfileViewModel>(_unitOfWork.UserInfoRepository.GetById(userId));
        }

        public IEnumerable<TableUserViewModel> GetAllTableUser()
        {
            var tableUsers = _mapper.Map<IEnumerable<TableUserViewModel>>(GetUserInfoAll().Where(ui => ui.IsDeleted == false)).ToList();
            foreach (var tableUser in tableUsers)
            {
                tableUser.Role = GetRoles(tableUser.Id).Single();
            }

            return tableUsers;
        }

        public UserInfoViewModel GetUserInfo(string id)
        {
            var userModel = GetUser(id);
            var userInfoModel = _unitOfWork.UserInfoRepository.GetById(id);
            var userInfoViewModel = _mapper.Map<UserInfoViewModel>(userInfoModel);

            userInfoViewModel.Role = GetRoles(id).FirstOrDefault();
            userInfoViewModel.DepartmentName = _departmentService.Get(userModel.DepartmentId).Name;
            userInfoViewModel.StockName = _stockService.Get(userModel.StockId)?.Name;

            return userInfoViewModel;
        }

        public IEnumerable<string> GetRoles(string userId)
        {
            return _userManager.GetRoles(userId);
        }

        public bool Update(UserViewModel user)
        {
            if (user.DepartmentId == new Guid().ToString()) user.DepartmentId = null;
            if (user.StockId == new Guid().ToString()) user.StockId = null;

            var userInfoModel = _unitOfWork.UserInfoRepository.GetById(user.Id);
            var userModel = _userManager.FindById(user.Id);

            if (!_userManager.IsInRole(user.Id, user.Role))
            {
                _userManager.RemoveFromRole(userModel.Id, _userManager.GetRoles(user.Id).First());
                _userManager.AddToRole(user.Id, user.Role);
            }

            _mapper.Map(user, userModel);
            _mapper.Map(user, userInfoModel);

            _userManager.Update(userModel);
            _unitOfWork.UserInfoRepository.Update(userInfoModel);
            _unitOfWork.Save();

            return true;
        }

        public void Update(UserProfileViewModel user)
        {
            var userInfoModel = _unitOfWork.UserInfoRepository.GetById(user.Id);
            _mapper.Map(user, userInfoModel);
            _unitOfWork.UserInfoRepository.Update(userInfoModel);
            _unitOfWork.Save();
        }

        public void Delete(UserViewModel user)
        {
            user.IsDeleted = true;
            Update(user);
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
                _unitOfWork.Dispose();
                _userManager.Dispose();
                _departmentService.Dispose();
                _roleService.Dispose();
                _stockService.Dispose();
            }

            _disposed = true;
        }

        private IEnumerable<UserInfoModel> GetUserInfoAll()
        {
            return _unitOfWork.UserInfoRepository.GetAll();
        }
    }
}
