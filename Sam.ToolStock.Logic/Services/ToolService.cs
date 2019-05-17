using System;
using System.Collections.Generic;
using System.Linq;
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
                if (toolViewModel.UserId == new Guid().ToString()) toolViewModel.UserId = null;
                toolViewModel.ToolLogs = new List<ToolLogViewModel>();

                var toolLog = new ToolLogViewModel
                {
                    Id = Guid.NewGuid().ToString(),
                    Date = DateTime.Now,
                    Status = toolViewModel.Status,
                    StockId = toolViewModel.StockId,
                    ToolId = toolViewModel.Id,
                    UserId = toolViewModel?.UserId
                };
                toolViewModel.ToolLogs.Add(toolLog);

                _unitOfWork.ToolRepository.Create(_mapper.Map<ToolModel>(toolViewModel));
            }

            _unitOfWork.Save();
        }

        public IEnumerable<ToolViewModel> GetAll(bool showDeleted)
        {
            return _mapper.Map<IEnumerable<ToolViewModel>>(_unitOfWork.ToolRepository.GetAll());
        }

        public IEnumerable<ToolCountViewModel> GetAllToolCounts()
        {
            var toolModels = _unitOfWork.ToolRepository.GetAll();
            var toolCounts = toolModels.GroupBy(t => t.Name)
                .Select(tc => new ToolCountViewModel
                {
                    Count = tc.Count(),
                    Name = tc.Key,
                    Manufacturer = tc.First().Manufacturer,
                    ToolTypeName = tc.FirstOrDefault().ToolType.Name,
                    Tools = _mapper.Map<IEnumerable<ToolViewModel>>(tc.Where(t => t.Name == tc.Key))
                }).ToList();

            return toolCounts;
        }

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }
    }
}
