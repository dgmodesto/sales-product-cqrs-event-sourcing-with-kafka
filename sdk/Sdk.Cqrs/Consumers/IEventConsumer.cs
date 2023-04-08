namespace Sdk.Cqrs.Consumers;

public interface IEventConsumer
{
    void Consume(string topic);
}
