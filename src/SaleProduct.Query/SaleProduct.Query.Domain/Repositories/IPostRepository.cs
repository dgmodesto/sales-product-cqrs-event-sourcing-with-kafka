using SaleProduct.Query.Domain.Entities;

namespace SaleProduct.Query.Domain.Repositories;

public interface IOrderRepository
{
    Task CreateAsync(OrderEntity order);
    Task UpdateAsync(OrderEntity order);
    Task DeleteAsync(Guid Id);
    Task<OrderEntity> GetByIdAsync(Guid Id);
    Task<List<OrderEntity>> ListAllAsync();
}
