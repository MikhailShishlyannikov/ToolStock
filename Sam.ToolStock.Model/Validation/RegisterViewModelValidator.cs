using System;
using FluentValidation;
using Sam.ToolStock.Common.Constants;
using Sam.ToolStock.Model.ViewModels;

namespace Sam.ToolStock.Model.Validation
{
    public class RegisterViewModelValidator : AbstractValidator<RegisterViewModel>
    {
        public RegisterViewModelValidator()
        {
            RuleFor(rvm => rvm.Name)
                .NotEmpty().WithMessage(Resources.Resource.RegisterNameIsRequired)
                .MaximumLength(ConfigEntityFramework.MaxLengthOfUserInfoName)
                .WithMessage(Resources.Resource.RegisterMaxLengthName);

            RuleFor(rvm => rvm.Patronymic)
                .MaximumLength(ConfigEntityFramework.MaxLengthOfUserInfoPatronymic)
                .WithMessage(Resources.Resource.RegisterMaxLengthPatronymic);

            RuleFor(rvm => rvm.Surname)
                .NotEmpty().WithMessage(Resources.Resource.RegisterSurnameIsRequired)
                .MaximumLength(ConfigEntityFramework.MaxLengthOfUserInfoSurname)
                .WithMessage(Resources.Resource.RegisterMaxLengthSurname);

            RuleFor(rvm => rvm.Phone)
                .MaximumLength(ConfigEntityFramework.MaxLengthOfUserInfoPhone)
                .WithMessage(Resources.Resource.RegisterMaxLengthPhone);

            RuleFor(rvm => rvm.Email)
                .NotEmpty()
                .EmailAddress()
                .MaximumLength(ConfigEntityFramework.MaxLengthOfUserInfoEmail)
                .WithName(Resources.Resource.Email);

            RuleFor(rvm => rvm.Password)
                .NotEmpty()
                .MinimumLength(ConfigEntityFramework.MinLengthOfUserPassword)
                .WithName(Resources.Resource.Password);

            RuleFor(rvm => rvm.ConfirmPassword)
                .NotEmpty()
                .Equal(rvm => rvm.Password)
                .WithMessage(Resources.Resource.RegisterConfirmPasswordEqualPassword);

            RuleFor(svm => svm.DepartmentId)
                .NotEmpty()
                .NotEqual(new Guid().ToString())
                .WithMessage(Resources.Resource.YouDidNotChooseDepartment)
                .WithName(Resources.Resource.YourDepartment);
        }
    }
}
