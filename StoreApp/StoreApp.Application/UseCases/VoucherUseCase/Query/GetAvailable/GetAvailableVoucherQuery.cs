using MediatR;
using StoreApp.Application.DTOs;

namespace StoreApp.Application.UseCases.VoucherUseCase.Query.GetAvailable
{
    public sealed record GetAvailableVoucherQuery() : IRequest<List<AvailableVoucherDTO>>;
}