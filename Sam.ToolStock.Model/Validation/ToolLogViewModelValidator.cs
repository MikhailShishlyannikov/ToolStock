using FluentValidation;
using Sam.ToolStock.Model.ViewModels;

namespace Sam.ToolStock.Model.Validation
{
    public class ToolLogViewModelValidator : AbstractValidator<ToolLogViewModel>
    {
        public ToolLogViewModelValidator()
        {
            RuleFor(tlvm => tlvm.Date)
                .NotNull()
                .WithName(Resources.Resource.Date);

            RuleFor(tlvm => tlvm.Status)
                .NotNull()
                .WithName(Resources.Resource.Status);
        }
    }
}
