using Sdk.Cqrs.Events;

namespace SaleProduct.Common.Events;

public class ItemAddedEvent : BaseEvent
{
    public ItemAddedEvent() : base(nameof(ItemAddedEvent))
    {
    }

    public Guid Id { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
}
