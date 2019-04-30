using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sam.ToolStock.Model.ViewModels
{
    public class StockViewModel
    {
        public string Id { get; set; }
        [Required]
        [StringLength(70)]
        public string Name { get; set; }

        public bool IsDeleted { get; set; }

        public string DepartmentId { get; set; }
        public DepartmentViewModel Department { get; set; }

        public IEnumerable<DepartmentViewModel> Departments { get; set; }

        public IEnumerable<UserViewModel> Users { get; set; }
        public IEnumerable<ToolViewModel> Tools { get; set; }
    }
}
