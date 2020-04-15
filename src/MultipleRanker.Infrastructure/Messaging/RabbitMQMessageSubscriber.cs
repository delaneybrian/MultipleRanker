using System;
using System.Collections.Generic;
using System.Text;
using MultipleRanker.Interfaces;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace MultipleRanker.Infrastructure.Messaging
{
    public class RabbitMQMessageSubscriber : IMessageSubscriber
    {
        private readonly IConnection _connection;
        private readonly IDispatcher _dispatcher;
        private readonly ICollection<string> _subscribedTo;
        private readonly ISerializer _serializer;

        private readonly string ExchangeName = "eventdrivo";

        public RabbitMQMessageSubscriber(
            ICollection<string> subscribedTo,
            IDispatcher dispatcher,
            ISerializer serializer)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            _connection = factory.CreateConnection();

            _subscribedTo = subscribedTo;

            _dispatcher = dispatcher;
            _serializer = serializer;

            Listen();
        }

        private void Listen()
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

                    object data;
                    using (var stream = new MemoryStream(message.Content))
                    {
                        data = ProtoBuf.Serializer.Deserialize(type, stream);
                    }

                    _dispatcher.DispatchMessage(data);
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
