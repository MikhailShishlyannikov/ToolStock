using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Sam.ToolStock.Common.Constants;
using Sam.ToolStock.DataProvider.Contexts;
using Sam.ToolStock.DataProvider.Models;

namespace Sam.ToolStock.Logic.Services
{
    public class ApplicationUserManager : UserManager<UserModel>
    {
        public ApplicationUserManager(IUserStore<UserModel> store) : base(store)
        {
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            var service = new ApplicationUserManager(new UserStore<UserModel>(context.Get<ToolContext>()));
            // Configure validation logic for usernames
            service.UserValidator = new UserValidator<UserModel>(service)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            service.PasswordValidator = new PasswordValidator
            {
                RequiredLength = ConfigEntityFramework.MinLengthOfUserPassword,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };

            // Configure user lockout defaults
            service.UserLockoutEnabledByDefault = true;
            service.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            service.MaxFailedAccessAttemptsBeforeLockout = 5;

            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug it in here.
            service.RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<UserModel>
            {
                MessageFormat = "Your security code is {0}"
            });
            service.RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<UserModel>
            {
                Subject = "Security Code",
                BodyFormat = "Your security code is {0}"
            });
            service.EmailService = new EmailService();
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                service.UserTokenProvider =
                    new DataProtectorTokenProvider<UserModel>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return service;
        }
    }
}
