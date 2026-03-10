using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Application.UseCases.AuthUseCase.Command.Logout
{
    public sealed record LogoutCommand (Guid UserId, string RefreshToken) : IRequest<Unit>;
}
