using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Sam.ToolStock.Common;
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
                    UserId = toolViewModel.UserId
                };
                toolViewModel.ToolLogs.Add(toolLog);

                _unitOfWork.ToolRepository.Create(_mapper.Map<ToolModel>(toolViewModel));
            }

            _unitOfWork.Save();
        }

        public IEnumerable<ToolViewModel> GetAll(bool showDeleted)
        {
            return _mapper.Map<IEnumerable<ToolViewModel>>(showDeleted
                ? _unitOfWork.ToolRepository.GetAll()
                : _unitOfWork.ToolRepository.GetWhere(t => t.IsDeleted == false));
        }

        public IEnumerable<ToolCountViewModel> GetAllToolCounts(bool showDeleted)
        {
            return GetAllToolCounts(showDeleted, null);
        }

        public IEnumerable<ToolCountViewModel> GetAllToolCounts(bool showDeleted, string stockId)
        {
            IEnumerable<ToolModel> toolModels;

            if (stockId == null)
            {
                toolModels = showDeleted
                    ? _unitOfWork.ToolRepository.GetAll()
                    : _unitOfWork.ToolRepository.GetWhere(t => t.IsDeleted == false);
            }
            else
            {
                toolModels = showDeleted
                    ? _unitOfWork.ToolRepository.GetWhere(t => t.StockId == stockId)
                    : _unitOfWork.ToolRepository.GetWhere(t => t.IsDeleted == false && t.StockId == stockId);
            }

            var toolCounts = toolModels.GroupBy(t => t.Name)
                .Select(tc => new ToolCountViewModel
                {
                    Count = tc.Count(),
                    Name = tc.Key,
                    Manufacturer = tc.First().Manufacturer,
                    ToolTypeName = tc.FirstOrDefault()?.ToolType.Name,
                    Tools = _mapper.Map<IEnumerable<ToolViewModel>>(tc.Where(t => t.Name == tc.Key))
                }).ToList();

            return toolCounts;
        }

        public IEnumerable<ToolCountViewModel> GetAllBorrowedToolCounts(bool showDeleted, string userId)
        {
            IEnumerable<ToolModel> toolModels;

                toolModels = showDeleted
                    ? _unitOfWork.ToolRepository.GetWhere(t => t.UserId == userId)
                    : _unitOfWork.ToolRepository.GetWhere(t => t.IsDeleted == false && t.UserId == userId);

            var toolCounts = toolModels.GroupBy(t => t.Name)
                .Select(tc => new ToolCountViewModel
                {
                    Count = tc.Count(),
                    Name = tc.Key,
                    Manufacturer = tc.First().Manufacturer,
                    ToolTypeName = tc.FirstOrDefault()?.ToolType.Name,
                    Tools = _mapper.Map<IEnumerable<ToolViewModel>>(tc.Where(t => t.Name == tc.Key))
                }).ToList();

            return toolCounts;
        }

        public IEnumerable<ToolViewModel> GetByName(string toolName)
        {
            return _mapper.Map<IEnumerable<ToolViewModel>>(
                _unitOfWork.ToolRepository.GetWhere(t => t.Name == toolName));
        }

        public void IssueToUser(IssueToolViewModel issueToolViewModel)
        {
            var tools = _unitOfWork.ToolRepository.GetWhere(t =>
                t.Name == issueToolViewModel.ToolName && t.StockId == issueToolViewModel.StockId &&
                t.Status == Statuses.InStock &&
                t.UserId == null).Take(issueToolViewModel.Amount);

            foreach (var tool in tools)
            {
                tool.Status = Statuses.IssuedToUser;
                tool.UserId = issueToolViewModel.UserId;

                var toolLog = new ToolLogViewModel
                {
                    Id = Guid.NewGuid().ToString(),
                    Date = DateTime.Now,
                    Status = tool.Status,
                    StockId = tool.StockId,
                    ToolId = tool.Id,
                    UserId = tool.UserId
                };

                _unitOfWork.ToolRepository.Update(tool);
                _unitOfWork.ToolLogRepository.Create(_mapper.Map<ToolLogModel>(toolLog));
            }

            _unitOfWork.Save();
        }

        public void GiveInToRepair(ActionsViewModel giveInForRepairViewModel)
        {
            var tools = _unitOfWork.ToolRepository.GetWhere(t =>
                t.Name == giveInForRepairViewModel.ToolName && t.StockId == giveInForRepairViewModel.StockId &&
                t.Status == Statuses.InStock &&
                t.UserId == null).Take(giveInForRepairViewModel.Amount);

            foreach (var tool in tools)
            {
                tool.Status = Statuses.UnderRepair;

                var toolLog = new ToolLogViewModel
                {
                    Id = Guid.NewGuid().ToString(),
                    Date = DateTime.Now,
                    Status = tool.Status,
                    StockId = tool.StockId,
                    ToolId = tool.Id,
                };

                _unitOfWork.ToolRepository.Update(tool);
                _unitOfWork.ToolLogRepository.Create(_mapper.Map<ToolLogModel>(toolLog));
            }

            _unitOfWork.Save();
        }

        public void WriteOff(ActionsViewModel writeOffViewModel)
        {
            var tools = _unitOfWork.ToolRepository.GetWhere(t =>
                t.Name == writeOffViewModel.ToolName && t.StockId == writeOffViewModel.StockId &&
                t.Status == Statuses.InStock &&
                t.UserId == null).Take(writeOffViewModel.Amount);

            foreach (var tool in tools)
            {
                tool.Status = Statuses.WrittenOff;
                tool.IsDeleted = true;

                var toolLog = new ToolLogViewModel
                {
                    Id = Guid.NewGuid().ToString(),
                    Date = DateTime.Now,
                    Status = tool.Status,
                    StockId = tool.StockId,
                    ToolId = tool.Id,
                };

                _unitOfWork.ToolRepository.Update(tool);
                _unitOfWork.ToolLogRepository.Create(_mapper.Map<ToolLogModel>(toolLog));
            }

            _unitOfWork.Save();
        }

        public void MoveToStock(MoveToStockViewModel moveToStockViewModel)
        {
            var tools = _unitOfWork.ToolRepository.GetWhere(t =>
                t.Name == moveToStockViewModel.ToolName && t.StockId == moveToStockViewModel.StockId &&
                t.Status == Statuses.InStock &&
                t.UserId == null).Take(moveToStockViewModel.Amount);

            foreach (var tool in tools)
            {
                tool.Status = Statuses.InStock;
                tool.StockId = moveToStockViewModel.TargetStockId;

                var toolLog = new ToolLogViewModel
                {
                    Id = Guid.NewGuid().ToString(),
                    Date = DateTime.Now,
                    Status = tool.Status,
                    StockId = tool.StockId,
                    ToolId = tool.Id
                };

                _unitOfWork.ToolRepository.Update(tool);
                _unitOfWork.ToolLogRepository.Create(_mapper.Map<ToolLogModel>(toolLog));
            }

            _unitOfWork.Save();
        }

        public void ReturnFromRepair(ActionsViewModel returnFromRepairViewModel)
        {
            var tools = _unitOfWork.ToolRepository.GetWhere(t =>
                t.Name == returnFromRepairViewModel.ToolName && t.StockId == returnFromRepairViewModel.StockId &&
                t.Status == Statuses.UnderRepair &&
                t.UserId == null).Take(returnFromRepairViewModel.Amount);

            foreach (var tool in tools)
            {
                tool.Status = Statuses.InStock;

                var toolLog = new ToolLogViewModel
                {
                    Id = Guid.NewGuid().ToString(),
                    Date = DateTime.Now,
                    Status = tool.Status,
                    StockId = tool.StockId,
                    ToolId = tool.Id,
                };

                _unitOfWork.ToolRepository.Update(tool);
                _unitOfWork.ToolLogRepository.Create(_mapper.Map<ToolLogModel>(toolLog));
            }

            _unitOfWork.Save();
        }

        public void ReturnFromUser(ReturnFromUserViewModel returnFromUserViewModel)
        {
            var tools = _unitOfWork.ToolRepository.GetWhere(t =>
                t.Name == returnFromUserViewModel.ToolName && t.StockId == returnFromUserViewModel.StockId &&
                t.Status == Statuses.IssuedToUser &&
                t.UserId == returnFromUserViewModel.UserId).Take(returnFromUserViewModel.Amount);

            foreach (var tool in tools)
            {
                tool.Status = Statuses.InStock;
                tool.UserId = null;

                var toolLog = new ToolLogViewModel
                {
                    Id = Guid.NewGuid().ToString(),
                    Date = DateTime.Now,
                    Status = tool.Status,
                    StockId = tool.StockId,
                    ToolId = tool.Id,
                };

                _unitOfWork.ToolRepository.Update(tool);
                _unitOfWork.ToolLogRepository.Create(_mapper.Map<ToolLogModel>(toolLog));
            }

            _unitOfWork.Save();
        }

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }
    }
}
