using MediatR;
using StoreApp.Application.Exceptions;
using StoreApp.Application.Repository;

namespace StoreApp.Application.UseCases.CategoryUseCase.Command.BulkDelete
{
    public class BulkDeleteCategoryHandler(
        ICategoryRepository categoryRepository,
        IProductRepository productRepository
    ) : IRequestHandler<BulkDeleteCategoryCommand, Unit>
    {
        public async Task<Unit> Handle(BulkDeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var ids = request.Ids?
                .Where(id => id != Guid.Empty)
                .Distinct()     // Loại bỏ các ID trùng lặp và ID rỗng
                .ToList() ?? [];

            if (ids.Count == 0)
            {
                throw new BadRequestException("Phải chọn ít nhất một danh mục để xóa.");
            }

            var categories = await categoryRepository.GetByIds(ids);

            // Kiểm tra xem có ID nào không tồn tại trong cơ sở dữ liệu hay không
            var foundIds = categories.Select(x => x.Id).ToHashSet();
            var missingIds = ids.Where(id => !foundIds.Contains(id)).ToList();

            if (missingIds.Count > 0)
            {
                throw new NotFoundException("Có danh mục không tồn tại.");
            }

            // Kiểm tra xem có danh mục nào đang được sử dụng bởi sản phẩm hay không
            foreach (var category in categories)
            {
                if (await productRepository.IsExist(p => p.CategoryId == category.Id))
                {
                    throw new ConflictException($"Thể loại \"{category.Name}\" đang được sử dụng, không thể xóa.");
                }
            }

            await categoryRepository.DeleteRange(categories);

            return Unit.Value;
        }
    }
}