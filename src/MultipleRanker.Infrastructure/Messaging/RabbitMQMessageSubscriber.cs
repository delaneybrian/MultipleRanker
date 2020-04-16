using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using MediatR;
using MultipleRanker.Contracts;
using MultipleRanker.Interfaces;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace MultipleRanker.Infrastructure.Messaging
{
    public class RabbitMQMessageSubscriber : IMessageSubscriber
    {
        private readonly IConnection _connection;
        private readonly IMessageDispatcher _messageDispatcher;
        private readonly ICollection<string> _subscribedTo;
        private readonly ISerializer _serializer;

        private readonly string ExchangeName = "multipleranker";

        public RabbitMQMessageSubscriber(
            ICollection<string> subscribedTo,
            IMessageDispatcher messageDispatcher,
            ISerializer serializer)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            _connection = factory.CreateConnection();

            _subscribedTo = subscribedTo;

            _messageDispatcher = messageDispatcher;
            _serializer = serializer;
        }

        public void Start(CancellationToken cancellationToken)
        {
            using (var channel = _connection.CreateModel())
            {
                channel.ExchangeDeclare(exchange: ExchangeName,
                    type: "direct");

                var queueName = channel.QueueDeclare().QueueName;

                foreach (var subscribeTo in _subscribedTo)
                {
                    channel.QueueBind(queue: queueName,
                        exchange: ExchangeName,
                        routingKey: subscribeTo);
                }

                var consumer = new EventingBasicConsumer(channel);

                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body;
                    var messageString = Encoding.UTF8.GetString(body);

                    var message = _serializer.Deserialize<Message>(messageString);

                    var type = Type.GetType(message.AssemblyQualifiedName);

                    var messageContent = Encoding.UTF8.GetString(message.Content.ToArray());

                    var obj = _serializer.Deserialize(type, messageContent) as IRequest;

                    _messageDispatcher.DispatchMessage(obj);
                };

                channel.BasicConsume(
                    queue: queueName,
                    autoAck: true,
                    consumer: consumer);

                Console.WriteLine("Consuming");
                Console.ReadKey();
            }
        }
    }
}
