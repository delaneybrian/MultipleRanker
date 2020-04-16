using Autofac;

namespace MultipleRanker.Host
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
