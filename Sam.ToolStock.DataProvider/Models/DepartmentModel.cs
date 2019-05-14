using System.Collections.Generic;

namespace Sam.ToolStock.DataProvider.Models
{
    public class DepartmentModel
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public bool IsDeleted { get; set; }

        public virtual ICollection<UserModel> Users { get; set; }
        public virtual ICollection<StockModel> Stocks { get; set; }
    }
}
