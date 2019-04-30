using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sam.ToolStock.Model.ViewModels
{
    public class DepartmentReassigningViewModel
    {
        public string DeletingDepartmentId { get; set; }

        public bool HasUsers { get; set; }
        public bool HasStocks { get; set; }

        public IEnumerable<DepartmentViewModel> TargetDepartments { get; set; }

        public string DepartmentIdForUsers { get; set; }
        public string DepartmentIdForStocks { get; set; }
    }
}
