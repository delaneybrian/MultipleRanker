using Autofac;
using MultipleRanker.Infrastructure.Repositories;
using MultipleRanker.Interfaces;

namespace MultipleRanker.Host.Console.IoC
{
    internal class InfrastructureModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<MongoDbRankingBoardSnapshotRepository>()
                .As<IRankingBoardSnapshotRepository>();
        }
    }
}
