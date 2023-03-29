using SaleProduct.Cmd.Domain.Aggregates;
using Sdk.Cqrs.Domain;
using Sdk.Cqrs.Hanlders;

namespace SaleProduct.Cmd.Infrastructure.Handlers;

public class EventSourcingHandler : IEventSourcingHandler<OrderAggregate>
{
    public Task<OrderAggregate> GetByIdAsync(Guid aggregateId)
    {
        throw new NotImplementedException();
    }

    public Task RepublishEventsAsync()
    {
        throw new NotImplementedException();
    }

    public Task SaveAsync(AggregateRoot aggregate)
    {
        throw new NotImplementedException();
    }
}
