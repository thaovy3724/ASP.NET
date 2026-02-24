using MediatR;
using StoreApp.Application.DTOs;
using StoreApp.Application.Results;

namespace StoreApp.Application.UseCases.SupplierUseCase.Query.GetList
{
    public sealed record GetListSupplierQuery(
        string? Keyword = null
    ) : IRequest<ResultWithData<List<SupplierDTO>>>;
}
