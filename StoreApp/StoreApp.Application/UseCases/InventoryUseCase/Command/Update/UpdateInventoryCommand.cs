using MediatR;
using StoreApp.Application.Results;
using System.Text.Json.Serialization;

namespace StoreApp.Application.UseCases.InventoryUseCase.Command.Update
{
    public sealed record UpdateInventoryCommand(int Quantity) : IRequest<Result>
    {
        // Id lấy từ route (/api/inventory/{id}) - không cho client truyền trong body
        [JsonIgnore]
        public Guid InventoryId { get; set; }
    }
}
