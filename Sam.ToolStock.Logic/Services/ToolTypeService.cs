using System;
using System.Collections.Generic;
using AutoMapper;
using Sam.ToolStock.DataProvider.Interfaces;
using Sam.ToolStock.DataProvider.Models;
using Sam.ToolStock.Logic.Interfaces;
using Sam.ToolStock.Model.ViewModels;

namespace Sam.ToolStock.Logic.Services
{
    public class ToolTypeService : IToolTypeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ToolTypeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public void Create(ToolTypeViewModel toolTypeViewModel)
        {
            toolTypeViewModel.Id = Guid.NewGuid().ToString();
            _unitOfWork.ToolTypeRepository.Create(_mapper.Map<ToolTypeModel>(toolTypeViewModel));
            _unitOfWork.Save();
        }

        public IEnumerable<ToolTypeViewModel> GetAll()
        {
            return _mapper.Map<IEnumerable<ToolTypeViewModel>>(_unitOfWork.ToolTypeRepository.GetAll());
        }

        public ToolTypeViewModel Get(string id)
        {
            return _mapper.Map<ToolTypeViewModel>(_unitOfWork.ToolTypeRepository.GetById(id));
        }

        public void Update(ToolTypeViewModel toolTypeViewModel)
        {
            var toolTypeModel = _unitOfWork.ToolTypeRepository.GetById(toolTypeViewModel.Id);
            _mapper.Map(toolTypeViewModel, toolTypeModel);

            _unitOfWork.ToolTypeRepository.Update(toolTypeModel);
            _unitOfWork.Save();
        }

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }
    }
}
