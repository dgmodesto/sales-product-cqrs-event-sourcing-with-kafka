using SaleProduct.Common.Events;
using Sdk.Cqrs.Domain;

namespace SaleProduct.Cmd.Domain.Aggregates;

public  class OrderAggregate : AggregateRoot
{
    private bool _active;
    private string _vendor;
    private string _description;
    private readonly Dictionary<Guid, Tuple<string, string>> _itens = new();

    public bool Active
    {
        get => _active; set => _active = value;
    }

    public OrderAggregate(){}

    public OrderAggregate(Guid id, string vendor, string description)
    {
        RaiseEvent(new OrderCreatedEvent
        {
            Id = id,
            Active = true,
            Description = description,
            Vendor = vendor,
            CreatedAt = DateTime.Now,
        });
    }
    public void Apply(OrderCreatedEvent @event)
    {
        _id = @event.Id;
        _active = @event.Active;
        _vendor = @event.Vendor;
        _description = @event.Description;
    }

    public void EditItens(string description, double price)
    {
        if(!_active)
        {
            throw new InvalidOperationException("You cannot edit the item of an inactive order!");
        }

        if(string.IsNullOrEmpty(description))
        {
            throw new InvalidOperationException($"The value of {nameof(description)} cannot be null or empty. Please, provide a valid { nameof(description)}.");
        }

        if(price < 0)
        {
            throw new InvalidOperationException($"The value of {nameof(price)} cannot be less than zero. Please, provide a valid {nameof(price)}.");
        }

        RaiseEvent(new ItemUpdatedEvent
        {
            Id = _id,
            Description = description,
            Price = price,
        });
    }
    public void Apply(ItemUpdatedEvent @event)
    {
        _id = @event.Id;
    }


    public void AddItens(string description, double price)
    {
        if(!_active)
        {
            throw new InvalidOperationException("You cannot add a item to an inactive order!");
        }

        if(string.IsNullOrEmpty(description))
        {
            throw new InvalidOperationException($"The value of { nameof(description) } cannlt be null or empty. Provider a valid { nameof(description)}");
        }

        RaiseEvent(new ItemAddedEvent { 
            Id = _id, 
            Description = description, 
            Price = price 
        });
    }
    public void Apply(ItemAddedEvent @event)
    {
        _id = @event.Id;
        _itens.Add(@event.Id, new Tuple<string, string>(@event.Description, @event.Price.ToString("N2")));
    }
    
}
