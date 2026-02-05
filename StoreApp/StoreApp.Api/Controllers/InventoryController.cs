using MediatR;
using Microsoft.AspNetCore.Mvc;
using StoreApp.Application.Results;
using StoreApp.Application.UseCases.InventoryUseCase.Command.Update;
using StoreApp.Application.UseCases.InventoryUseCase.Query.GetByProduct;
using StoreApp.Application.UseCases.InventoryUseCase.Query.GetList;
using StoreApp.Application.UseCases.InventoryUseCase.Query.GetOne;

namespace StoreApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController(IMediator mediator) : Controller
    {
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetOne(Guid id)
        {
            var query = new GetInventoryQuery(id);
            var result = await mediator.Send(query);
            return Ok(result);

        }

        [HttpGet("by-product/{productId:guid}")]
        public async Task<IActionResult> GetByProduct(Guid productId)
        {
            var query = new GetInventoryByProductQuery(productId);
            return Ok(await mediator.Send(query));
        }

        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            var query = new GetListInventoryQuery();
            var result = await mediator.Send(query);
            return Ok(result);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateInventoryCommand command)
        {
            // Không cho client gửi InventoryId trong body => server luôn lấy id từ route
            command.InventoryId = id;

            var result = await mediator.Send(command);
            return Ok(result);
        }
    }
}
