using System.Collections.Generic;

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
        public IEnumerable<RoleViewModel> Roles { get; set; }

        public string DepartmentId { get; set; }
        public IEnumerable<DepartmentViewModel> Departments { get; set; }

        public string StockId { get; set; }
        public IEnumerable<StockViewModel> Stocks { get; set; }
    }
}
