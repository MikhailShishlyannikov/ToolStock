using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FluentValidation.Attributes;

namespace Sam.ToolStock.Model.ViewModels
{
    [Validator(typeof(StockViewModel))]
    public class StockViewModel
    {
        public string Id { get; set; }
        
        [Display(Name = "Name", ResourceType = typeof(Resources.Resource))]
        public string Name { get; set; }

        public bool IsDeleted { get; set; }

        public string DepartmentId { get; set; }
        public DepartmentViewModel Department { get; set; }

        public IEnumerable<DepartmentViewModel> Departments { get; set; }

        public IEnumerable<UserViewModel> Users { get; set; }
        public IEnumerable<ToolViewModel> Tools { get; set; }
    }
}
