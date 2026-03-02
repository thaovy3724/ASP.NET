using MediatR;
using StoreApp.Application.DTOs;

namespace StoreApp.Application.UseCases.UserUseCase.Query.GetList
{
    public sealed record GetListUserQuery(string? Keyword = null) : IRequest<List<UserDTO>>; 
}
