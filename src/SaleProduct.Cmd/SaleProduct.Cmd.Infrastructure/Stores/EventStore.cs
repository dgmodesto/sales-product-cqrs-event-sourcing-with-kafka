using SaleProduct.Cmd.Domain.Aggregates;
using Sdk.Cqrs.Domain;
using Sdk.Cqrs.Events;
using Sdk.Cqrs.Exceptions;
using Sdk.Cqrs.Infrastructure;
using Sdk.Cqrs.Producers;

namespace SaleProduct.Cmd.Infrastructure.Stores;

public class EventStore : IEventStore
{
    private readonly IEventStoreRepository _eventStoreRepository;
    private readonly IEventProducer _eventProducer;

    public EventStore(
        IEventStoreRepository eventStoreRepository, 
        IEventProducer eventProducer)
    {
        _eventStoreRepository = eventStoreRepository;
        _eventProducer = eventProducer;
    }

    public async Task<List<Guid>> GetAggregateIdAsync()
    {
        var eventStreams = await _eventStoreRepository.FindAllAsync();
        if(eventStreams == null || !eventStreams.Any())
        {
            throw new ArgumentNullException(nameof(eventStreams), "Could not retrieve event stream from the event store");
        }

        return eventStreams.Select(e => e.AggregateIndentifier).Distinct().ToList();    
    }

    public async Task<List<BaseEvent>> GetEventsAsync(Guid aggregateId)
    {
        var eventStreams = await _eventStoreRepository.FindByAggregateId(aggregateId);

        if (eventStreams != null && !eventStreams.Any())
            throw new AggregateNotFoundException("Incorrect order Id provider");

        return eventStreams.OrderBy(e => e.Version).Select(e => e.EventData).ToList();
    }

    public async Task SaveEventAsync(Guid aggregateId, IEnumerable<BaseEvent> events, int expectedVersion)
    {
        var eventStream = await _eventStoreRepository.FindByAggregateId(aggregateId);

        if (expectedVersion > 0 && eventStream[^1].Version != expectedVersion)
            throw new ConcurrencyException();

        var version = expectedVersion;

        foreach(var @event in events)
        {
            version++;
            @event.Version = version;
            var eventType = @event.GetType().Name;
            var eventModel = new EventModel
            {
                TimeStamp = DateTime.Now,
                AggregateIndentifier = aggregateId,
                AggregateType = nameof(OrderAggregate),
                Version = version,
                EventType = eventType,
                EventData = @event
            };

            await _eventStoreRepository.SaveAsync(eventModel);

            var topic = Environment.GetEnvironmentVariable("KAFKA_TOPIC");
            await _eventProducer.ProduceAsync(topic, @event);
        }
    }

}
