using MediatR;
using StoreApp.Application.DTOs;

namespace StoreApp.Application.UseCases.UserUseCase.Query.GetOne
{
    public sealed record class GetUserQuery(Guid Id) : IRequest<UserDTO>;
}
