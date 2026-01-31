using MediatR;
using Microsoft.AspNetCore.Mvc;
using StoreApp.Application.Results;
using StoreApp.Application.UseCases.InventoryUseCase.Command.Create;
using StoreApp.Application.UseCases.InventoryUseCase.Command.Delete;
using StoreApp.Application.UseCases.InventoryUseCase.Command.Update;
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

        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            var query = new GetListInventoryQuery();
            var result = await mediator.Send(query);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateInventoryCommand command)
        {
            var result = await mediator.Send(command);
            return Ok(result);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateInventoryCommand command)
        {
            if (id != command.InventoryId)
            {
                return BadRequest(new Result(false, "InventoryId không khớp với id trên URL"));
            }

            var result = await mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteInventoryCommand(id);
            var result = await mediator.Send(command);
            return Ok(result);
        }
    }
}
