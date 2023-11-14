using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System.Text.Json;
using System.Text;
using ThrowingStonesGame.Infrastructure.EventBus.Interfaces;

namespace ThrowingStonesGame.Infrastructure.EventBus.Service
{
    public class RabbitMQProducer : IServiceBusProducer
    {
        private readonly IEventBusConfiguration _eventBusConfiguration;
        private static IConnection? connection;

        public RabbitMQProducer(IOptions<IEventBusConfiguration> eventBusConfiguration)
        {
            _eventBusConfiguration = eventBusConfiguration.Value;
            OpenConnection();
        }

        public void Publish(object payload, IEventRoute evenRoute)
        {
            using var channel = connection.CreateModel();

            channel.ExchangeDeclare(exchange: evenRoute.Exchange, type: ExchangeType.Direct, durable: evenRoute.Durable, autoDelete: false, arguments: null);

            channel.QueueDeclare(queue: evenRoute.Queue, durable: evenRoute.Durable, autoDelete: false, arguments: null);

            channel.QueueBind(queue: evenRoute.Queue, exchange: evenRoute.Exchange, routingKey: evenRoute.RoutingKey, arguments: null);

            var properties = channel.CreateBasicProperties();

            var payloadSerializer = JsonSerializer.Serialize(payload);
            var bytes = Encoding.UTF8.GetBytes(payloadSerializer);

            channel.BasicPublish(evenRoute.Exchange, evenRoute.RoutingKey, properties, bytes);
        }

        private void OpenConnection()
        {
            if (connection == null)
            {
                connection = new ConnectionFactory()
                {
                    HostName = _eventBusConfiguration.Hostname,
                    Password = _eventBusConfiguration.Password,
                    AutomaticRecoveryEnabled = true,

                    VirtualHost = _eventBusConfiguration.VirtualHost
                }.CreateConnection();
            }
        }

    }
}