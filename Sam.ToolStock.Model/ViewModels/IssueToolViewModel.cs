using System.Collections.Generic;
using FluentValidation.Attributes;

namespace Sam.ToolStock.Model.ViewModels
{
    [Validator(typeof(IssueToolViewModel))]
    public class IssueToolViewModel
    {
        public string ToolName { get; set; }
        public string StockId { get; set; }

        public int Amount { get; set; }
        public int MaxAmount { get; set; }

        public string UserId { get; set; }
        public IEnumerable<UserViewModel> Users { get; set; }
    }
}
