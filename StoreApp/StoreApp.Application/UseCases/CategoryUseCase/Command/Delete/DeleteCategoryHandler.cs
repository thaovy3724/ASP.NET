using MediatR;
using StoreApp.Application.Exceptions;
using StoreApp.Application.Repository;
using StoreApp.Application.Results;

namespace StoreApp.Application.UseCases.CategoryUseCase.Command.Delete
{
    public class DeleteCategoryHandler(ICategoryRepository categoryRepository, IProductRepository productRepository) : IRequestHandler<DeleteCategoryCommand, Result>
    {
        public async Task<Result> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            // Kiểm tra category có tồn tại không
            var category = await categoryRepository.GetById(request.Id) ;
            if (category is null)
            {
                throw new NotFoundException("Thể loại không tồn tại.");
            }

            if (await productRepository.IsExist(p => p.CategoryId == request.Id))
            {
                throw new ConflictException("Thể loại đang được sử dụng, không thể xóa.");
            }

            // Thực hiện xóa
            await categoryRepository.Delete(category);
            return new Result(
                Success: true,
                Message: "Xóa thể loại thành công."
            );
        }
    }
}
