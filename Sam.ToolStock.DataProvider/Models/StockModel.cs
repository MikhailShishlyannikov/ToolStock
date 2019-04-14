using System;
using System.Collections.Generic;

namespace Sam.ToolStock.DataProvider.Models
{
    public class StockModel
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public string DepartmentId { get; set; }
        public virtual DepartmentModel Department { get; set; }

        public virtual ICollection<UserModel> Users { get; set; }
        public virtual ICollection<ToolModel> Tools { get; set; }
        public virtual ICollection<ToolLogModel> ToolLogs { get; set; }
    }
}
