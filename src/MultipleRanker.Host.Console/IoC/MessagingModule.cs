using Autofac;
using MultipleRanker.Infrastructure.Messaging;
using MultipleRanker.Interfaces;

namespace MultipleRanker.Host.Console.IoC
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
        }
    }
}
