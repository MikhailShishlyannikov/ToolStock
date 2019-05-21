using FluentValidation.Attributes;

namespace Sam.ToolStock.Model.ViewModels
{
    [Validator(typeof(ChangePasswordViewModel))]
    public class ChangePasswordViewModel
    {
        public string OldPassword { get; set; }

        public string NewPassword { get; set; }

        public string ConfirmPassword { get; set; }
    }
}
