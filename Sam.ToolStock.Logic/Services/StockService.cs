using System.Collections.Generic;
using AutoMapper;
using Sam.ToolStock.DataProvider.Interfaces;
using Sam.ToolStock.Logic.Interfaces;
using Sam.ToolStock.Model.Models;

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

        public IEnumerable<Stock> GetAll()
        {
            return _mapper.Map<IEnumerable<Stock>>(_unitOfWork.StockRepository.GetAll());
        }

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }
    }
}
