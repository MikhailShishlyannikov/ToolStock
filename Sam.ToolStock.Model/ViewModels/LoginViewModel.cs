using System.ComponentModel.DataAnnotations;

namespace Sam.ToolStock.Model.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessageResourceType = typeof(Resources.Resource), 
            ErrorMessageResourceName = "LoginEmailRequered")]
        [Display(Name = "Email", ResourceType = typeof(Resources.Resource))]
        [EmailAddress(ErrorMessageResourceType = typeof(Resources.Resource),
            ErrorMessageResourceName = "LoginEmailIsNotValid")]
        public string Email { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
            ErrorMessageResourceName = "LoginPasswordRequered")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}
