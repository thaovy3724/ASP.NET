using MediatR;
using StoreApp.Application.UseCases.GRNUseCase.Command.Create;

namespace StoreApp.Application.UseCases.GRNUseCase.Command.Update
{
    public sealed record UpdateGRNCommand(Guid Id, List<CreateGRNItem> Items) : IRequest<Unit>;
}
