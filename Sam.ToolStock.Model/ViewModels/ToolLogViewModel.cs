using System;
using System.ComponentModel.DataAnnotations;
using FluentValidation.Attributes;
using Sam.ToolStock.Common;

namespace Sam.ToolStock.Model.ViewModels
{
    [Validator(typeof(ToolLogViewModel))]
    public class ToolLogViewModel
    {
        public string Id { get; set; }
        [Display(Name = "Status", ResourceType = typeof(Resources.Resource))]
        public Statuses Status { get; set; }
        [Display(Name = "Date", ResourceType = typeof(Resources.Resource))]
        public DateTime Date { get; set; }

        public string ToolId { get; set; }
        public string StockId { get; set; }
        public string UserId { get; set; }
    }
}
