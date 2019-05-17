using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FluentValidation.Attributes;
using Sam.ToolStock.Common;

namespace Sam.ToolStock.Model.ViewModels
{
    [Validator(typeof(ToolViewModel))]
    public class ToolViewModel
    {
        public string Id { get; set; }
        [Display(Name = "Name", ResourceType = typeof(Resources.Resource))]
        public string Name { get; set; }
        [Display(Name = "Manufacturer", ResourceType = typeof(Resources.Resource))]
        public string Manufacturer { get; set; }
        [Display(Name = "Status", ResourceType = typeof(Resources.Resource))]
        public Statuses Status { get; set; }

        public int Amount { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ICollection<ToolLogViewModel> ToolLogs { get; set; }

        public string ToolTypeId { get; set; }
        public ToolTypeViewModel ToolType { get; set; }
        public ICollection<ToolTypeViewModel> ToolTypes { get; set; }

        public string StockId { get; set; }
        public StockViewModel Stock { get; set; }
        public ICollection<StockViewModel> Stocks { get; set; }

        public string UserId { get; set; }
        public UserViewModel User { get; set; }
        public ICollection<UserViewModel> Users { get; set; }
    }
}
