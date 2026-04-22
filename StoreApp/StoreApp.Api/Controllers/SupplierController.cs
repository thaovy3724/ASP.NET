using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StoreApp.Application.UseCases.SupplierUseCase.Command.Create;
using StoreApp.Application.UseCases.SupplierUseCase.Command.Delete;
using StoreApp.Application.UseCases.SupplierUseCase.Command.Update;
using StoreApp.Application.UseCases.SupplierUseCase.Query.GetList;
using StoreApp.Application.UseCases.SupplierUseCase.Query.GetOne;

namespace StoreApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController(IMediator mediator) : ControllerBase
    {
        [AllowAnonymous]
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var cmd = new GetSupplierQuery(id);
            var result = await mediator.Send(cmd);
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] GetListSupplierQuery cmd)
        {
            var result = await mediator.Send(cmd);
            Response.Headers.Append("X-Pagination", JsonConvert.SerializeObject(result.MetaData));
            return Ok(result.Items);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSupplierCommand cmd) {
            var result = await mediator.Send(cmd);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateSupplierCommand cmd) {
            cmd = cmd with { Id = id };
            await mediator.Send(cmd);
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var cmd = new DeleteSupplierCommand(Id : id);
            await mediator.Send(cmd);
            return NoContent();
        }
    }
}
