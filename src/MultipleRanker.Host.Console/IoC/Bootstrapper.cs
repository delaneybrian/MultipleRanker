using Autofac;
using MultipleRanker.Host.Console.IoC;

namespace MultipleRanker.Host.Console
{
    internal static class Bootstrapper
    {
        internal static IContainer Bootstrap()
        {
            var builder = new ContainerBuilder();

            builder
                .RegisterModule(new MediatorModule());

            builder
                .RegisterModule(new InfrastructureModule());

            builder
                .RegisterModule(new MessagingModule());

            return builder.Build();
        }
    }
}
