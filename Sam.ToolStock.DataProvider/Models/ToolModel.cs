using System;
using System.Collections.Generic;
using Sam.ToolStock.Common;

namespace Sam.ToolStock.DataProvider.Models
{
    public class ToolModel
    {
        public string Id { get; set; }

        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public Statuses Status { get; set; }

        public bool IsDeleted { get; set; }

        public virtual ICollection<ToolLogModel> ToolLogs { get; set; }

        public string ToolTypeId { get; set; }
        public virtual ToolTypeModel ToolType { get; set; }

        public string StockId { get; set; }
        public virtual StockModel Stock { get; set; }

        public string UserId { get; set; }
        public virtual UserModel User { get; set; }
    }
}
