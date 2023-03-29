using Sdk.Cqrs.Events;

namespace SaleProduct.Common.Events;

public class ItemUpdatedEvent : BaseEvent
{

    public ItemUpdatedEvent() : base(nameof(ItemUpdatedEvent))
    {
    }

    public Guid Id { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }

}
