using MediatR;
using StoreApp.Application.DTOs;
using StoreApp.Core.Entities;

namespace StoreApp.Application.UseCases.VoucherUseCase.Query.GetList
{
    public sealed record GetListVoucherQuery(
        string? Keyword = null
    ) : QueryStringParameters, IRequest<PagedList<VoucherDTO>>;
}