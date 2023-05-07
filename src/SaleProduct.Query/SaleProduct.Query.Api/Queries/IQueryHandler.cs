using SaleProduct.Query.Domain.Entities;

namespace SaleProduct.Query.Api.Queries;

public interface IQueryHandler
{
    Task<List<OrderEntity>> HandleAsync(FindAllOrderQuery query);
}
