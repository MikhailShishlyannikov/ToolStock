using System;
using FluentValidation;
using Sam.ToolStock.Common.Constants;
using Sam.ToolStock.Model.ViewModels;

namespace Sam.ToolStock.Model.Validation
{
    public class ToolViewModelValidator : AbstractValidator<ToolViewModel>
    {
        public ToolViewModelValidator()
        {
            RuleFor(tvm => tvm.Name)
                .NotEmpty()
                .MaximumLength(ConfigEntityFramework.MaxLengthOfToolName)
                .WithName(Resources.Resource.Name);

            RuleFor(tvm => tvm.Manufacturer)
                .MaximumLength(ConfigEntityFramework.MaxLengthOfToolManufacturer)
                .WithName(Resources.Resource.Manufacturer);

            RuleFor(tvm => tvm.Status)
                .NotNull()
                .WithName(Resources.Resource.Status);

            RuleFor(tvm => tvm.Amount)
                .GreaterThan(0)
                .WithName(Resources.Resource.Amount);

            RuleFor(tvm => tvm.ToolTypeId)
                .NotEmpty()
                .NotEqual(new Guid().ToString())
                .WithName(Resources.Resource.ToolType)
                .WithMessage(Resources.Resource.YouDidNotChooseToolType);

            RuleFor(tvm => tvm.StockId)
                .NotEmpty()
                .NotEqual(new Guid().ToString())
                .WithName(Resources.Resource.Stock)
                .WithMessage(Resources.Resource.YouDidNotChooseStock);

            RuleFor(tvm => tvm.UserId)
                .NotEmpty()
                .WithName(Resources.Resource.User);
        }
    }
}
