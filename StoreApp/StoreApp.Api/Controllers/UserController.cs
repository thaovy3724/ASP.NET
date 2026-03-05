using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StoreApp.Application.UseCases.UserUseCase.Command.Create;
using StoreApp.Application.UseCases.UserUseCase.Command.Delete;
using StoreApp.Application.UseCases.UserUseCase.Command.Update;
using StoreApp.Application.UseCases.UserUseCase.Query.GetList;
using StoreApp.Application.UseCases.UserUseCase.Query.GetOne;

namespace StoreApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IMediator mediator) : ControllerBase
    {
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var cmd = new GetUserQuery(Id: id);
            var result = await mediator.Send(cmd);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] GetListUserQuery cmd)
        {
            var result = await mediator.Send(cmd);
            Response.Headers.Append("X-Pagination", JsonConvert.SerializeObject(result.MetaData));
            return Ok(result.Items);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserCommand cmd)
        {
            var result = await mediator.Send(cmd);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateUserCommand cmd)
        {
            cmd = cmd with { Id = id };
            await mediator.Send(cmd);
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var cmd = new DeleteUserCommand(Id: id);
            await mediator.Send(cmd);
            return NoContent();
        }
    }
}