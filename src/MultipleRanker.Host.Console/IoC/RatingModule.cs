using System.Reflection;
using Autofac;
using MultipleRanker.Domain.Raters;
using Module = Autofac.Module;

namespace MultipleRanker.Host.IoC
{
    internal class RatingModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var ratingAssembly = Assembly.Load("MultipleRanker.Domain.Raters");

            builder
                .RegisterAssemblyTypes(ratingAssembly)
                .Where(t => typeof(IRater).IsAssignableFrom(t))
                .As<IRater>();

            builder
                .RegisterAssemblyTypes(ratingAssembly)
                .Where(t => typeof(IGenerator).IsAssignableFrom(t))
                .As<IGenerator>();
        }
    }
}
