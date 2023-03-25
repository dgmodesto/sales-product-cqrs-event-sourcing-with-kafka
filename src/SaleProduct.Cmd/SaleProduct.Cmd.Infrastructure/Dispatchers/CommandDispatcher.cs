using Sdk.Cqrs.Commands;
using Sdk.Cqrs.Infrastructure;

namespace SaleProduct.Cmd.Infrastructure.Dispatchers;

public class CommandDispatcher : ICommandDispatcher
{
    private readonly Dictionary<Type, Func<BaseCommand, Task>> _handllers = new();


    public void RegisterHandler<T>(Func<T, Task> handler) where T : BaseCommand
    {
        if(_handllers.ContainsKey(typeof(T)))
        {
            throw new IndexOutOfRangeException("You cannot register the same command handler twice");
        }

        _handllers.Add(typeof(T), t => handler((T)t));
    }

    public async Task SendAsync(BaseCommand command)
    {
        if(_handllers.TryGetValue(command.GetType(), out Func<BaseCommand, Task> handler))
        {
            await handler(command);
        } else
        {
            throw new ArgumentNullException(nameof(handler), "No command handler was registered");
        }
    }
}
