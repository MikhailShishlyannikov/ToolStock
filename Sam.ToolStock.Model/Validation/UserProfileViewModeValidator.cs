using FluentValidation;
using Sam.ToolStock.Common.Constants;
using Sam.ToolStock.Model.ViewModels;

namespace Sam.ToolStock.Model.Validation
{
    public class UserProfileViewModeValidator : AbstractValidator<UserProfileViewModel>
    {
        public UserProfileViewModeValidator()
        {
            RuleFor(upvm => upvm.Id)
                .NotEmpty();

            RuleFor(uvm => uvm.Name)
                .NotEmpty()
                .MaximumLength(ConfigEntityFramework.MaxLengthOfUserInfoName)
                .WithName(Resources.Resource.Name);

            RuleFor(uvm => uvm.Patronymic)
                .MaximumLength(ConfigEntityFramework.MaxLengthOfUserInfoPatronymic)
                .WithName(Resources.Resource.Patronymic);

            RuleFor(uvm => uvm.Surname)
                .NotEmpty()
                .MaximumLength(ConfigEntityFramework.MaxLengthOfUserInfoSurname)
                .WithName(Resources.Resource.Surname);

            RuleFor(uvm => uvm.Phone)
                .MaximumLength(ConfigEntityFramework.MaxLengthOfUserInfoPhone)
                .WithName(Resources.Resource.Phone);

            RuleFor(uvm => uvm.Email)
                .NotEmpty()
                .EmailAddress()
                .MaximumLength(ConfigEntityFramework.MaxLengthOfUserInfoEmail)
                .WithName(Resources.Resource.Email);

            RuleFor(uvm => uvm.Role)
                .Null()
                .WithName(Resources.Resource.Role);

            RuleFor(uvm => uvm.Department)
                .Null()
                .WithName(Resources.Resource.Department);

            RuleFor(uvm => uvm.Stock)
                .Null()
                .WithName(Resources.Resource.Stock);
        }
    }
}
