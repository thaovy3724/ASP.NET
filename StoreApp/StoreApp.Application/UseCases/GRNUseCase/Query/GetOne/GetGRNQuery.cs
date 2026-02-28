using MediatR;
using StoreApp.Application.DTOs;

namespace StoreApp.Application.UseCases.GRNUseCase.Query.GetOne
{
    public sealed record GetGRNQuery(Guid Id) : IRequest<GRNDTO>;
}
