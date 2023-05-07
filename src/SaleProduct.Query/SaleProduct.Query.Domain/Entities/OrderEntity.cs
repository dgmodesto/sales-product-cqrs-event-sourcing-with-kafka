using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SaleProduct.Query.Domain.Entities;

[Table("Order", Schema = "dbo")]
public class OrderEntity
{
    [Key]
    public Guid OrderId { get; set; }
    public string Vendor { get; set; }
    public string Description { get; set; }
}
