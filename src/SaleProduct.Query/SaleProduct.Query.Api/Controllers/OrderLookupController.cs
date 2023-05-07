
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SaleProduct.Query.Api.DTOs;
using SaleProduct.Query.Api.Queries;
using SaleProduct.Query.Domain.Entities;
using SaleProduct.Query.Infrastructure.Dispatchers;
using Sdk.Cqrs.Infrastructure;

namespace SaleProduct.Query.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class OrderLookupController : ControllerBase
{
    private readonly ILogger<OrderLookupController> _logger;
    private readonly IQueryDispatcher<OrderEntity> _queryDispatcher;

    public OrderLookupController(
        ILogger<OrderLookupController> logger, 
        IQueryDispatcher<OrderEntity> queryDispatcher)
    {
        _logger = logger;
        _queryDispatcher = queryDispatcher;
    }


    [HttpGet]
    public async Task<ActionResult> GetAllOrderAsync()
    {
        try
        {
            var orders = await _queryDispatcher.SendAsync(new FindAllOrderQuery());
            return NormalResponse(orders);
        }
        catch (Exception ex)
        {

            throw;
        }
    }

    private ActionResult NormalResponse(List<OrderEntity> orders)
    {
        if (orders == null || !orders.Any()) return NoContent();

        var count = orders.Count();

        return Ok(new OrderLookupResponse
        {
            Orders = orders,
            Message = $"Successfully retuned {count}  orders{(count > 1 ? "s" : string.Empty)}"
        });
    }
}
