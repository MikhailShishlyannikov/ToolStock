using System.Collections.Generic;
using FluentValidation.Attributes;

namespace Sam.ToolStock.Model.ViewModels
{
    [Validator(typeof(MoveToStockViewModel))]
    public class MoveToStockViewModel
    {
        public string ToolName { get; set; }
        public string StockId { get; set; }

        public int Amount { get; set; }
        public int MaxAmount { get; set; }

        public string TargetStockId { get; set; }
        public IEnumerable<StockViewModel> Stocks { get; set; }
    }
}
