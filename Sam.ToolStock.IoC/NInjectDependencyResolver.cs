using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Ninject;

namespace Sam.ToolStock.IoC
{
    public class NInjectDependencyResolver : IDependencyResolver
    {
        private readonly IKernel _kernel;

        public NInjectDependencyResolver(IKernel kernel)
        {
            _kernel = kernel;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return _kernel.Get(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {

            _kernel.Load(new DataProvider.InjectorModule(), new Logic.InjectorModule());
        }
    }
}
