using MediatR;
using StoreApp.Application.DTOs;
using StoreApp.Application.Mapper;
using StoreApp.Application.Repository;
using StoreApp.Application.Results;

namespace StoreApp.Application.UseCases.CategoryUseCase.Query.Search
{
    public class SearchCategoryHandler(ICategoryRepository categoryRepository) : IRequestHandler<SearchCategoryQuery, ResultWithData<List<CategoryDTO>>>
    {
        public async Task<ResultWithData<List<CategoryDTO>>> Handle(SearchCategoryQuery request, CancellationToken cancellationToken)
        {
            var categories = await categoryRepository.Search(request.Keyword);

            var categoryDTO = categories
                .Select(category => category.ToDTO())
                .ToList();

            // Trả về kết quả trực tiếp
            return new ResultWithData<List<CategoryDTO>>(
                Success: true,
                Message: "Tìm kiếm danh mục thành công.",
                Data: categoryDTO
            );
        }
    }
}
