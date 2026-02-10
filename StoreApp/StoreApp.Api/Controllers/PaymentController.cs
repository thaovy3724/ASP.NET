using MediatR;
using Microsoft.AspNetCore.Mvc;
using StoreApp.Application.Repository;
using StoreApp.Application.UseCases.OrderUseCase.Command.Update;
using StoreApp.Application.UseCases.PaymentUseCase.Query.GetList;
using StoreApp.Application.UseCases.PaymentUseCase.Query.GetOne;
using StoreApp.Application.UseCases.PaymentUseCase.Query.Search;

namespace StoreApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController(IMediator mediator, IVnPayService vnPayService) : Controller
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

        [HttpGet("payment-callback")]
        public async Task<IActionResult> PaymentCallback()
        {
            Console.WriteLine("--- BẮT ĐẦU CALLBACK ---");
            // 1. Nhận dữ liệu từ VNPay
            var response = vnPayService.PaymentExecute(Request.Query.ToDictionary(x => x.Key, x => x.Value.ToString()));

            // 2. Tạo Command gửi sang Handler
            // Lưu ý: response.Success sẽ là FALSE nếu khách hủy (Code 24)
            var command = new UpdateOrderStatusCommand(response.OrderId, response.Success, response.PaymentId);
            Console.WriteLine("--- BẮT ĐẦU GỌI MEDIATOR ---");
            // 3. Gọi Handler để xử lý Database (Cập nhật trạng thái + Hoàn kho)
            var result = await mediator.Send(command);
            Console.WriteLine("--- KẾT THÚC MEDIATOR ---");
            // 4. ĐIỀU HƯỚNG NGƯỜI DÙNG (Frontend)
            if (response.Success)
            {
                return Redirect($"http://localhost:3000/payment-success?id={response.OrderId}");
            }
            else
            {
                // Kiểm tra xem có phải khách hủy không (Mã 24)
                string reason = "Lỗi thanh toán";
                if (response.VnPayResponseCode == "24")
                {
                    reason = "Bạn đã hủy giao dịch";
                }
                Console.WriteLine("Response Code: " + response.VnPayResponseCode + "Response order id" + response.OrderId);
                // Chuyển hướng về trang thất bại kèm lý do
                string encodedReason = Uri.EscapeDataString(reason);

                return Redirect($"http://localhost:3000/payment-failed?reason={encodedReason}&id={response.OrderId}");
            }
        }
    }
}
