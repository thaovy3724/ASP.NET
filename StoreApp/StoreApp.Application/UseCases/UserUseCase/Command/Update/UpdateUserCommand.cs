using MediatR;
using StoreApp.Application.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Application.UseCases.UserUseCase.Command.Update
{
    public sealed record UpdateUserCommand(
        Guid Id, 
        string UserName, 
        string FullName, 
        string Password,
        string Phone, 
        string Role) : IRequest<Result>;
}
