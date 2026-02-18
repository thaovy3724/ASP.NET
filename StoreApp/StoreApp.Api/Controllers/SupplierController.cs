using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoreApp.Application.UseCases.CustomerUseCase.Query.Search;
using StoreApp.Application.UseCases.SupplierUseCase.Command.Create;
using StoreApp.Application.UseCases.SupplierUseCase.Command.Remove;
using StoreApp.Application.UseCases.SupplierUseCase.Command.Update;
using StoreApp.Application.UseCases.SupplierUseCase.Query.GetList;
using StoreApp.Application.UseCases.SupplierUseCase.Query.GetOne;
using StoreApp.Application.UseCases.SupplierUseCase.Query.Search;
using System.Threading.Tasks;

namespace StoreApp.Api.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/admin/[controller]")]
    [ApiController]
    public class SupplierController(IMediator mediator) : Controller
    {
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var cmd = new GetSupplierQuery(id);
            var result = await mediator.Send(cmd);
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            var cmd = new GetListSupplierQuery();
            var result = await mediator.Send(cmd);
            return Ok(result);
        }
        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] GetListSupplierBySearchQuery cmd)
        {
            var result = await mediator.Send(cmd);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSupplierCommand cmd) {
            var result = await mediator.Send(cmd);
            return Ok(result);
        }
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateSupplierCommand cmd) {
            cmd = cmd with { Id = id };
            var result = await mediator.Send(cmd);
            return Ok(result);
        }
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Remove(Guid id)
        {
            var cmd = new RemoveSupplierCommand(Id : id);
            var result = await mediator.Send(cmd);
            return Ok(result);
        }
    }
}
