using MediatR;
using StoreApp.Application.Results;
using System.Text.Json.Serialization;

namespace StoreApp.Application.UseCases.ProductUseCase.Command.Update
{
    public sealed record UpdateProductCommand(
        Guid CategoryId,
        Guid SupplierId,
        string ProductName,
        string Barcode,
        decimal Price,
        string Unit,
        string? ImageUrl
    ) : IRequest<Result>
    {
        // Id lấy từ route (/api/product/{id}) - không cho client truyền trong body
        [JsonIgnore]
        public Guid ProductId { get; set; }
    }
}
