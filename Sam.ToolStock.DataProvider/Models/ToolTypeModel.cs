using System;
using System.Collections.Generic;

namespace Sam.ToolStock.DataProvider.Models
{
    public class ToolTypeModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<ToolModel> Tools { get; set; }
    }
}
