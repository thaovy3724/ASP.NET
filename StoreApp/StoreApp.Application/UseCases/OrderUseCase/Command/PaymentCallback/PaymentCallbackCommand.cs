using MediatR;
using StoreApp.Application.DTOs;


namespace StoreApp.Application.UseCases.OrderUseCase.Command.PaymentCallback
{
    public sealed record PaymentCallbackCommand(Dictionary<string, string> PaymentParam) : IRequest<PaymentResponseDTO>;
}
