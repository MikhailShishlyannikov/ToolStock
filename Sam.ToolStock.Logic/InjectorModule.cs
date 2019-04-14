using System.Reflection;
using System.Web;
using AutoMapper;
using Microsoft.Owin.Security;
using Ninject.Modules;
using Ninject.Web.Common;
using Sam.ToolStock.Logic.Interfaces;
using Sam.ToolStock.Logic.Services;

namespace Sam.ToolStock.Logic
{
    public class InjectorModule : NinjectModule
    {
        public override void Load()
        {
            if (Kernel is null) return;

            Bind<ApplicationRoleManager>().ToSelf();

            Bind<ApplicationUserManager>().ToSelf().InRequestScope();

            Bind<ApplicationSignInManager>().ToSelf().InRequestScope();

            Bind<IAuthenticationManager>()
                .ToMethod(x => HttpContext.Current.GetOwinContext().Authentication)
                .InRequestScope();

            Bind<IMapper>().ToMethod(ctx =>
                    new Mapper(new MapperConfiguration(cfg =>
                    {
                        cfg.AddProfiles(Assembly.GetExecutingAssembly());
                    })));

            Bind<IUserService>().To<UserService>();
            Bind<IUserInfoService>().To<UserInfoService>();
            Bind<ISignInService>().To<SignInService>();
        }
    }
}
