using System.Collections.Generic;

namespace Sam.ToolStock.Model.ViewModels
{
    public class ReassignViewModel
    {
        public string DeletingDepartmentId { get; set; }

        public IEnumerable<DepartmentViewModel> TargetDepartments { get; set; }


        public string DepartmentIdForUsers { get; set; }
        public string DepartmentIdForStocks { get; set; }
    }
}
