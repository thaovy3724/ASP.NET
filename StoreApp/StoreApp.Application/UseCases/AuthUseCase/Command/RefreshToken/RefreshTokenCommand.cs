using MediatR;
using StoreApp.Application.DTOs;
using StoreApp.Application.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Application.UseCases.AuthUseCase.Command.RefreshToken
{
    public sealed record RefreshTokenCommand(Guid userId, string refreshToken) : IRequest<ResultWithData<TokenResponseDTO>>;
}
