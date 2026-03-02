using MediatR;
using StoreApp.Application.DTOs;

namespace StoreApp.Application.UseCases.ProductUseCase.Command.Create
{
    public sealed record CreateProductCommand(Guid CategoryId, Guid SupplierId, string ProductName, decimal Price, string? ImageUrl) 
        : IRequest<ProductDTO>;

    // sealed: ko cho phép kế thừa => không có hoạt động dư thừa 
    // record: immutable aka bất biến 

}
