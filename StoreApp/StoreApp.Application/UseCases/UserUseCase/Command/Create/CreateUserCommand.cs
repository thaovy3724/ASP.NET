using MediatR;
using StoreApp.Application.DTOs;
using StoreApp.Application.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Application.UseCases.UserUseCase.Command.Create
{
    public sealed record CreateUserCommand(string userName, string fullName, string password, string role) : IRequest<ResultWithData<UserDTO>>;
}
