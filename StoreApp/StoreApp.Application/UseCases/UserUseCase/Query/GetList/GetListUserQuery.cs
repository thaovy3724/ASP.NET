using MediatR;
using StoreApp.Application.DTOs;
using StoreApp.Core.Entities;

namespace StoreApp.Application.UseCases.UserUseCase.Query.GetList
{
    public sealed record GetListUserQuery(string? Keyword = null) : QueryStringParameters, IRequest<PagedList<UserDTO>>; 
}
