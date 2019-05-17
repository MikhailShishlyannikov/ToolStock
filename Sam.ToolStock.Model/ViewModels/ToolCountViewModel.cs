using System.Collections.Generic;

namespace Sam.ToolStock.Model.ViewModels
{
    public class ToolCountViewModel
    {
        public string Name { get; set; }
        public int Count { get; set; }
        public string Manufacturer { get; set; }
        public string ToolTypeName { get; set; }

        public IEnumerable<ToolViewModel> Tools { get; set; }
    }
}
