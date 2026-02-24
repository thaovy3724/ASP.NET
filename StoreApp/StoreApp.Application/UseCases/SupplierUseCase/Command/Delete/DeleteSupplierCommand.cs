using MediatR;
using StoreApp.Application.Results;

namespace StoreApp.Application.UseCases.SupplierUseCase.Command.Delete
{
    public sealed record DeleteSupplierCommand(Guid Id) : IRequest<Result> ;
}
