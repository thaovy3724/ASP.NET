using MediatR;
using Microsoft.AspNetCore.Mvc;
using StoreApp.Application.UseCases.InventoryUseCase.Command.Update;
using StoreApp.Application.UseCases.InventoryUseCase.Query.GetList;
using StoreApp.Application.UseCases.InventoryUseCase.Query.GetOne;

namespace StoreApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GRNController(IMediator mediator) : Controller
    {
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var query = new GetGRNQuery(id);
            var result = await mediator.Send(query);
            return Ok(result);

        }

        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] GetListGRNQuery query)
        {
            var result = await mediator.Send(query);
            return Ok(result);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateInventoryCommand command)
        {
            command = command with { InventoryId = id };
            var result = await mediator.Send(command);
            return Ok(result);
        }

        // Complete GRN
        [HttpPut("{id:guid}/complete")]
        public async Task<IActionResult> Complete(Guid id)
        {
            var cmd = new CompleteGRNCommand(Id: id);
            var result = await mediator.Send(cmd);
            return Ok(result);
        }

        // Cancel GRN
        [HttpPut("{id:guid}/cancel")]
        public async Task<IActionResult> Cancel(Guid id)
        {
            var cmd = new CancelGRNCommand(Id: id);
            var result = await mediator.Send(cmd);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateGRNCommand cmd)
        {
            var result = await mediator.Send(cmd);
            return Ok(result);
        }
    }
}
