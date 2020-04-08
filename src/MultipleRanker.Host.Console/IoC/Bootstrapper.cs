using Autofac;

namespace MultipleRanker.Host.Console
{
    internal static class Bootstrapper
    {
        internal static IContainer Bootstrap()
        {
            var builder = new ContainerBuilder();

            builder
                .RegisterModule(new MediatorModule());

            return builder.Build();
        }
    }
}
