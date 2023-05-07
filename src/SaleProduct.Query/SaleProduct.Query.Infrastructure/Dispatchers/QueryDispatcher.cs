using SaleProduct.Query.Domain.Entities;
using Sdk.Cqrs.Infrastructure;
using Sdk.Cqrs.Queries;

namespace SaleProduct.Query.Infrastructure.Dispatchers;

public class QueryDispatcher : IQueryDispatcher<OrderEntity>
{

    private readonly Dictionary<Type, Func<BaseQuery, Task<List<OrderEntity>>>> _handlers = new();

    public void RegisterHandler<TQuery>(Func<TQuery, Task<List<OrderEntity>>> handler) where TQuery : BaseQuery
    {
        if(_handlers.ContainsKey(typeof(TQuery)))
        {
            throw new InvalidOperationException("You cannot register the same query handler twice");
        }
        _handlers.Add(typeof(TQuery), x => handler((TQuery)x));
    }

    public async Task<List<OrderEntity>> SendAsync(BaseQuery query)
    {
        if(_handlers.TryGetValue(query.GetType(), out Func<BaseQuery, Task<List<OrderEntity>>> handler))
        {
            return await handler(query);
        }

        throw new ArgumentNullException(nameof(handler), "No query handler was registered");
    }
}
