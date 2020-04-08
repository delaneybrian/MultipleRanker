using System.Reflection;
using Autofac;
using MediatR;
using Module = Autofac.Module;

namespace MultipleRanker.Host.Console
{
    internal class MediatorModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<Mediator>()
                .As<IMediator>()
                .InstancePerLifetimeScope();

            builder.Register<ServiceFactory>(context =>
            {
                var c = context.Resolve<IComponentContext>();
                return t => c.Resolve(t);
            });

            builder.RegisterAssemblyTypes(Assembly.Load("MultipleRanker.Application")).AsImplementedInterfaces(); // via assembly scan

        }
    }
}
