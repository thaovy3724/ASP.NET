using MediatR;

namespace StoreApp.Application.UseCases.SupplierUseCase.Command.Delete
{
    public sealed record DeleteSupplierCommand(Guid Id) : IRequest<Unit> ;
}
