using FluentValidation.Attributes;

namespace Sam.ToolStock.Model.ViewModels
{
    [Validator(typeof(LoginViewModel))]
    public class LoginViewModel
    {
        //[Required(ErrorMessageResourceType = typeof(Resources.Resource), 
        //    ErrorMessageResourceName = "LoginEmailRequired")]
        //[Display(Name = "Email", ResourceType = typeof(Resources.Resource))]
        //[EmailAddress(ErrorMessageResourceType = typeof(Resources.Resource),
        //    ErrorMessageResourceName = "LoginEmailIsNotValid")]
        public string Email { get; set; }

        //[Required(ErrorMessageResourceType = typeof(Resources.Resource),
        //    ErrorMessageResourceName = "LoginPasswordRequired")]
        //[DataType(DataType.Password)]
        public string Password { get; set; }

        //[Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}
