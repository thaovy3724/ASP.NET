using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoreApp.Application.UseCases.FavoriteUseCase.Command.Add;
using StoreApp.Application.UseCases.FavoriteUseCase.Command.Remove;
using StoreApp.Application.UseCases.FavoriteUseCase.Query.GetList;
using System.Security.Claims;
using System.Text.Json;

namespace StoreApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Customer")]
    public class FavoriteController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 8)
        {
            var customerId = GetCurrentUserId();

            var result = await mediator.Send(new GetListFavoriteQuery(
                customerId,
                pageNumber,
                pageSize
            ));

            Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(result.MetaData));

            return Ok(result.Items);
        }

        [HttpPost("{productId:guid}")]
        public async Task<IActionResult> Add(Guid productId)
        {
            var customerId = GetCurrentUserId();

            var result = await mediator.Send(new AddFavoriteCommand(
                customerId,
                productId
            ));

            return CreatedAtAction(nameof(GetList), result);
        }

        [HttpDelete("{productId:guid}")]
        public async Task<IActionResult> Remove(Guid productId)
        {
            var customerId = GetCurrentUserId();

            await mediator.Send(new RemoveFavoriteCommand(
                customerId,
                productId
            ));

            return NoContent();
        }

        private Guid? GetCurrentUserId()
        {
            var idText = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (Guid.TryParse(idText, out var id))
            {
                return id;
            }

            return null;
        }
    }
}