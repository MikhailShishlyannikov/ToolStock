using System;
using System.Collections.Generic;
using Sam.ToolStock.Model.Models;

namespace Sam.ToolStock.Logic.Interfaces
{
    public interface IStockService : IDisposable
    {
        IEnumerable<Stock> GetAll();
    }
}
