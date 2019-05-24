using System;
using FluentValidation;
using Sam.ToolStock.Model.ViewModels;

namespace Sam.ToolStock.Model.Validation
{
    public class MoveToStockViewModelValidator : AbstractValidator<MoveToStockViewModel>
    {
        public MoveToStockViewModelValidator()
        {
            RuleFor(ta => ta.Amount)
                .GreaterThan(0)
                .LessThanOrEqualTo(ta => ta.MaxAmount)
                .WithName(Resources.Resource.Amount);
        }
    }
}
