using System;

namespace StoreApp.Application.UseCases.OrderUseCase.Command.Create
{
    public sealed record CreateOrderRequestItem(Guid ProductId, int Quantity);
}
