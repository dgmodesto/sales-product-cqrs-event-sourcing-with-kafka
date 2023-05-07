using SaleProduct.Common.DTOs;
using SaleProduct.Query.Domain.Entities;

namespace SaleProduct.Query.Api.DTOs
{
    public class OrderLookupResponse : BaseResponse
    {
        public List<OrderEntity> Orders { get; set; }
    }
}
