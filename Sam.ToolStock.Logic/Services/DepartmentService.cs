using System;
using System.Collections.Generic;
using AutoMapper;
using Sam.ToolStock.DataProvider.Interfaces;
using Sam.ToolStock.DataProvider.Models;
using Sam.ToolStock.Logic.Interfaces;
using Sam.ToolStock.Model.ViewModels;

namespace Sam.ToolStock.Logic.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DepartmentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public void Create(DepartmentViewModel departmentViewModel)
        {
            departmentViewModel.Id = Guid.NewGuid().ToString();
            _unitOfWork.DepartmentRepository.Create(_mapper.Map<DepartmentModel>(departmentViewModel));
            _unitOfWork.Save();
        }

        public IEnumerable<DepartmentViewModel> GetAll()
        {
            return _mapper.Map<IEnumerable<DepartmentViewModel>>(_unitOfWork.DepartmentRepository.GetAll());
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
