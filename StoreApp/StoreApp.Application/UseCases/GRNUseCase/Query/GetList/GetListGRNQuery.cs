using MediatR;
using StoreApp.Application.DTOs;
using StoreApp.Application.Results;
using StoreApp.Core.ValueObject;

namespace StoreApp.Application.UseCases.InventoryUseCase.Query.GetList
{
    public sealed record GetListGRNQuery(Guid? Supplier = null, GRNStatus? GRNStatus = null) : IRequest<ResultWithData<List<GRNDTO>>>;
}
