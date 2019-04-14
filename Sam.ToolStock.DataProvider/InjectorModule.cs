using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Ninject;
using Ninject.Modules;
using Ninject.Web.Common;
using Sam.ToolStock.DataProvider.Contexts;
using Sam.ToolStock.DataProvider.Interfaces;
using Sam.ToolStock.DataProvider.Models;
using Sam.ToolStock.DataProvider.Repositories;
using Sam.ToolStock.DataProvider.UoW;

namespace Sam.ToolStock.DataProvider
{
    public class InjectorModule : NinjectModule
    {
        public override void Load()
        {
            if (Kernel is null)
                return;

            Bind<ToolContext>().ToSelf();

            Bind<IRoleStore<IdentityRole, string>>()
                .ToMethod(x => new RoleStore<IdentityRole, string, IdentityUserRole>(x.Kernel.Get<ToolContext>()))
                .InRequestScope();

            Bind<IUserStore<UserModel>>()
                .ToMethod(x => new UserStore<UserModel>(x.Kernel.Get<ToolContext>()))
                .InRequestScope();

            Bind(typeof(IRepository<>)).To(typeof(GenericRepository<>));
            Bind<IUnitOfWork>().To<UnitOfWork>();
        }
    }
}
