using System;
using FluentValidation;
using Sam.ToolStock.Common.Constants;
using Sam.ToolStock.Model.ViewModels;

namespace Sam.ToolStock.Model.Validation
{
    public class StockViewModelValidator : AbstractValidator<StockViewModel>
    {
        public StockViewModelValidator()
        {
            RuleFor(svm => svm.Name)
                .NotEmpty()
                .MaximumLength(ConfigEntityFramework.MaxLengthOfStockName)
                .WithName(Resources.Resource.Name);

            RuleFor(svm => svm.DepartmentId)
                .NotEmpty()
                .NotEqual(new Guid().ToString())
                .WithMessage(Resources.Resource.YouDidNotChooseDepartment)
                .WithName(Resources.Resource.DepartmentForNewStock);
        }
    }
}
