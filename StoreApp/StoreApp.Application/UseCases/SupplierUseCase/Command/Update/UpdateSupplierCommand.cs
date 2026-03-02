using MediatR;

namespace StoreApp.Application.UseCases.SupplierUseCase.Command.Update
{
    public sealed record UpdateSupplierCommand(Guid Id,
        string Name,
        string Phone,
        string Email,
        string Address
    ) : IRequest<Unit>;
}
