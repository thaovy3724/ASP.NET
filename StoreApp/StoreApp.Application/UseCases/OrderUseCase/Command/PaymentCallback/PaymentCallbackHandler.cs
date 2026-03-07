//using MediatR;
//using StoreApp.Application.DTOs;
//using StoreApp.Application.Service.Payment;

//namespace StoreApp.Application.UseCases.OrderUseCase.Command.PaymentCallback
//{
//    public class PaymentCallbackHandler(IVnPayService vnPayService) : IRequestHandler<PaymentCallbackCommand, PaymentResponseModel>
//    {
//        public Task<PaymentResponseModel> Handle(PaymentCallbackCommand request, CancellationToken cancellationToken)
//        {
//            Console.WriteLine("--- BẮT ĐẦU CALLBACK ---");
//            // 1. Nhận dữ liệu từ VNPay
//            var response = vnPayService.PaymentExecute(request.PaymentParam);


//            // 2. Tạo Command gửi sang Handler
//            // Lưu ý: response.Success sẽ là FALSE nếu khách hủy (Code 24)
//            var command = new UpdateOrderStatusCommand(response.OrderId, response.Success, response.PaymentId);
//            Console.WriteLine("--- BẮT ĐẦU GỌI MEDIATOR ---");
//            // 3. Gọi Handler để xử lý Database (Cập nhật trạng thái + Hoàn kho)
//            var result = await mediator.Send(command);
//            Console.WriteLine("--- KẾT THÚC MEDIATOR ---");
//            // 4. ĐIỀU HƯỚNG NGƯỜI DÙNG (Frontend)
//            if (response.Success)
//            {
//                return Redirect($"http://localhost:3000/payment-success?id={response.OrderId}");
//            }
//            else
//            {
//                // Kiểm tra xem có phải khách hủy không (Mã 24)
//                string reason = "Lỗi thanh toán";
//                if (response.VnPayResponseCode == "24")
//                {
//                    reason = "Bạn đã hủy giao dịch";
//                }
//                Console.WriteLine("Response Code: " + response.VnPayResponseCode + "Response order id" + response.OrderId);
//                // Chuyển hướng về trang thất bại kèm lý do
//                string encodedReason = Uri.EscapeDataString(reason);

//                return Redirect($"http://localhost:3000/payment-failed?reason={encodedReason}&id={response.OrderId}");
//            }
//        }
//    }
