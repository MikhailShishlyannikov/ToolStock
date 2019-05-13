using System;
using FluentValidation;
using Sam.ToolStock.Model.ViewModels;

namespace Sam.ToolStock.Model.Validation
{
    public class StockReassigningViewModelValidator : AbstractValidator<StockReassigningViewModel>
    {
        public StockReassigningViewModelValidator()
        {
            RuleFor(srvm => srvm.DeletingStockId)
                .NotNull();

            RuleFor(srvm => srvm.StockIdForTools)
                .NotEqual(srvm => srvm.DeletingStockId)
                .NotEqual(new Guid().ToString());

            RuleFor(srvm => srvm.StockIdForUsers)
                .NotEqual(srvm => srvm.DeletingStockId)
                .NotEqual(new Guid().ToString());
        }
    }
}
