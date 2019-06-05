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

        public string searchString { get; set; }
        public string Manufacturer { get; set; }
        public IList<string> Manufacturers { get; set; }
        public string toolTypeId { get; set; }
        public IList<ToolTypeViewModel> ToolTypes { get; set; }

        public IList<ToolCountViewModel> ToolCountViewModels { get; set; }
    }
}
