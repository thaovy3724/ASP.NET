using MediatR;
using StoreApp.Application.DTOs;
using StoreApp.Application.Exceptions;
using StoreApp.Application.Mapper;
using StoreApp.Application.Repository;

namespace StoreApp.Application.UseCases.CategoryUseCase.Query.GetOne
{
    public class GetCategoryHandler(ICategoryRepository categoryRepository) : IRequestHandler<GetCategoryQuery, CategoryDTO>
    {
        public async Task<CategoryDTO> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
        {
            var category = await categoryRepository.GetById(request.Id);
            if(category is null)
            {
                throw new NotFoundException("Thể loại không tồn tại.");
            }
            return category.ToDTO();
        }
    }
}
