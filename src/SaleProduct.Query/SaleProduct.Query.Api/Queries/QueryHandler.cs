using SaleProduct.Query.Domain.Entities;
using SaleProduct.Query.Domain.Repositories;

namespace SaleProduct.Query.Api.Queries;

public class QueryHandler : IQueryHandler
{
    /*
     The QueryHandler clas it the concrete colleague class (and the IQueryHandler interface the Colleague or Abstract Colleague)
     that handler queries by invoking the relevant repository method to obtain one or more orders records
     from the read database, and once retrivied, maps it to a list of OrderEntity that is returned to the controller by the mediator.

     Query objects are often classes with no properties or fields, yet the name of a query object should always clearly express its intent, for example: FindAllOrderAsync
     */

    private readonly IOrderRepository _orderRepository;

    public QueryHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<List<OrderEntity>> HandleAsync(FindAllOrderQuery query)
    {
        return await _orderRepository.ListAllAsync();
    }
}
