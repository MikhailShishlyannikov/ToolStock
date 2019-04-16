using System;
using AutoMapper;
using Sam.ToolStock.DataProvider.Interfaces;
using Sam.ToolStock.DataProvider.Models;
using Sam.ToolStock.Logic.Interfaces;
using Sam.ToolStock.Model.ViewModels;

namespace Sam.ToolStock.Logic.Services
{
    public class UserInfoService : IUserInfoService
    {
        private readonly ApplicationUserManager _userManager;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UserInfoService(ApplicationUserManager userManager, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public void Add(RegisterViewModel registerViewModel, UserModel user)
        {
            var userInfo = _mapper.Map<UserInfoModel>(registerViewModel);
            userInfo.Id = user.Id;

            _unitOfWork.UserInfoRepository.Create(userInfo);
            _unitOfWork.Save();
        }

        public ProfileViewModel GetProfile(string userId)
        {
            return _mapper.Map<ProfileViewModel>(_unitOfWork.UserInfoRepository.GetById(userId));
        }

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }
    }
}
