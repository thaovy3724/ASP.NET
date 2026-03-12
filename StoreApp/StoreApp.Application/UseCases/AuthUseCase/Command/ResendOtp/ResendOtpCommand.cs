using MediatR;

namespace StoreApp.Application.UseCases.AuthUseCase.Command.ResendOtp
{
    public sealed record ResendOtpCommand(string UserName) : IRequest<bool>;
}
