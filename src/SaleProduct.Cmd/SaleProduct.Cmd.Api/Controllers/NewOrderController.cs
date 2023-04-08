using Microsoft.AspNetCore.Mvc;
using SaleProduct.Cmd.Api.Commands;
using SaleProduct.Common.DTOs;
using Sdk.Cqrs.Infrastructure;

namespace SaleProduct.Cmd.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[Controller]")]
    public class NewOrderController : ControllerBase
    {
        private readonly ILogger<NewOrderController> _logger;
        private readonly ICommandDispatcher _commandDispatcher;

        public NewOrderController(
            ILogger<NewOrderController> logger, 
            ICommandDispatcher commandDispatcher)
        {
            _logger = logger;
            _commandDispatcher = commandDispatcher;
        }

        [HttpPost]
        public async Task<ActionResult> NewOrderAsync(NewOrderCommand command)
        {
            var id = Guid.NewGuid();
            try
            {
                command.Id = id;
                await _commandDispatcher.SendAsync(command);

                return StatusCode(StatusCodes.Status201Created, new NewOrderResponse
                {
                    Id = id,
                    Message = "New order creation request complete successfuly"
                });
            }
            catch(InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "Client made a bad request");
                return BadRequest(new BaseResponse
                {
                    Message = ex.Message
                });
            }
            catch (Exception ex)
            {
                const string SAFE_ERROR_MESSAGE = "Error while processing request to create a new order";
                _logger.LogError(ex, SAFE_ERROR_MESSAGE);

                return StatusCode(StatusCodes.Status500InternalServerError, new BaseResponse {
                    Message = SAFE_ERROR_MESSAGE
                });
            }
        }
    }
}
