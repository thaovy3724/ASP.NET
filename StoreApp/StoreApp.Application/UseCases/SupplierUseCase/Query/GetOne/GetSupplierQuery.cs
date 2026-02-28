using MediatR;
using StoreApp.Application.DTOs;

namespace StoreApp.Application.UseCases.SupplierUseCase.Query.GetOne
{
    public sealed record GetSupplierQuery(Guid Id) : IRequest<SupplierDTO>;
}
