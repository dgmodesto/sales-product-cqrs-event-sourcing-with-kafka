namespace SaleProduct.Cmd.Api.Commands;

public interface ICommandHandler
{
    Task HandleAsync(NewOrderCommand command);
}
