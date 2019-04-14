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
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UserInfoService(IMapper mapper, IUnitOfWork unitOfWork)
        {
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

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }
    }
}
