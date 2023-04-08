using Sdk.Cqrs.Events;

namespace SaleProduct.Common.Events;

public class OrderCreatedEvent : BaseEvent
{
    public OrderCreatedEvent() : base(nameof(OrderCreatedEvent))
    {
    }

    public bool Active { get; set; }
    public string Vendor { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; }

}
