using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StoreApp.Application.UseCases.VoucherUseCase.Command.Create;
using StoreApp.Application.UseCases.VoucherUseCase.Command.Update;
using StoreApp.Application.UseCases.VoucherUseCase.Query.GetAvailable;
using StoreApp.Application.UseCases.VoucherUseCase.Query.GetList;
using StoreApp.Application.UseCases.VoucherUseCase.Query.GetOne;

namespace StoreApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoucherController(IMediator mediator) : ControllerBase
    {
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] GetListVoucherQuery query)
        {
            var result = await mediator.Send(query);
            Response.Headers.Append("X-Pagination", JsonConvert.SerializeObject(result.MetaData));
            return Ok(result.Items);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await mediator.Send(new GetVoucherQuery(id));
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateVoucherCommand command)
        {
            var result = await mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateVoucherCommand command)
        {
            command = command with { Id = id };
            await mediator.Send(command);
            return NoContent();
        }

        // API này chỉ dành cho khách hàng để lấy danh sách voucher đang có sẵn
        [Authorize(Roles = "Customer")]
        [HttpGet("available")]
        public async Task<IActionResult> GetAvailable()
        {
            var result = await mediator.Send(new GetAvailableVoucherQuery());
            return Ok(result);
        }
    }
}