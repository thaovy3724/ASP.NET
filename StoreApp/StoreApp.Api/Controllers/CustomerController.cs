using MediatR;
using Microsoft.AspNetCore.Mvc;
using StoreApp.Application.UseCases.CustomerUseCase.Query.GetList;
using StoreApp.Application.UseCases.CustomerUseCase.Query.Search;

namespace StoreApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController(IMediator mediator) : Controller
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            var cmd = new GetListCustomerQuery();
            var result = mediator.Send(cmd).Result;
            return Ok(result);
        }
        [HttpGet("{id:guid}")]
        public IActionResult GetById(Guid id)
        {
            var cmd = new StoreApp.Application.UseCases.CustomerUseCase.Query.GetOne.GetCustomerQuery(id);
            var result = mediator.Send(cmd).Result;
            return Ok(result);
        }
        [HttpGet("search")]
        public IActionResult Search([FromQuery] GetListCustomerByKeywordQuery cmd)
        {
            var result = mediator.Send(cmd).Result;
            return Ok(result);
        }
        [HttpPost]
        public IActionResult Create([FromBody] StoreApp.Application.UseCases.CustomerUseCase.Command.Create.CreateCustomerCommand cmd)
        {
            var result = mediator.Send(cmd).Result;
            return Ok(result);
        }
        [HttpPut("{id:guid}")]
        public IActionResult Update(Guid id, [FromBody] StoreApp.Application.UseCases.CustomerUseCase.Command.Update.UpdateCustomerCommand cmd)
        {
            cmd = cmd with { Id = id };
            var result = mediator.Send(cmd).Result;
            return Ok(result);
        }
        [HttpDelete("{id:guid}")]
        public IActionResult Remove(Guid id)
        {
            var cmd = new StoreApp.Application.UseCases.CustomerUseCase.Command.Remove.RemoveCustomerCommand(id);
            var result = mediator.Send(cmd).Result;
            return Ok(result);
        }
    }
}
