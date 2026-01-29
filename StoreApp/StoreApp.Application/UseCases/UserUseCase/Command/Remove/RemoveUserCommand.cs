using MediatR;
using StoreApp.Application.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Application.UseCases.UserUseCase.Command.Remove
{
    public sealed record RemoveUserCommand(Guid Id) : IRequest<Result>;
}
