using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Sam.ToolStock.DataProvider.Models;

namespace Sam.ToolStock.Logic.Services
{
    public class ApplicationSignInManager : SignInManager<UserModel, string>
    {
        public ApplicationSignInManager(ApplicationUserManager userService, IAuthenticationManager authenticationManager) 
            : base(userService, authenticationManager)
        {
        }

        //public override Task<ClaimsIdentity> CreateUserIdentityAsync(ApplicationUser user)
        //{
        //    return user.GenerateUserIdentityAsync((ApplicationUserManager)UserManager);
        //}

        public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
        {
            return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
        }
    }
}
