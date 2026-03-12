using MediatR;
using StoreApp.Application.DTOs;

namespace StoreApp.Application.UseCases.AuthUseCase.Command.VerifyOtp
{
    public sealed record VerifyOtpCommand(string Email, string Otp) : IRequest<VerifyOtpResponseDTO>;
}
