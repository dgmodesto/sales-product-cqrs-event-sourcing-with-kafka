using SaleProduct.Common.Events;

namespace SaleProduct.Query.Infrastructure.Handlers;

public interface IEventHandler
{
    /*
     - The purpose of the EventHandler is to retrieve al events for a given aggregate from Event Store
        and to invoke the ReplayEvents methods on the AggregateRoot to recreate the latest state of the Aggregate?
        - No. Is The EventSourcing Handler, and not the EventHandler, that is responsible for reatrieving all events for a given
            aggregate from the EventStore and to invoke the ReplayEvents method on the AggregateRoot to recreate the latest state of the aggregate.
     
     - The EventHandler is responsible to update the read database via the relevant repository interface once a new event was conumed from kafka.
        - Yes. Once the EventConsumer consumes an event, it will invoke the relevant handler. 
           (O(n)) method which will use the event message to build or alter the OrderEntity or ItemEntity, and update the related record in the read database.

     */

    Task On(OrderCreatedEvent @event);
}
