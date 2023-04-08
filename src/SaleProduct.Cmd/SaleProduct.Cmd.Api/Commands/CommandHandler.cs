using SaleProduct.Cmd.Domain.Aggregates;
using Sdk.Cqrs.Hanlders;

namespace SaleProduct.Cmd.Api.Commands;

public class CommandHandler : ICommandHandler
{
    private readonly IEventSourcingHandler<OrderAggregate> _eventSourcingHandler;

    public CommandHandler(IEventSourcingHandler<OrderAggregate> eventSourcingHandler)
    {
        _eventSourcingHandler = eventSourcingHandler;
    }

    public async Task HandleAsync(NewOrderCommand command)
    {
        var aggregate = new OrderAggregate(command.Id, command.Vendor, command.Description);
        await _eventSourcingHandler.SaveAsync(aggregate);
    }

    //TODO: Todos os commands serão implementados nessa classe.
}
