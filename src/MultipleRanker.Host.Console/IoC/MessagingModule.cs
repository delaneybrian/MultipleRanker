using System.Collections.Generic;
using Autofac;
using MultipleRanker.Contracts.Messages;
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
                .WithParameter(
                    "subscribedTo", 
                    new List<string>
                    {
                        typeof(CreateRatingBoard).FullName,
                        typeof(AddParticipantToRatingBoard).FullName,
                        typeof(GenerateRatingsForRatingBoard).FullName,
                        typeof(MatchUpCompleted).FullName
                    }.ToArray())
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
