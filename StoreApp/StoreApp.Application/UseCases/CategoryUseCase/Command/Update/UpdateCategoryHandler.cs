using MediatR;
using StoreApp.Application.Repository;
using StoreApp.Application.Results;

namespace StoreApp.Application.UseCases.CategoryUseCase.Command.Update
{
    public class UpdateCategoryHandler(ICategoryRepository categoryRepository) : IRequestHandler<UpdateCategoryCommand, Result>
    {
        public async Task<Result> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            // kiểm tra category có tồn tại chưa
            // tức kiểm tra tên category được cập nhật không được trùng với tên của category khác đã tồn tại trong hệ thống
            var categoryExisted = await categoryRepository.GetByExactName(request.Name, request.Id);
            if (categoryExisted is not null)
            {
                throw new ConflictException("Tên danh mục đã tồn tại trong hệ thống. Vui lòng sử dụng tên khác.");
            }

            // thực hiện cập nhật

            // B1: truy vấn category theo id
            var category = await categoryRepository.GetById(request.Id);

            // B2: cập nhật các thuộc tính thông qua hàm Update được định nghĩa trong Entity,
            // do các thuộc tính là private set nên không thể gán trực tiếp như thế này: category.Name = request.Name;
            category.Update(request.Name);

            // B3: gọi repository để cập nhật
            await categoryRepository.Update(category);
            return new Result(
                Success: true,
                Message: "Cập nhật danh mục thành công."
            );

        }
    }
}
