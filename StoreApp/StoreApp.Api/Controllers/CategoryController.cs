using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StoreApp.Application.UseCases.CategoryUseCase.Command.BulkDelete;
using StoreApp.Application.UseCases.CategoryUseCase.Command.Create;
using StoreApp.Application.UseCases.CategoryUseCase.Command.Delete;
using StoreApp.Application.UseCases.CategoryUseCase.Command.Update;
using StoreApp.Application.UseCases.CategoryUseCase.Query.GetList;
using StoreApp.Application.UseCases.CategoryUseCase.Query.GetOne;

namespace StoreApp.Api.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController(IMediator mediator) : ControllerBase
    {
        [AllowAnonymous]
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var cmd = new GetCategoryQuery(Id: id);
            var result = await mediator.Send(cmd);
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] GetListCategoryQuery cmd)
        {
            var result = await mediator.Send(cmd);
            Response.Headers.Append("X-Pagination", JsonConvert.SerializeObject(result.MetaData));
            return Ok(result.Items);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCategoryCommand cmd)
        {
            var result = await mediator.Send(cmd);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateCategoryCommand cmd)
        {
            cmd = cmd with { Id = id };
            await mediator.Send(cmd);
            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var cmd = new DeleteCategoryCommand (Id : id);
            await mediator.Send(cmd);
            return NoContent();
        }

        // endpoint xóa nhiều category cùng lúc
        [Authorize(Roles = "Admin")]
        [HttpPost("bulk-delete")]
        public async Task<IActionResult> BulkDelete([FromBody] BulkDeleteCategoryCommand cmd)
        {
            await mediator.Send(cmd);
            return NoContent();
        }
    }
}
