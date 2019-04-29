using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Sam.ToolStock.Model.Models;

namespace Sam.ToolStock.Model.ViewModels
{
    public class DepartmentViewModel
    {
        public string Id { get; set; }
        [Required]
        [StringLength(350)]
        public string Name { get; set; }
        public bool IsDeleted { get; set; }

        public IEnumerable<UserViewModel> Users { get; set; }
        public IEnumerable<Stock> Stocks { get; set; }
    }
}
