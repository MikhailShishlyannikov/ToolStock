using FluentValidation.Attributes;

namespace Sam.ToolStock.Model.ViewModels
{
    [Validator(typeof(RegisterViewModel))]
    public class RegisterViewModel
    {
        public string Name { get; set; }

        public string Patronymic { get; set; }

        public string Surname { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public string DepartmentId { get; set; }
    }
}
