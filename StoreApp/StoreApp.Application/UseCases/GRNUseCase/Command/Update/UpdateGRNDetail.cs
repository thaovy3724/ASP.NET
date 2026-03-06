namespace StoreApp.Application.UseCases.GRNUseCase.Command.Update
{
    public sealed record UpdateGRNDetail(Guid ProductId, int Quantity, decimal Price);
}
