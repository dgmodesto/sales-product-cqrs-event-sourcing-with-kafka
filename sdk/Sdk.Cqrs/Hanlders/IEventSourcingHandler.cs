using Sdk.Cqrs.Domain;

namespace Sdk.Cqrs.Hanlders;

public interface IEventSourcingHandler<T>
{
    Task SaveAsync(AggregateRoot aggregate);
    Task<T> GetByIdAsync(Guid aggregateId);
    Task RepublishEventsAsync();
}
