using MediatR;
using StoreApp.Application.DTOs;

namespace StoreApp.Application.UseCases.GRNUseCase.Command.Create
{
    public sealed record CreateGRNCommand(Guid SupplierId, List<CreateGRNItem> Items) : IRequest<GRNDTO>;
}
