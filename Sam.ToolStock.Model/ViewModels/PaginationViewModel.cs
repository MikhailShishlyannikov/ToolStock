using System;
using System.Collections.Generic;

namespace Sam.ToolStock.Model.ViewModels
{
    public class PaginationViewModel
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages => (int)Math.Ceiling((decimal)TotalItems / PageSize);

        public IEnumerable<ToolCountViewModel> ToolCountViewModels { get; set; }
    }
}
