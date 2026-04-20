using MediatR;

namespace StoreApp.Application.UseCases.GRNUseCase.Command.BulkDelete
{
    public sealed record BulkDeleteGRNCommand(List<Guid>? Ids) : IRequest<Unit>;
}