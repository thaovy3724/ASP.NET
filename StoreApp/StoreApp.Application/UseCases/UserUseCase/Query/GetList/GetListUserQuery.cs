using MediatR;
using StoreApp.Application.DTOs;
using StoreApp.Core.Entities;
using StoreApp.Core.ValueObject;

namespace StoreApp.Application.UseCases.UserUseCase.Query.GetList
{
    public sealed record GetListUserQuery(string? Keyword = null, Role? role = null) : QueryStringParameters, IRequest<PagedList<UserDTO>>; 
}
