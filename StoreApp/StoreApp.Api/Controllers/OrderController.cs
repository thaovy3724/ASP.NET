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
        //get order by id
        // staff xem order
        [Authorize(Roles = "Staff,Customer")]
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var cmd = new GetOrderQuery(Id: id);
            var result = await mediator.Send(cmd);

            // Nếu là customer thì chỉ được xem order của chính mình
            if (User.IsInRole("Customer"))
            {
                var customerIdText = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (!Guid.TryParse(customerIdText, out var customerId))
                {
                    return Forbid();
                }

                if (result.CustomerId != customerId)
                {
                    return Forbid();
                }
            }

            return Ok(result);
        }

        // staff xem tất cả, customer chỉ xem lịch sử của chính mình
        [Authorize(Roles = "Staff,Customer")]
        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] GetListOrderQuery query)
        {
            if (User.IsInRole("Customer"))
            {
                var customerIdText = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (!Guid.TryParse(customerIdText, out var customerId))
                {
                    return Forbid();
                }

                // ép customer chỉ lấy order của chính họ
                query = query with { CustomerId = customerId };
            }

            var result = await mediator.Send(query);
            Response.Headers.Append("X-Pagination", JsonConvert.SerializeObject(result.MetaData));
            return Ok(result.Items);
        }

        // customer tạo order
        [Authorize(Roles = "Customer")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateOrderCommand cmd)
        {
            var result = await mediator.Send(cmd);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        // staff duyệt order
        [Authorize(Roles = "Staff")]
        // Confirm order
        [HttpPut("{id:guid}/confirm")]
        public async Task<IActionResult> Confirm(Guid id)
        {
            // Lấy staffId từ token
            var staffId = GetCurrentUserId();
            var cmd = new ConfirmOrderCommand(id, staffId);
            var result = await mediator.Send(cmd);
            return Ok(result);
        }

        [Authorize(Roles = "Staff")]
        // Deliver order
        [HttpPut("{id:guid}/deliver")]
        public async Task<IActionResult> Deliver(Guid id)
        {
            var staffId = GetCurrentUserId();
            var cmd = new DeliverOrderCommand(id, staffId);
            var result = await mediator.Send(cmd);
            return Ok(result);
        }

        [Authorize(Roles = "Staff,Customer")]
        [HttpPut("{id:guid}/cancel")]
        public async Task<IActionResult> Cancel(Guid id)
        {
            if (User.IsInRole("Staff"))
            {
                var staffId = GetCurrentUserId();
                var cmd = new CancelOrderCommand(id, staffId);
                var result = await mediator.Send(cmd);
                return Ok(result);
            }

            // Customer được huỷ đơn của chính mình
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

            var cancelCmd = new CancelOrderCommand(id, null);
            var cancelResult = await mediator.Send(cancelCmd);
            return Ok(cancelResult);
        }

        // This endpoint will be called by VNPAY after payment is completed 
        [AllowAnonymous]
        // Pay order (VNPAY)
        [HttpGet("payment-callback")]
        public async Task<IActionResult> PaymentCallback([FromQuery] PaymentCallbackCommand cmd)
        {
            var result = await mediator.Send(cmd);
            string frontendUrl = "https://localhost:7235";  // đổi thành URL của frontend
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
