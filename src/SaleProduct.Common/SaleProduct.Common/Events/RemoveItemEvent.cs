using Sdk.Cqrs.Events;

namespace SaleProduct.Common.Events;

public class RemoveItemEvent : BaseEvent
{

    public RemoveItemEvent() : base(nameof(RemoveItemEvent))
    {
    }

    public Guid ItemId;

}
