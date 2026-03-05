using MediatR;
using Microsoft.AspNetCore.Mvc;
using StoreApp.Application.UseCases.CategoryUseCase.Command.Create;
using StoreApp.Application.UseCases.CategoryUseCase.Command.Delete;
using StoreApp.Application.UseCases.CategoryUseCase.Command.Update;
using StoreApp.Application.UseCases.CategoryUseCase.Query.GetList;
using StoreApp.Application.UseCases.CategoryUseCase.Query.GetOne;

namespace StoreApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController(IMediator mediator) : Controller
    {
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var cmd = new GetCategoryQuery(Id: id);
            var result = await mediator.Send(cmd);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] GetListCategoryQuery cmd)
        {
            var result = await mediator.Send(cmd);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCategoryCommand cmd)
        {
            var result = await mediator.Send(cmd);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateCategoryCommand cmd)
        {
            cmd = cmd with { Id = id };
            await mediator.Send(cmd);
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var cmd = new DeleteCategoryCommand (Id : id);
            await mediator.Send(cmd);
            return NoContent();
        }
    }
}
