using MediatR;
using StoreApp.Application.Repository;
using StoreApp.Application.Results;
using StoreApp.Application.UseCases.CategoryUseCase.Command.Delete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Application.UseCases.ProductUseCase.Command.Delete
{
    public class DeleteProductHandler(IProductRepository productRepository) : IRequestHandler<DeleteProductCommand, Result>
    {
        public async Task<Result> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            // Kiểm tra product muốn xóa có trong db không
            var product = await productRepository.GetById(request.Id);
            if (product is null)
            {
                return new Result(
                    Success: false,
                    Message: "Sản phẩm không tồn tại."
                );
            }

            // Thực hiện xóa
            await productRepository.Delete(product);
            return new Result(
                Success: true,
                Message: "Xóa sản phẩm thành công."
            );
        }
    }
}
