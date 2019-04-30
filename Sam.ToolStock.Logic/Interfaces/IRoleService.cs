using System;
using System.Collections.Generic;
using Sam.ToolStock.Model.ViewModels;

namespace Sam.ToolStock.Logic.Interfaces
{
    public interface IRoleService : IDisposable
    {
        IEnumerable<RoleViewModel> GetAll();
    }
}
