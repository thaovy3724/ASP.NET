using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StoreApp.Application.UseCases.OrderUseCase.Command.Cancel;
using StoreApp.Application.UseCases.OrderUseCase.Command.Confirm;
using StoreApp.Application.UseCases.OrderUseCase.Command.Create;
using StoreApp.Application.UseCases.OrderUseCase.Command.Deliver;
using StoreApp.Application.UseCases.OrderUseCase.Command.PaymentCallback;
using StoreApp.Application.UseCases.OrderUseCase.Query.GetList;
using StoreApp.Application.UseCases.OrderUseCase.Query.GetOne;
using System.Security.Claims;

namespace StoreApp.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController(IMediator mediator) : Controller
    {

        // staff xem tất cả order
        [Authorize(Roles = "Staff")]
        [HttpGet("staff")]
        public async Task<IActionResult> GetListForStaff([FromQuery] GetListOrderQuery query)
        {
            var result = await mediator.Send(query);
            // thêm 1 HTTP Response Header tên là "X-Pagination"
            // bên trong chứa thông tin phân trang
            // dữ liệu được chuyển thành chuỗi JSON 
            Response.Headers.Append("X-Pagination", JsonConvert.SerializeObject(result.MetaData));
            return Ok(result.Items);
        }

        // customer chỉ xem order của chính mình
        [Authorize(Roles = "Customer")]
        [HttpGet("customer")]
        public async Task<IActionResult> GetListForCustomer([FromQuery] GetListOrderQuery query)
        {
            var customerIdText = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!Guid.TryParse(customerIdText, out var customerId))
            {
                return Forbid();
            }

            query = query with { CustomerId = customerId };

            var result = await mediator.Send(query);
            // thêm 1 HTTP Response Header tên là "X-Pagination"
            // bên trong chứa thông tin phân trang
            // dữ liệu được chuyển thành chuỗi JSON 
            Response.Headers.Append("X-Pagination", JsonConvert.SerializeObject(result.MetaData));
            return Ok(result.Items);
        }


        // staff xem chi tiết order
        [Authorize(Roles = "Staff")]
        [HttpGet("staff/{id:guid}")]
        public async Task<IActionResult> GetByIdForStaff(Guid id)
        {
            var query = new GetOrderQuery(Id: id);
            var result = await mediator.Send(query);
            return Ok(result);
        }

        // customer chỉ xem order của chính mình
        [Authorize(Roles = "Customer")]
        [HttpGet("customer/{id:guid}")]
        public async Task<IActionResult> GetByIdForCustomer(Guid id)
        {
            var query = new GetOrderQuery(Id: id);
            var result = await mediator.Send(query);

            var customerIdText = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!Guid.TryParse(customerIdText, out var customerId))
            {
                return Forbid();
            }

            if (result.CustomerId != customerId)
            {
                return Forbid();
            }

            return Ok(result);
        }

        [Authorize(Roles = "Customer")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateOrderCommand cmd)
        {
            var result = await mediator.Send(cmd);
            return CreatedAtAction(nameof(GetByIdForCustomer), new { id = result.Id }, result);
        }


        [Authorize(Roles = "Staff")]
        [HttpPut("{id:guid}/confirm")]
        public async Task<IActionResult> Confirm(Guid id)
        {
            var staffId = GetCurrentUserId();
            var cmd = new ConfirmOrderCommand(id, staffId);
            var result = await mediator.Send(cmd);
            return Ok(result);
        }

        [Authorize(Roles = "Staff")]
        [HttpPut("{id:guid}/deliver")]
        public async Task<IActionResult> Deliver(Guid id)
        {
            var staffId = GetCurrentUserId();
            var cmd = new DeliverOrderCommand(id, staffId);
            var result = await mediator.Send(cmd);
            return Ok(result);
        }

        [Authorize(Roles = "Staff")]
        [HttpPut("staff/{id:guid}/cancel")]
        public async Task<IActionResult> CancelByStaff(Guid id)
        {
            var staffId = GetCurrentUserId();
            var cmd = new CancelOrderCommand(id, staffId);
            var result = await mediator.Send(cmd);
            return Ok(result);
        }

        [Authorize(Roles = "Customer")]
        [HttpPut("customer/{id:guid}/cancel")]
        public async Task<IActionResult> CancelByCustomer(Guid id)
        {
            var query = new GetOrderQuery(Id: id);
            var order = await mediator.Send(query);

            var customerIdText = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!Guid.TryParse(customerIdText, out var customerId))
            {
                return Forbid();
            }

            if (order.CustomerId != customerId)
            {
                return Forbid();
            }

            var cmd = new CancelOrderCommand(id, null);
            var result = await mediator.Send(cmd);
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet("payment-callback")]
        public async Task<IActionResult> PaymentCallback([FromQuery] PaymentCallbackCommand cmd)
        {
            var result = await mediator.Send(cmd);
            string frontendUrl = "https://localhost:7235";

            if (result.Success)
            {
                return Redirect($"{frontendUrl}/customer/orders?payment=success&id={result.OrderId}");
            }
            else
            {
                return Redirect($"{frontendUrl}/customer/orders?payment=failed&id={result.OrderId}");
            }
        }

        private Guid? GetCurrentUserId()
        {
            var idText = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (Guid.TryParse(idText, out var guid))
            {
                return guid;
            }

            return null;
        }
    }
}