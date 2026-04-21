using MediatR;
using StoreApp.Application.DTOs;

namespace StoreApp.Application.UseCases.VoucherUseCase.Query.GetOne
{
    public sealed record GetVoucherQuery(Guid Id) : IRequest<VoucherDTO>;
}