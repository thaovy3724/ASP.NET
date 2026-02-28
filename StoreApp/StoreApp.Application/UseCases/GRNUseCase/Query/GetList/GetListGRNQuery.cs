using MediatR;
using StoreApp.Application.DTOs;
using StoreApp.Core.ValueObject;

namespace StoreApp.Application.UseCases.GRNUseCase.Query.GetList
{
    public sealed record GetListGRNQuery(Guid? Supplier = null, GRNStatus? GRNStatus = null) : IRequest<List<GRNDTO>>;
}
