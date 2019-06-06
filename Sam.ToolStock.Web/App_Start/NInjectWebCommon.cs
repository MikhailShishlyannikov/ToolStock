using System;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using FluentValidation.Mvc;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using Ninject;
using Ninject.Web.Common;
using Ninject.Web.Common.WebHost;
using Sam.ToolStock.IoC;
using Sam.ToolStock.Web.Factories;
using WebApiContrib.IoC.Ninject;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Sam.ToolStock.Web.NInjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethod(typeof(Sam.ToolStock.Web.NInjectWebCommon), "Stop")]

namespace Sam.ToolStock.Web
{
        public static class NInjectWebCommon
        {
            private static readonly Bootstrapper Bootstrapper = new Bootstrapper();

            /// <summary>
            /// Starts the application
            /// </summary>
            public static void Start()
            {
                DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
                DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
                Bootstrapper.Initialize(CreateKernel);
            }

            /// <summary>
            /// Stops the application.
            /// </summary>
            public static void Stop()
            {
                Bootstrapper.ShutDown();
            }

            /// <summary>
            /// Creates the kernel that will manage your application.
            /// </summary>
            /// <returns>The created kernel.</returns>
            private static IKernel CreateKernel()
            {
                var kernel = new StandardKernel();
                try
                {
                    kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                    kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                    RegisterServices(kernel);
                    ValidationConfiguration(kernel);
                    GlobalConfiguration.Configuration.DependencyResolver = new NinjectResolver(kernel);
                return kernel;
                }
                catch
                {
                    kernel.Dispose();
                    throw;
                }
            }
            /// <summary>
            /// Load your modules or register your services here!
            /// </summary>
            /// <param name="kernel">The kernel.</param>
            private static void RegisterServices(IKernel kernel)
            {
                kernel.Load(Assembly.GetExecutingAssembly());
                DependencyResolver.SetResolver(new NInjectDependencyResolver(kernel));
            }

            private static void ValidationConfiguration(IKernel kernel)
            {
                ValidatorFactory validatorFactory = new ValidatorFactory(kernel);
                FluentValidationModelValidatorProvider.Configure(x => x.ValidatorFactory = validatorFactory);
                DataAnnotationsModelValidatorProvider.AddImplicitRequiredAttributeForValueTypes = false;
            }
    }
}