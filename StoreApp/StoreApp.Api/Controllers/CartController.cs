using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoreApp.Application.UseCases.CartUseCase.Command.AddItem;
using StoreApp.Application.UseCases.CartUseCase.Command.Clear;
using StoreApp.Application.UseCases.CartUseCase.Command.DeleteItem;
using StoreApp.Application.UseCases.CartUseCase.Command.UpdateItem;
using StoreApp.Application.UseCases.CartUseCase.Query.GetCart;
using System.Security.Claims;

namespace StoreApp.Api.Controllers
{
    [Authorize(Roles = "Customer")]
    [Route("api/[controller]")]
    [ApiController]
    public class CartController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetCart()
        {
            var customerId = GetCurrentUserId();
            var result = await mediator.Send(new GetCartQuery(customerId));
            return Ok(result);
        }

        [HttpPost("items")]
        public async Task<IActionResult> AddItem([FromBody] AddCartItemRequest request)
        {
            var customerId = GetCurrentUserId();

            var command = new AddCartItemCommand(
                ProductId: request.ProductId,
                Quantity: request.Quantity,
                CustomerId: customerId
            );

            var result = await mediator.Send(command);
            return Ok(result);
        }

        [HttpPut("items/{productId:guid}")]
        public async Task<IActionResult> UpdateItem(Guid productId, [FromBody] UpdateCartItemRequest request)
        {
            var customerId = GetCurrentUserId();

            var command = new UpdateCartItemCommand(
                ProductId: productId,
                Quantity: request.Quantity,
                CustomerId: customerId
            );

            var result = await mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("items/{productId:guid}")]
        public async Task<IActionResult> DeleteItem(Guid productId)
        {
            var customerId = GetCurrentUserId();
            await mediator.Send(new DeleteCartItemCommand(productId, customerId));
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> Clear()
        {
            var customerId = GetCurrentUserId();
            await mediator.Send(new ClearCartCommand(customerId));
            return NoContent();
        }

        private Guid? GetCurrentUserId()
        {
            var idText = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return Guid.TryParse(idText, out var guid) ? guid : null;
        }
    }
}