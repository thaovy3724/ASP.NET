using MediatR;
using StoreApp.Application.DTOs;
using StoreApp.Application.Service.Payment;
using StoreApp.Application.UseCases.OrderUseCase.Command.Cancel;
using StoreApp.Application.UseCases.OrderUseCase.Command.Pay;

namespace StoreApp.Application.UseCases.OrderUseCase.Command.PaymentCallback
{
    public class PaymentCallbackHandler(IVnPayService vnPayService, IMediator mediator) : IRequestHandler<PaymentCallbackCommand, PaymentResponseDTO>
    {
        public async Task<PaymentResponseDTO> Handle(PaymentCallbackCommand request, CancellationToken cancellationToken)
        {
            Console.WriteLine("--- BẮT ĐẦU CALLBACK ---");
            // 1. Nhận dữ liệu từ VNPay
            var response = vnPayService.PaymentExecute(request.PaymentParam);

            if (!response.Success)
            {
                await mediator.Send(new CancelOrderCommand(response.OrderId));
            }
            else
            {
                await mediator.Send(new PayOrderCommand(response.OrderId));
            }

            return new PaymentResponseDTO
            (
                Success : response.Success,
                OrderId : response.OrderId
            );
        }
    }
}