﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        public IEnumerable<StockViewModel> Stocks { get; set; }
    }
}
