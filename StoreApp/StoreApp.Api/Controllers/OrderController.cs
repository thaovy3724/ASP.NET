using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StoreApp.Application.UseCases.OrderUseCase.Command.Cancel;
using StoreApp.Application.UseCases.OrderUseCase.Command.Confirm;
using StoreApp.Application.UseCases.OrderUseCase.Command.Create;
using StoreApp.Application.UseCases.OrderUseCase.Command.Deliver;
using StoreApp.Application.UseCases.OrderUseCase.Query.GetList;
using StoreApp.Application.UseCases.OrderUseCase.Query.GetOne;

namespace StoreApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController(IMediator mediator) : Controller
    {
       //get order by id
       [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
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
            Response.Headers.Append("X-Pagination", JsonConvert.SerializeObject(result.MetaData));
            return Ok(result.Items);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateOrderCommand cmd)
        {
            var result = await mediator.Send(cmd);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        // Confirm order
        [HttpPut("{id:guid}/confirm")]
        public async Task<IActionResult> Confirm(Guid id)
        {
            var cmd = new ConfirmOrderCommand(Id: id);
            var result = await mediator.Send(cmd);
            return Ok(result);
        }

        // Deliver order
        [HttpPut("{id:guid}/deliver")]
        public async Task<IActionResult> Deliver(Guid id)
        {
            var cmd = new DeliverOrderCommand(Id: id);
            var result = await mediator.Send(cmd);
            return Ok(result);
        }

        // Cancel order
        [HttpPut("{id:guid}/cancel")]
        public async Task<IActionResult> Cancel(Guid id)
        {
            var cmd = new CancelOrderCommand(Id: id);
            var result = await mediator.Send(cmd);
            return Ok(result);
        }

        // Pay order (VNPAY)
        //[HttpGet("payment-callback")]
        //public async Task<IActionResult> PaymentCallback([FromQuery] Dictionary<string, string> callbackParams)
        //{
        //    Console.WriteLine("--- BẮT ĐẦU CALLBACK ---");
        //    // 1. Nhận dữ liệu từ VNPay
        //    var response = vnPayService.PaymentExecute(callbackParams);

        //    // 2. Tạo Command gửi sang Handler
        //    // Lưu ý: response.Success sẽ là FALSE nếu khách hủy (Code 24)
        //    var command = new UpdateOrderStatusCommand(response.OrderId, response.Success, response.PaymentId);
        //    Console.WriteLine("--- BẮT ĐẦU GỌI MEDIATOR ---");
        //    // 3. Gọi Handler để xử lý Database (Cập nhật trạng thái + Hoàn kho)
        //    var result = await mediator.Send(command);
        //    Console.WriteLine("--- KẾT THÚC MEDIATOR ---");
        //    // 4. ĐIỀU HƯỚNG NGƯỜI DÙNG (Frontend)
        //    if (response.Success)
        //    {
        //        return Redirect($"http://localhost:3000/payment-success?id={response.OrderId}");
        //    }
        //    else
        //    {
        //        // Kiểm tra xem có phải khách hủy không (Mã 24)
        //        string reason = "Lỗi thanh toán";
        //        if (response.VnPayResponseCode == "24")
        //        {
        //            reason = "Bạn đã hủy giao dịch";
        //        }
        //        Console.WriteLine("Response Code: " + response.VnPayResponseCode + "Response order id" + response.OrderId);
        //        // Chuyển hướng về trang thất bại kèm lý do
        //        string encodedReason = Uri.EscapeDataString(reason);

        //        return Redirect($"http://localhost:3000/payment-failed?reason={encodedReason}&id={response.OrderId}");
        //    }
        //}
    }
}
