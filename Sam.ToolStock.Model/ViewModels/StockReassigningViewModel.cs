using System.Collections.Generic;

namespace Sam.ToolStock.Model.ViewModels
{
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
