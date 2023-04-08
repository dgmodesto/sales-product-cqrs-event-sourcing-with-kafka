using Sdk.Cqrs.Events;

namespace SaleProduct.Common.Events;

public class ItemRemovedEvent : BaseEvent
{

    public ItemRemovedEvent() : base(nameof(ItemRemovedEvent))
    {
    }

    public Guid ItemId;

}
