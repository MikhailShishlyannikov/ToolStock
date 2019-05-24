using System;
using FluentValidation;
using Sam.ToolStock.Model.ViewModels;

namespace Sam.ToolStock.Model.Validation
{
    public class IssueToolViewModelValidator : AbstractValidator<IssueToolViewModel>
    {
        public IssueToolViewModelValidator()
        {
            RuleFor(ta => ta.UserId)
                .NotEqual(new Guid().ToString())
                .WithName(Resources.Resource.User)
                .WithMessage(@Resources.Resource.YouDidNotChooseUser);

            RuleFor(ta => ta.Amount)
                .GreaterThan(0)
                .LessThanOrEqualTo(ta => ta.MaxAmount)
                .WithName(Resources.Resource.Amount);
        }
    }
}
