using MediatR;
using Microsoft.AspNetCore.Mvc;
using StoreApp.Application.UseCases.CategoryUseCase.Query.GetList;
using StoreApp.Application.UseCases.CategoryUseCase.Query.GetOne;
using StoreApp.Application.UseCases.OrderUseCase.Command.Create;
using StoreApp.Application.UseCases.OrderUseCase.Command.Update;
using StoreApp.Application.UseCases.OrderUseCase.Query.GetList;
using StoreApp.Application.UseCases.OrderUseCase.Query.GetOne;
using StoreApp.Application.UseCases.OrderUseCase.Query.Search;
using StoreApp.Core.ValueObject;

namespace StoreApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController(IMediator mediator) : Controller
    {
        // get order by id
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var cmd = new GetOrderQuery(Id: id);
            var result = await mediator.Send(cmd);
            return Ok(result);
        }

        // get list of orders
        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] GetListOrderQuery query)
        {
            var result = await mediator.Send(query);
            return Ok(result);
        }

        // search orders
        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string? keyword)
        {
            var query = new SearchOrderQuery(keyword);
            var result = await mediator.Send(query);
            return Ok(result);
        }

        // create new order
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateOrderCommand cmd)
        {
            var result = await mediator.Send(cmd);
            return Ok(result);
        }

        // update existing order
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateOrderCommand cmd)
        {
            if (id != cmd.Id)
            {
                return BadRequest("ID in URL does not match ID in body.");
            }
            var result = await mediator.Send(cmd);
            return Ok(result);
        }


    }
}
