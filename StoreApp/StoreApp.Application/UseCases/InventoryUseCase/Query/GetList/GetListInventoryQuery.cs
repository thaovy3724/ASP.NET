using MediatR;
using StoreApp.Application.DTOs;
using StoreApp.Application.Results;

namespace StoreApp.Application.UseCases.InventoryUseCase.Query.GetList
{
    public sealed record GetListInventoryQuery() : IRequest<ResultWithData<List<InventoryDTO>>>;
}
