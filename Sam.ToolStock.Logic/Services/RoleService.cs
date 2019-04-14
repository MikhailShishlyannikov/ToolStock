using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Sam.ToolStock.Logic.Interfaces;
using Sam.ToolStock.Model.Models;

namespace Sam.ToolStock.Logic.Services
{
    public class RoleService : IRoleService
    {
        private readonly ApplicationRoleManager _roleManager;
        private readonly IMapper _mapper;

        public RoleService(ApplicationRoleManager roleManager, IMapper mapper)
        {
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public IEnumerable<Role> GetAll()
        {
            return _mapper.Map<IEnumerable<Role>>(_roleManager.Roles.ToList());
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
