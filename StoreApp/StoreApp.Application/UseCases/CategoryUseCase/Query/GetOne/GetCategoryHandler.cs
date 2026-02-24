using MediatR;
using StoreApp.Application.DTOs;
using StoreApp.Application.Exceptions;
using StoreApp.Application.Mapper;
using StoreApp.Application.Repository;
using StoreApp.Application.Results;

namespace StoreApp.Application.UseCases.CategoryUseCase.Query.GetOne
{
    public class GetCategoryHandler(ICategoryRepository categoryRepository) : IRequestHandler<GetCategoryQuery, ResultWithData<CategoryDTO>>
    {
        public async Task<ResultWithData<CategoryDTO>> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
        {
            var category = await categoryRepository.GetById(request.Id);
            if(category is null)
            {
                throw new NotFoundException("Thể loại không tồn tại.");
            }
            var categoryDTO = category.ToDTO();
            return new ResultWithData<CategoryDTO>(
                Success: true,
                Message: "Lấy thông tin thể loại thành công.",
                Data: categoryDTO
                );
        }
    }
}
