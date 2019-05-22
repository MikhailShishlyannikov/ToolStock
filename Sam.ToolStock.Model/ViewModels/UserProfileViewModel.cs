using FluentValidation.Attributes;

namespace Sam.ToolStock.Model.ViewModels
{
    [Validator(typeof(UserProfileViewModel))]
    public class UserProfileViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Patronymic { get; set; }

        public string Surname { get; set; }

        public string FullName => Patronymic == null ? $"{Name} {Surname}" : $"{Name} {Patronymic} {Surname}";

        public string Phone { get; set; }

        public string Email { get; set; }

        public string Role { get; set; }

        public string Department { get; set; }

        public string Stock { get; set; }
    }
}
