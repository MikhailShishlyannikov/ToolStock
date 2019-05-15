using System;
using System.Collections.Generic;
using AutoMapper;
using Sam.ToolStock.DataProvider.Interfaces;
using Sam.ToolStock.DataProvider.Models;
using Sam.ToolStock.Logic.Interfaces;
using Sam.ToolStock.Model.ViewModels;

namespace Sam.ToolStock.Logic.Services
{
    public class ToolService : IToolService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ToolService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public void Create(ToolViewModel toolViewModel)
        {
            for (var i = 0; i < toolViewModel.Amount; i++)
            {
                toolViewModel.Id = Guid.NewGuid().ToString();
                toolViewModel.ToolLogs = new List<ToolLogViewModel>();

                var toolLog = new ToolLogViewModel
                {
                    Id = Guid.NewGuid().ToString(),
                    Date = DateTime.Now,
                    Status = toolViewModel.Status,
                    StockId = toolViewModel.StockId,
                    ToolId = toolViewModel.Id,
                    UserId = toolViewModel.UserId
                };
                toolViewModel.ToolLogs.Add(toolLog);

                _unitOfWork.ToolRepository.Create(_mapper.Map<ToolModel>(toolViewModel));
            }

            _unitOfWork.Save();
        }

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }
    }
}
