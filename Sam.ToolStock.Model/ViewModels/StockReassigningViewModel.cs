using System.Collections.Generic;
using FluentValidation.Attributes;

namespace Sam.ToolStock.Model.ViewModels
{
    [Validator(typeof(StockReassigningViewModel))]
    public class StockReassigningViewModel
    {
        public string DeletingStockId { get; set; }

        public bool HasUsers { get; set; }
        public bool HasTools { get; set; }

        public IEnumerable<StockViewModel> TargetStocks { get; set; }

        public string StockIdForUsers { get; set; }
        public string StockIdForTools { get; set; }
    }
}
