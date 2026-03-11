using MediatR;

namespace StoreApp.Application.UseCases.OrderUseCase.Command.Confirm
{
    public sealed record ConfirmOrderCommand (Guid Id, Guid StaffId) : IRequest<Unit>;
}
