namespace StoreApp.Application.UseCases.OrderUseCase.Command.Create
{
    public sealed record CreateOrderItem(Guid ProductId, int Quantity, decimal Price);
}
