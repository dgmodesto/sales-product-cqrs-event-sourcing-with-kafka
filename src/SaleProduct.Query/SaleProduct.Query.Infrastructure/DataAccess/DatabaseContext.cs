using Microsoft.EntityFrameworkCore;
using SaleProduct.Query.Domain.Entities;

namespace SaleProduct.Query.Infrastructure.DataAccess;

public class DatabaseContext : DbContext
{
	public DatabaseContext(DbContextOptions options) : base(options)
	{
			
	}

	public DbSet<OrderEntity> Orders { get; set; }
}
