using MediatR;
using StoreApp.Application.DTOs;
using StoreApp.Application.Exceptions;
using StoreApp.Application.Mapper;
using StoreApp.Application.Repository;
using StoreApp.Core.Entities;

namespace StoreApp.Application.UseCases.CategoryUseCase.Command.Create
{
    public class CreateCategoryHandler(ICategoryRepository categoryRepository) : IRequestHandler<CreateCategoryCommand, CategoryDTO>
    {
        public async Task<CategoryDTO> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            // kiểm tra trùng tên
            if (await categoryRepository.IsExist(p => p.Name == request.Name))
            {
                throw new ConflictException("Thể loại đã tồn tại");
            }

            // tạo mới
            var category = new Category(request.Name);
            await categoryRepository.Create(category);

            return category.ToDTO();
        }
    }
}
