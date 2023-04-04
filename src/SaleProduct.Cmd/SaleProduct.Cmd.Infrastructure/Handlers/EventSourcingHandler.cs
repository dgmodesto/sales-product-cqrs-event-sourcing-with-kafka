using SaleProduct.Cmd.Domain.Aggregates;
using Sdk.Cqrs.Domain;
using Sdk.Cqrs.Hanlders;
using Sdk.Cqrs.Infrastructure;
using Sdk.Cqrs.Producers;

namespace SaleProduct.Cmd.Infrastructure.Handlers;

public class EventSourcingHandler : IEventSourcingHandler<OrderAggregate>
{
    //TODO
    private readonly IEventStore _eventStore;
    private readonly IEventProducer _eventroducer;

    public EventSourcingHandler(
        IEventStore eventStore, 
        IEventProducer eventroducer)
    {
        _eventStore = eventStore;
        _eventroducer = eventroducer;
    }

    public async Task<OrderAggregate> GetByIdAsync(Guid aggregateId)
    {
        var aggregate = new OrderAggregate();
        var events = await _eventStore.GetEventsAsync(aggregateId);

        if (events == null || !events.Any()) return aggregate;

        aggregate.ReplayEvents(events);
        aggregate.Version = events.Select(x => x.Version).Max();

        return aggregate;
    }

    public async Task RepublishEventsAsync()
    {
        var aggregateIds = await _eventStore.GetAggregateIdAsync();

        if (aggregateIds == null || !aggregateIds.Any()) return;

        foreach ( var aggregateId in aggregateIds)
        {
            var aggregate = await GetByIdAsync(aggregateId);

            if(aggregate == null || !aggregate.Active) continue;

            var events = await _eventStore.GetEventsAsync(aggregateId);

            foreach(var @event in events)
            {
                var topic = Environment.GetEnvironmentVariable("KAFKA_TOPIC");
                await _eventroducer.ProduceAsync(topic, @event);
            }

            
        }
    }

    public async Task SaveAsync(AggregateRoot aggregate)
    {
        await _eventStore.SaveEventAsync(aggregate.Id, aggregate.GetUnCommittedChanges(), aggregate.Version);
        aggregate.MarkchangeAsCommitted();
    }
}
