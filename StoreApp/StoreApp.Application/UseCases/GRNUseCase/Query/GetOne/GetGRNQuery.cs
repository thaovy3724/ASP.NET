using MediatR;
using StoreApp.Application.DTOs;
using StoreApp.Application.Results;

namespace StoreApp.Application.UseCases.InventoryUseCase.Query.GetOne
{
    public sealed record GetGRNQuery(Guid Id) : IRequest<ResultWithData<GRNDTO>>;
}
