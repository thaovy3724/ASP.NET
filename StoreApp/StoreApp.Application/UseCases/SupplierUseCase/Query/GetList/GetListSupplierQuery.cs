using MediatR;
using StoreApp.Application.DTOs;
using StoreApp.Core.Entities;

namespace StoreApp.Application.UseCases.SupplierUseCase.Query.GetList
{
    public sealed record GetListSupplierQuery(
        string? Keyword = null
    ) : QueryStringParameters, IRequest<PagedList<SupplierDTO>>;
}
