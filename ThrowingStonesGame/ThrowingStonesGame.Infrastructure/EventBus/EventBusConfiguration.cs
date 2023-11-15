using ThrowingStonesGame.Infrastructure.EventBus.Interfaces;

namespace ThrowingStonesGame.Infrastructure.EventBus;

public class EventBusConfiguration : IEventBusConfiguration
{
    public string Hostname { get; set; }
    public string Password { get; set; }
    public string Username { get; set; }
    public int Port { get; set; }
    public string VirtualHost { get; set; }
}
