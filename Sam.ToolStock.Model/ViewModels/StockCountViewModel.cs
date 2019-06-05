using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sam.ToolStock.Model.ViewModels
{
    public class StockCountViewModel
    {
        public string StockId { get; set; }
        public string Name { get; set; }
        public int ToolAmount { get; set; }
        public int InStockToolAmount { get; set; }
        public int UnderRepairToolAmount { get; set; }
        public int IssuedToUserToolAmount { get; set; }
        public int WrittenOffToolAmount { get; set; }

        public IList<UserCountViewModel> UserCountViewModels { get; set; }
    }

    public class UserCountViewModel
    {
        public string UserId { get; set; }
        public string FullName { get; set; }
        public int ToolAmount { get; set; }
    }
}
