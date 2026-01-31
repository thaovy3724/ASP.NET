using MediatR;
using Microsoft.AspNetCore.Mvc;
using StoreApp.Application.UseCases.PromotionUseCase.Command.Create;
using StoreApp.Application.UseCases.PromotionUseCase.Command.Update;
using StoreApp.Application.UseCases.PromotionUseCase.Query.GetList;
using StoreApp.Application.UseCases.PromotionUseCase.Query.GetOne;
using StoreApp.Application.UseCases.PromotionUseCase.Query.Search;

namespace StoreApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromotionController(IMediator mediator) : Controller
    {
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var query = new GetPromotionQuery(id); // Adjust as needed to include 'id'
            var result = await mediator.Send(query);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] GetListPromotionQuery query)
        {
            var result = await mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string keyword)
        {
            var query = new SearchPromotionQuery(keyword);
            var result = await mediator.Send(query);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePromotionCommand cmd)
        {
            var result = await mediator.Send(cmd);
            return Ok(result);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdatePromotionCommand cmd)
        {
            cmd = cmd with { Id = id };
            var result = await mediator.Send(cmd);
            return Ok(result);
        }
    }
}
