namespace ThrowingStonesGame.Infrastructure.EventBus.Interfaces;

public interface IEventRoute
{
    public string Queue { get; set; }
    public string RoutingKey { get; set; }
    public string Exchange { get; set; }
    public string QueueName { get; set; }
    public bool Durable { get; set; }
    public bool Persistent { get; set; }
}
