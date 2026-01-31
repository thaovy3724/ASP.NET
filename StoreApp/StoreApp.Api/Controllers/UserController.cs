using MediatR;
using Microsoft.AspNetCore.Mvc;
using StoreApp.Application.UseCases.UserUseCase.Command.Create;
using StoreApp.Application.UseCases.UserUseCase.Command.Remove;
using StoreApp.Application.UseCases.UserUseCase.Command.Update;
using StoreApp.Application.UseCases.UserUseCase.Query.GetList;
using StoreApp.Application.UseCases.UserUseCase.Query.GetOne;
using StoreApp.Application.UseCases.UserUseCase.Query.Search;
using System.Threading.Tasks;

namespace StoreApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IMediator mediator) : Controller
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var cmd = new GetListUserQuery();
            var result = await mediator.Send(cmd);
            return Ok(result);
        }
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var cmd = new GetUserQuery(Id: id);
            var result = await mediator.Send(cmd);
            return Ok(result);
        }
        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] GetListUserByKeywordQuery cmd)
        {
            var result = await mediator.Send(cmd);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserCommand cmd)
        {
            var result = await mediator.Send(cmd);
            return Ok(result);
        }
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateUserCommand cmd)
        {
            cmd = cmd with { Id = id };
            var result = await mediator.Send(cmd);
            return Ok(result);
        }
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Remove(Guid id)
        {
            var cmd = new RemoveUserCommand(Id:id);
            var result = await mediator.Send(cmd);
            return Ok(result);
        }
    }
}
