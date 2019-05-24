using FluentValidation.Attributes;

namespace Sam.ToolStock.Model.ViewModels
{
    [Validator(typeof(ActionsViewModel))]
    public class ActionsViewModel
    {
        public string ToolName { get; set; }
        public string StockId { get; set; }

        public int Amount { get; set; }
        public int MaxAmount { get; set; } 
    }
}
