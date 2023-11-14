namespace ThrowingStonesGame.Infrastructure.EventBus.Interfaces
{
    public interface IEventBusConfiguration
    {
        public string Hostname { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
        public int Port { get; set; }
        public string VirtualHost { get; set; }
    }
}
