using MediatR;
using Microsoft.AspNetCore.Mvc;
using StoreApp.Application.UseCases.PaymentUseCase.Query.GetList;
using StoreApp.Application.UseCases.PaymentUseCase.Query.GetOne;
using StoreApp.Application.UseCases.PaymentUseCase.Query.Search;

namespace StoreApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController(IMediator mediator) : Controller
    {
        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] GetListPaymentQuery query)
        {
            var result = await mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var cmd = new GetPaymentQuery(Id: id);
            var result = await mediator.Send(cmd);
            return Ok(result);
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string keyword)
        {
            var query = new SearchPaymentQuery(keyword);
            var result = await mediator.Send(query);
            return Ok(result);
        }
    }
}
