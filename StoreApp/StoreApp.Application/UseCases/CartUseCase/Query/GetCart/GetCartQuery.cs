using MediatR;
using StoreApp.Application.DTOs;

namespace StoreApp.Application.UseCases.CartUseCase.Query.GetCart
{
    public sealed record GetCartQuery(Guid? CustomerId) : IRequest<CartDTO>;
}