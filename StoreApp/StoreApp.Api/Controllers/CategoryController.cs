using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoreApp.Application.UseCases.CategoryUseCase.Command.Create;
using StoreApp.Application.UseCases.CategoryUseCase.Command.Delete;
using StoreApp.Application.UseCases.CategoryUseCase.Command.Update;
using StoreApp.Application.UseCases.CategoryUseCase.Query.GetList;
using StoreApp.Application.UseCases.CategoryUseCase.Query.GetOne;
using StoreApp.Application.UseCases.CategoryUseCase.Query.Search;

namespace StoreApp.Api.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/admin/[controller]")]
    [ApiController]
    public class CategoryController(IMediator mediator) : Controller
    {
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
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

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] SearchCategoryQuery cmd)
        {
            var result = await mediator.Send(cmd);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCategoryCommand cmd)
        {
            var result = await mediator.Send(cmd);
            return Ok(result);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateCategoryCommand cmd)
        {
            cmd = cmd with { Id = id };
            var result = await mediator.Send(cmd);
            return Ok(result);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Remove(Guid id)
        {
            var cmd = new DeleteCategoryCommand ( Id : id );
            var result = await mediator.Send(cmd);
            return Ok(result);
        }
    }
}
