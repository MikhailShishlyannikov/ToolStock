using FluentValidation;
using Sam.ToolStock.Common.Constants;
using Sam.ToolStock.Model.ViewModels;

namespace Sam.ToolStock.Model.Validation
{
    public class DepartmentViewModelValidator : AbstractValidator<DepartmentViewModel>
    {
        public DepartmentViewModelValidator()
        {
            RuleFor(dvm => dvm.Name)
                .NotEmpty()
                .MaximumLength(ConfigEntityFramework.MaxLengthOfDepartmentName)
                .WithName(Resources.Resource.Name);
        }
    }
}
