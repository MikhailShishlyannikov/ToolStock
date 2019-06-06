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
        private bool _disposed;
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

        public IEnumerable<DepartmentViewModel> GetAll(bool showDeleted)
        {
            return showDeleted
                ? GetAll()
                : _mapper.Map<IEnumerable<DepartmentViewModel>>(
                    _unitOfWork.DepartmentRepository.GetWhere(d => d.IsDeleted != true));
        }

        public DepartmentViewModel Get(string id)
        {
            return _mapper.Map<DepartmentViewModel>(_unitOfWork.DepartmentRepository.GetById(id));
        }

        public void Update(DepartmentViewModel departmentViewModel)
        {
            var departmentModel = _unitOfWork.DepartmentRepository.GetById(departmentViewModel.Id);
            _mapper.Map(departmentViewModel, departmentModel);

            _unitOfWork.DepartmentRepository.Update(departmentModel);
            _unitOfWork.Save();
        }

        public void Delete(DepartmentViewModel departmentViewModel)
        {
            departmentViewModel.IsDeleted = true;
            Update(departmentViewModel);
        }

        public void ReassignUsers(string deletingDepartmentId, string departmentIdForUsers)
        {
            var deletingDepartment = _unitOfWork.DepartmentRepository.GetById(deletingDepartmentId);
            var departmentForUsers = _unitOfWork.DepartmentRepository.GetById(departmentIdForUsers);

            foreach (var userModel in deletingDepartment.Users)
            {
                departmentForUsers.Users.Add(userModel);
            }

            deletingDepartment.Users = null;
            _unitOfWork.Save();
        }

        public void ReassignStocks(string deletingDepartmentId, string departmentIdForStocks)
        {
            var deletingDepartment = _unitOfWork.DepartmentRepository.GetById(deletingDepartmentId);
            var departmentForStocks = _unitOfWork.DepartmentRepository.GetById(departmentIdForStocks);

            foreach (var stockModel in deletingDepartment.Stocks)
            {
                departmentForStocks.Stocks.Add(stockModel);
            }

            deletingDepartment.Stocks = null;
            _unitOfWork.Save();
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
            }

            _disposed = true;
        }
    }
}
