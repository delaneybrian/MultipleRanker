using Autofac;
using MultipleRanker.Infrastructure.Repositories;
using MultipleRanker.Interfaces;

namespace MultipleRanker.Host
{
    internal class InfrastructureModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<MongoDbRatingListSnapshotRepository>()
                .As<IListSnapshotRepository>();
        }
    }
}
