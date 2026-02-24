using MediatR;
using StoreApp.Application.DTOs;
using StoreApp.Application.Results;

namespace StoreApp.Application.UseCases.SupplierUseCase.Command.Create
{
    public sealed record CreateSupplierCommand(
        string Name,
        string Phone,
        string Email,
        string Address
    ) : IRequest<ResultWithData<SupplierDTO>>;
}
