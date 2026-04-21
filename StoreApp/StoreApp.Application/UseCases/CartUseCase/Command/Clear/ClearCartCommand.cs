using MediatR;

namespace StoreApp.Application.UseCases.CartUseCase.Command.Clear
{
    public sealed record ClearCartCommand(Guid? CustomerId) : IRequest;
}