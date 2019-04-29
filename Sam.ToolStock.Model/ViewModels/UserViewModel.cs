using System.Collections.Generic;
using Sam.ToolStock.Model.Models;

namespace Sam.ToolStock.Model.ViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }
        public string Patronymic { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public bool IsDeleted { get; set; }

        public string Role { get; set; }
        public IEnumerable<Role> Roles { get; set; }

        public string DepartmentId { get; set; }
        public IEnumerable<DepartmentViewModel> Departments { get; set; }

        public string StockId { get; set; }
        public IEnumerable<Stock> Stocks { get; set; }
    }
}
