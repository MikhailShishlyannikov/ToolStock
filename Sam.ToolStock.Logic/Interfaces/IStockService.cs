using System;
using System.Collections.Generic;
using Sam.ToolStock.Model.ViewModels;

namespace Sam.ToolStock.Logic.Interfaces
{
    public interface IStockService : IDisposable
    {
        void Create(StockViewModel stockViewModel);

        IEnumerable<StockViewModel> GetAll();

        IEnumerable<StockViewModel> GetAll(bool showDeleted);

        StockViewModel Get(string stockId);

        void Update(StockViewModel stockViewModel);

        void Delete(StockViewModel stock);

        void ReassignUsers(string deletingStockId, string stockIdForUsers);

        void ReassignTools(string deletingStockId, string stockIdForTools);
    }
}
