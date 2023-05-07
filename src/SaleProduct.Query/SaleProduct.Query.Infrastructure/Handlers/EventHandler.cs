using SaleProduct.Common.Events;
using SaleProduct.Query.Domain.Entities;
using SaleProduct.Query.Domain.Repositories;

namespace SaleProduct.Query.Infrastructure.Handlers;

public class EventHandler : IEventHandler
{
    private readonly IOrderRepository _orderRepository;

    public EventHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task On(OrderCreatedEvent @event)
    {
        var order = new OrderEntity
        {
            OrderId = @event.Id,
            Vendor = @event.Vendor,
            Description = @event.Description
        };

        await _orderRepository.CreateAsync(order);
    }
}
