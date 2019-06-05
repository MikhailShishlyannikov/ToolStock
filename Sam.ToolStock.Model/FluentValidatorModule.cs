using System.Reflection;
using FluentValidation;
using Ninject.Modules;

namespace Sam.ToolStock.Model
{
    public class FluentValidatorModule : NinjectModule
    {
        public override void Load()
        {
            AssemblyScanner.FindValidatorsInAssembly(Assembly.GetExecutingAssembly())
                .ForEach(match => Bind(match.InterfaceType).To(match.ValidatorType));
        }
    }
}
