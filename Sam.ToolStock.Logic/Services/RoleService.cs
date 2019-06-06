using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Sam.ToolStock.Logic.Interfaces;
using Sam.ToolStock.Model.ViewModels;

namespace Sam.ToolStock.Logic.Services
{
    public class RoleService : IRoleService
    {
        private bool _disposed;
        private readonly ApplicationRoleManager _roleManager;
        private readonly IMapper _mapper;

        public RoleService(ApplicationRoleManager roleManager, IMapper mapper)
        {
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public IEnumerable<RoleViewModel> GetAll()
        {
            return _mapper.Map<IEnumerable<RoleViewModel>>(_roleManager.Roles.ToList());
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
                _roleManager.Dispose();
            }

            _disposed = true;
        }
    }
}
