using Sam.ToolStock.Common.Constants;
using System.ComponentModel.DataAnnotations;

namespace Sam.ToolStock.Model.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [MaxLength(ConfigEntityFramework.MaxLengthOfUserInfoName)]
        public string Name { get; set; }

        [MaxLength(ConfigEntityFramework.MaxLengthOfUserInfoPatronymic)]
        public string Patronymic { get; set; }

        [Required]
        [MaxLength(ConfigEntityFramework.MaxLengthOfUserInfoSurname)]
        public string Surname { get; set; }

        [MaxLength(ConfigEntityFramework.MaxLengthOfUserInfoPhone)]
        [Phone]
        public string Phone { get; set; }

        [MaxLength(ConfigEntityFramework.MaxLengthOfUserInfoEmail)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        public string DepartmentId { get; set; }
    }
}
