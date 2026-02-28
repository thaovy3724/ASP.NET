using MediatR;

namespace StoreApp.Application.UseCases.GRNUseCase.Command.Cancel
{
    public sealed record CancelGRNCommand(Guid Id) : IRequest<Unit>;
}
