using MediatR;
using StoreApp.Application.DTOs;

namespace StoreApp.Application.UseCases.SupplierUseCase.Query.GetList
{
    public sealed record GetListSupplierQuery(
        string? Keyword = null
    ) : IRequest<List<SupplierDTO>>;
}
