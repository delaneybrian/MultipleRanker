using System;
using System.Text;
using MultipleRanker.Contracts.Messages;
using MultipleRanker.Infrastructure.Messaging;
using MultipleRanker.Interfaces;
using MultipleRanker.Messaging.Contracts;
using NUnit.Framework;
using RabbitMQ.Client;

namespace MultipleRanker.Tests.Integration
{
    [TestFixture]
    public class OffensiveDefensiveTests
    {
        public TestContext _context;

        [SetUp]
        public void SetUp() => _context = new TestContext();

        [Test]
        public void TestPublish() =>
            _context
                .PublishCreateRatingBoard();
            

        public class TestContext
        {
            private readonly IConnection _connection;
            private readonly ISerializer _serializer = new SystemJsonSerializer();

            private readonly string ExchangeName = "multipleranker";

            public TestContext()
            {
                var factory = new ConnectionFactory() { HostName = "localhost" };
                _connection = factory.CreateConnection();
            }

            public TestContext PublishCreateRatingBoard()
            {
                var correlationId = Guid.NewGuid();

                var createRatingBoardMessage = new CreateRatingBoard
                {
                    Id = Guid.NewGuid(),
                    Name = "Test Board"
                };

                Publish(createRatingBoardMessage, correlationId);

                return this;
            }

            public void Publish<T>(T content, Guid correlationId) where T : class
            {
                var message = new Message()
                {
                    Content = _serializer.Serialize(content),
                    RoutingKey = typeof(T).FullName,
                    CorrelationId = correlationId,
                    AssemblyQualifiedName = typeof(T).AssemblyQualifiedName
                };

                using (var channel = _connection.CreateModel())
                {
                    channel.ExchangeDeclare(
                        exchange: ExchangeName,
                        type: "direct");

                    var jsonMessage = _serializer.Serialize(message);

                    var body = Encoding.UTF8.GetBytes(jsonMessage);

                    channel.BasicPublish(ExchangeName, message.RoutingKey, null, body);
                }
            }
        }
    }
}
