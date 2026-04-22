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
using StoreApp.Core.ValueObject;
using System.Security.Claims;

namespace StoreApp.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController(IMediator mediator) : ControllerBase
    {

        // staff xem tất cả order
        [Authorize(Roles = "Staff")]
        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] GetListOrderQuery query)
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
        [HttpGet("history")]
        public async Task<IActionResult> GetOrderHistory([FromQuery] GetListOrderQuery query)
        {
            var customerId = GetRequiredUserId();
            query = query with { CustomerId = customerId };

            var result = await mediator.Send(query);
            // thêm 1 HTTP Response Header tên là "X-Pagination"
            // bên trong chứa thông tin phân trang
            // dữ liệu được chuyển thành chuỗi JSON 
            Response.Headers.Append("X-Pagination", JsonConvert.SerializeObject(result.MetaData));
            return Ok(result.Items);
        }


        // staff xem chi tiết order
        [Authorize(Roles = "Staff, Customer")]
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var role = GetCurrentUserRole();

            Guid? customerId = role == Role.Customer
                ? GetRequiredUserId()
                : null;

            var query = new GetOrderQuery(
                Id: id,
                CustomerId: customerId
            );

            var result = await mediator.Send(query);
            return Ok(result);
        }

        [Authorize(Roles = "Customer")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateOrderCommand cmd)
        {
            var customerId = GetRequiredUserId();
            cmd = cmd with { CustomerId = customerId };
            var result = await mediator.Send(cmd);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
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

        [Authorize(Roles = "Staff, Customer")]
        [HttpPut("{id:guid}/cancel")]
        public async Task<IActionResult> Cancel(Guid id)
        {
            var role = GetCurrentUserRole();

            Guid? staffId = role == Role.Staff
                ? GetRequiredUserId()
                : null;

            Guid? customerId = role == Role.Customer
                ? GetRequiredUserId()
                : null;

            var cmd = new CancelOrderCommand(
                Id: id,
                StaffId: staffId,
                CustomerId: customerId
            );

            var result = await mediator.Send(cmd);
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet("payment-callback")]
        public async Task<IActionResult> PaymentCallback([FromQuery] Dictionary<string, string> request)
        {
            var collections = Request.Query.ToDictionary(x => x.Key, x => x.Value.ToString());
            var command = new PaymentCallbackCommand(collections);
            var result = await mediator.Send(command);
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

        private Role? GetCurrentUserRole()
        {
            var roleText = User.FindFirstValue(ClaimTypes.Role);
            if (Enum.TryParse<Role>(roleText, out var role))
            {
                return role;
            }
            return null;
        }

        private Guid GetRequiredUserId()
        {
            var idText = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (Guid.TryParse(idText, out var guid))
            {
                return guid;
            }

            throw new UnauthorizedAccessException("Token không hợp lệ hoặc thiếu UserId.");
        }
    }
}