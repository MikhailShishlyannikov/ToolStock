using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FluentValidation.Attributes;

namespace Sam.ToolStock.Model.ViewModels
{
    [Validator(typeof(DepartmentViewModel))]
    public class DepartmentViewModel
    {
        public string Id { get; set; }
        
        [Display(Name = "Name", ResourceType = typeof(Resources.Resource))]
        public string Name { get; set; }

        public bool IsDeleted { get; set; }

        public IEnumerable<UserViewModel> Users { get; set; }
        public IEnumerable<StockViewModel> Stocks { get; set; }
    }
}
