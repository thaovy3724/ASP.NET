using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoreApp.Application.Exceptions;
using StoreApp.Application.UseCases.CustomerAddressUseCase.Command.Create;
using StoreApp.Application.UseCases.CustomerAddressUseCase.Command.Delete;
using StoreApp.Application.UseCases.CustomerAddressUseCase.Command.SetDefault;
using StoreApp.Application.UseCases.CustomerAddressUseCase.Command.Update;
using StoreApp.Application.UseCases.CustomerAddressUseCase.Query.GetList;
using StoreApp.Application.UseCases.CustomerAddressUseCase.Query.GetOne;
using System.Security.Claims;

namespace StoreApp.Api.Controllers
{
    [Authorize(Roles = "Customer")]
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerAddressController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            var query = new GetListCustomerAddressQuery(GetCurrentCustomerId());
            var result = await mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var query = new GetCustomerAddressQuery(id, GetCurrentCustomerId());
            var result = await mediator.Send(query);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCustomerAddressCommand command)
        {
            command = command with { CustomerId = GetCurrentCustomerId() };
            var result = await mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateCustomerAddressCommand command)
        {
            command = command with { Id = id, CustomerId = GetCurrentCustomerId() };
            await mediator.Send(command);
            return NoContent();
        }

        [HttpPut("{id:guid}/default")]
        public async Task<IActionResult> SetDefault(Guid id)
        {
            var command = new SetDefaultCustomerAddressCommand(id, GetCurrentCustomerId());
            var result = await mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteCustomerAddressCommand(id, GetCurrentCustomerId());
            await mediator.Send(command);
            return NoContent();
        }

        private Guid GetCurrentCustomerId()
        {
            var idText = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (Guid.TryParse(idText, out var id))
                return id;

            throw new BadRequestException("Không lấy được thông tin khách hàng từ token.");
        }
    }
}
