namespace ThrowingStonesGame.Infrastructure.EventBus.Interfaces;

public interface IServiceBusProducer
{
    void Publish(object payload, IEventRoute evenRoute);
}
