using System.Collections.Generic;
using System.Reflection;
using Autofac;
using MultipleRanker.Infrastructure.Messaging;
using MultipleRanker.Interfaces;
using MultipleRanker.RankerApi.Contracts.Events;
using Module = Autofac.Module;

namespace MultipleRanker.Host
{
    internal class MessagingModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var applicationAssembly = Assembly.Load("MultipleRanker.Application");

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
                        typeof(ParticipantAddedToRatingList).FullName,
                        typeof(GenerateRatings).FullName,
                        typeof(RatingListCreated).FullName,
                        typeof(ResultAdded).FullName
                    }.ToArray())
                .SingleInstance();

            builder
                .RegisterType<MessageDispatcher>()
                .As<IMessageDispatcher>();
            
            builder
                .RegisterAssemblyTypes(applicationAssembly)
                .Where(t => typeof(IHandler).IsAssignableFrom(t))
                .As<IHandler>();

            builder
                .RegisterType<SystemJsonSerializer>()
                .As<ISerializer>();
        }
    }
}
