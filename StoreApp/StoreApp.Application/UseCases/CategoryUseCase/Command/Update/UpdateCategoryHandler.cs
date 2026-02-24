using MediatR;
using StoreApp.Application.Exceptions;
using StoreApp.Application.Repository;
using StoreApp.Application.Results;

namespace StoreApp.Application.UseCases.CategoryUseCase.Command.Update
{
    public class UpdateCategoryHandler(ICategoryRepository categoryRepository) : IRequestHandler<UpdateCategoryCommand, Result>
    {
        public async Task<Result> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await categoryRepository.GetById(request.Id);
            if (category is null)
            {
                throw new NotFoundException("Danh mục không tồn tại trong hệ thống.");
            }

            if (await categoryRepository.IsExist(c => c.Name == request.Name && c.Id != request.Id))
            {
                throw new ConflictException("Tên danh mục đã tồn tại trong hệ thống. Vui lòng sử dụng tên khác.");
            }

            category.Update(request.Name);

            await categoryRepository.Update(category);
            return new Result(
                Success: true,
                Message: "Cập nhật danh mục thành công."
            );

        }
    }
}
