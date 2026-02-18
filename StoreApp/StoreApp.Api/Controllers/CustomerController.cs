using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoreApp.Application.UseCases.CustomerUseCase.Query.GetList;
using StoreApp.Application.UseCases.CustomerUseCase.Query.Search;
using System.Threading.Tasks;

namespace StoreApp.Api.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/admin/[controller]")]
    [ApiController]
    public class CustomerController(IMediator mediator) : Controller
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var cmd = new GetListCustomerQuery();
            var result = await mediator.Send(cmd);
            return Ok(result);
        }
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var cmd = new StoreApp.Application.UseCases.CustomerUseCase.Query.GetOne.GetCustomerQuery(id);
            var result = await mediator.Send(cmd);
            return Ok(result);
        }
        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] GetListCustomerByKeywordQuery cmd)
        {
            var result = await mediator.Send(cmd);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] StoreApp.Application.UseCases.CustomerUseCase.Command.Create.CreateCustomerCommand cmd)
        {
            var result = await mediator.Send(cmd);
            return Ok(result);
        }
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] StoreApp.Application.UseCases.CustomerUseCase.Command.Update.UpdateCustomerCommand cmd)
        {
            cmd = cmd with { Id = id };
            var result = await mediator.Send(cmd);
            return Ok(result);
        }
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Remove(Guid id)
        {
            var cmd = new StoreApp.Application.UseCases.CustomerUseCase.Command.Remove.RemoveCustomerCommand(Id:id);
            var result = await mediator.Send(cmd);
            return Ok(result);
        }
    }
}
