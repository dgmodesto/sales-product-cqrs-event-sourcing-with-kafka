using Sdk.Cqrs.Events;

namespace Sdk.Cqrs.Producers;

public interface IEventProducer
{
    Task ProduceAsync<T>(string topic, T @event) where T : BaseEvent;
}
