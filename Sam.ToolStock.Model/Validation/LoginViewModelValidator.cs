using FluentValidation;
using Sam.ToolStock.Model.ViewModels;

namespace Sam.ToolStock.Model.Validation
{
    public class LoginViewModelValidator : AbstractValidator<LoginViewModel>
    {
        public LoginViewModelValidator()
        {
            RuleFor(lvm => lvm.Email)
                .NotEmpty().WithMessage(Resources.Resource.LoginEmailIsRequired)
                .WithName(Resources.Resource.Email)
                .EmailAddress().WithMessage(Resources.Resource.LoginEmailIsNotValid);
            RuleFor(lvm => lvm.Password)
                .NotEmpty().WithMessage(Resources.Resource.LoginPasswordIsRequired)
                .MinimumLength(6).WithMessage(Resources.Resource.LoginMinLengthPassword);
        }
    }
}
