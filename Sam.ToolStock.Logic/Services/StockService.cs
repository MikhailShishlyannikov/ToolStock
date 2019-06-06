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
    public class StockService : IStockService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public StockService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public void Create(StockViewModel stockViewModel)
        {
            stockViewModel.Id = Guid.NewGuid().ToString();
            _unitOfWork.StockRepository.Create(_mapper.Map<StockViewModel, StockModel>(stockViewModel));
            _unitOfWork.Save();
        }

        public IEnumerable<StockViewModel> GetAll()
        {
            return _mapper.Map<IEnumerable<StockViewModel>>(_unitOfWork.StockRepository.GetAll());
        }

        public IEnumerable<StockViewModel> GetAll(bool showDeleted)
        {
            if (showDeleted) return GetAll();

            return _mapper.Map<IEnumerable<StockViewModel>>(_unitOfWork.StockRepository.GetAll()
                .Where(s => s.IsDeleted == false));
        }

        public StockViewModel Get(string stockId)
        {
            var stock = _unitOfWork.StockRepository.GetById(stockId);
            var stockViewModel = _mapper.Map<StockViewModel>(stock);

            return stockViewModel;
        }

        public void Update(StockViewModel stockViewModel)
        {
            var stockModel = _unitOfWork.StockRepository.GetById(stockViewModel.Id);
            _mapper.Map(stockViewModel, stockModel);

            _unitOfWork.StockRepository.Update(stockModel);
            _unitOfWork.Save();
        }

        public void Delete(StockViewModel stock)
        {
            stock.IsDeleted = true;
            Update(stock);
        }

        public void ReassignUsers(string deletingStockId, string stockIdForUsers)
        {
            var deletingStock = _unitOfWork.StockRepository.GetById(deletingStockId);
            var stockForUsers = _unitOfWork.StockRepository.GetById(stockIdForUsers);

            foreach (var userModel in deletingStock.Users)
            {
                stockForUsers.Users.Add(userModel);
            }

            deletingStock.Users = null;
            _unitOfWork.Save();
        }

        public void ReassignTools(string deletingStockId, string stockIdForTools)
        {
            var deletingStock = _unitOfWork.StockRepository.GetById(deletingStockId);
            var stockForTools = _unitOfWork.StockRepository.GetById(stockIdForTools);

            foreach (var toolModel in deletingStock.Tools)
            {
                stockForTools.Tools.Add(toolModel);
            }

            deletingStock.Tools = null;
            _unitOfWork.Save();
        }

        public void Dispose()
        {
            //throw new System.NotImplementedException();
        }
    }
}
