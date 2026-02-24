using MediatR;
using StoreApp.Application.DTOs;
using StoreApp.Application.Mapper;
using StoreApp.Application.Repository;
using StoreApp.Application.Results;
using StoreApp.Core.Entities;

namespace StoreApp.Application.UseCases.CategoryUseCase.Query.GetList
{
    public class GetListCategoryHandler(ICategoryRepository categoryRepository) : IRequestHandler<GetListCategoryQuery, ResultWithData<List<CategoryDTO>>>
    {
        public async Task<ResultWithData<List<CategoryDTO>>> Handle(GetListCategoryQuery request, CancellationToken cancellationToken)
        {
            var categories = await categoryRepository.Search(request.Keyword);

            var categoryDTO = categories
                    .Select(category => category.ToDTO())
                    .ToList();

            return new ResultWithData<List<CategoryDTO>>(
                Success: true,
                Message: "Lấy danh sách danh mục thành công.",
                Data: categoryDTO
            );
        }
    }
}
