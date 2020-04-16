using Autofac;
using MultipleRanker.Infrastructure.Messaging;
using MultipleRanker.Interfaces;

namespace MultipleRanker.Host
{
    internal class MessagingModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<RabbitMQMessagePublisher>()
                .As<IMessagePublisher>()
                .SingleInstance();

            builder
                .RegisterType<RabbitMQMessageSubscriber>()
                .As<IMessageSubscriber>()
                .SingleInstance();

            builder
                .RegisterType<MediatRDispatcher>()
                .As<IMessageDispatcher>();

            builder
                .RegisterType<SystemJsonSerializer>()
                .As<ISerializer>();
        }
    }
}
