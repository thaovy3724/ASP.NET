namespace StoreApp.Application.UseCases.GRNUseCase.Command.Create
{
    public sealed record CreateGRNItem(Guid ProductId, int Quantity, decimal Price);
}
