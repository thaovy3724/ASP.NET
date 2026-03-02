using MediatR;
using Microsoft.AspNetCore.Mvc;
using StoreApp.Application.UseCases.GRNUseCase.Command.Cancel;
using StoreApp.Application.UseCases.GRNUseCase.Command.Complete;
using StoreApp.Application.UseCases.GRNUseCase.Command.Create;
using StoreApp.Application.UseCases.GRNUseCase.Query.GetList;
using StoreApp.Application.UseCases.GRNUseCase.Query.GetOne;

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

        // Complete GRN
        [HttpPut("{id:guid}/complete")]
        public async Task<IActionResult> Complete(Guid id)
        {
            var cmd = new CompleteGRNCommand(Id: id);
            await mediator.Send(cmd);
            return NoContent();
        }

        // Cancel GRN
        [HttpPut("{id:guid}/cancel")]
        public async Task<IActionResult> Cancel(Guid id)
        {
            var cmd = new CancelGRNCommand(Id: id);
            await mediator.Send(cmd);
            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateGRNCommand cmd)
        {
            var result = await mediator.Send(cmd);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }
    }
}
