using Sdk.Cqrs.Events;

namespace SaleProduct.Common.Events;

public class OrderRemovedEvent : BaseEvent
{
    public OrderRemovedEvent() : base(nameof(OrderRemovedEvent))
    {
    }
}
