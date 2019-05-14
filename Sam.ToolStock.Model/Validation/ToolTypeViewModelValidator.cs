using FluentValidation;
using Sam.ToolStock.Common.Constants;
using Sam.ToolStock.Model.ViewModels;

namespace Sam.ToolStock.Model.Validation
{
    public class ToolTypeViewModelValidator : AbstractValidator<ToolTypeViewModel>
    {
        public ToolTypeViewModelValidator()
        {
            RuleFor(ttvm => ttvm.Name)
                .NotEmpty()
                .MaximumLength(ConfigEntityFramework.MaxLengthOToolTypeName)
                .WithName(Resources.Resource.Name);
        }
    }
}
