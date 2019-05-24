using FluentValidation.Attributes;

namespace Sam.ToolStock.Model.ViewModels
{
    [Validator(typeof(ReturnFromUserViewModel))]
    public class ReturnFromUserViewModel
    {
        public string ToolName { get; set; }
        public string StockId { get; set; }

        public int Amount { get; set; }
        public int MaxAmount { get; set; }

        public string UserId { get; set; }
        public string UserName { get; set; }
    }
}
