using ThrowingStonesGame.Infrastructure.EventBus.Interfaces;

namespace ThrowingStonesGame.Infrastructure.EventBus;

public class EventRoute : IEventRoute
{
    public EventRoute(string routingKey = "", string exchange = "", string queueName = "", bool durable = true, bool persistent = true)
    {
        RoutingKey = routingKey;
        Exchange = exchange;
        QueueName = queueName;
        Durable = durable;
        Persistent = persistent;
    }

    public string? RoutingKey { get; set; }
    public string? Exchange { get; set; }
    public string? QueueName { get; set; }
    public bool Durable { get; set; }
    public bool Persistent { get; set; }
}
