using FluentValidation;
using Sam.ToolStock.Common.Constants;
using Sam.ToolStock.Model.ViewModels;

namespace Sam.ToolStock.Model.Validation
{
    public class ChangePasswordViewModelValidator : AbstractValidator<ChangePasswordViewModel>
    {
        public ChangePasswordViewModelValidator()
        {
            RuleFor(cpvm => cpvm.OldPassword)
                .NotEmpty()
                .MinimumLength(ConfigEntityFramework.MinLengthOfUserPassword)
                .WithName(Resources.Resource.OldPassword);

            RuleFor(cpvm => cpvm.NewPassword)
                .NotEmpty()
                .MinimumLength(ConfigEntityFramework.MinLengthOfUserPassword)
                .WithName(Resources.Resource.NewPassword);

            RuleFor(cpvm => cpvm.ConfirmPassword)
                .NotEmpty()
                .Equal(cpvm => cpvm.NewPassword)
                .MinimumLength(ConfigEntityFramework.MinLengthOfUserPassword)
                .WithName(Resources.Resource.ConfirmPassword);

        }
    }
}
