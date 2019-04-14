using System;
using Sam.ToolStock.DataProvider.Models;
using Sam.ToolStock.Model.ViewModels;

namespace Sam.ToolStock.Logic.Interfaces
{
    public interface IUserInfoService : IDisposable
    {
        void Add(RegisterViewModel registerViewModel, UserModel user);
    }
}
