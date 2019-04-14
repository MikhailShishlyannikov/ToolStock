using System;
using Sam.ToolStock.Common;

namespace Sam.ToolStock.DataProvider.Models
{
    public class ToolLogModel
    {
        public string Id { get; set; }

        public Statuses Status { get; set; }
        public DateTime Date { get; set; }

        public string ToolId { get; set; }
        public virtual ToolModel Tool { get; set; }

        public string StockId { get; set; }
        public virtual StockModel Stock { get; set; }

        public string UserId { get; set; }
        public virtual UserModel User { get; set; }
    }
}
