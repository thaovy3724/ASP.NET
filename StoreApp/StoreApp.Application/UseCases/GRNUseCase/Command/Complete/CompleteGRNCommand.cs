using MediatR;

namespace StoreApp.Application.UseCases.GRNUseCase.Command.Complete
{
    public sealed record CompleteGRNCommand(Guid Id) : IRequest<Unit>;
}
