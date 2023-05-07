using Microsoft.EntityFrameworkCore;
using SaleProduct.Query.Domain.Entities;
using SaleProduct.Query.Domain.Repositories;
using SaleProduct.Query.Infrastructure.DataAccess;

namespace SaleProduct.Query.Infrastructure.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly DatabaseContextFactory _contextFactory;

    public OrderRepository(DatabaseContextFactory contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task CreateAsync(OrderEntity order)
    {
        using (DatabaseContext context = _contextFactory.CreateDbContext())
        {
            context.Orders.Add(order);
            _ = await context.SaveChangesAsync();
        }
    }

    public async Task DeleteAsync(Guid Id)
    {
        using DatabaseContext context = _contextFactory.CreateDbContext();
        var order = await GetByIdAsync(Id);

        if (order is not { }) return;

        context.Orders.Remove(order);
        await context.SaveChangesAsync();

    }

    public async Task<OrderEntity> GetByIdAsync(Guid orderId)
    {
        using DatabaseContext context = _contextFactory.CreateDbContext();
        return await context.Orders.AsNoTracking()
                        .FirstOrDefaultAsync(x => x.OrderId.Equals(orderId));
    }

    public async Task<List<OrderEntity>> ListAllAsync()
    {
        using DatabaseContext context = _contextFactory.CreateDbContext();
        return await context.Orders.AsNoTracking()
            .ToListAsync();
    }

    public async Task UpdateAsync(OrderEntity order)
    {
        using DatabaseContext context = _contextFactory.CreateDbContext();
        context.Orders.Update(order);
        await context.SaveChangesAsync();
    }
}
