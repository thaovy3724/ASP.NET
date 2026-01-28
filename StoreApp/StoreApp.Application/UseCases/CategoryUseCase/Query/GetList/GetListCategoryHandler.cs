using MediatR;
using StoreApp.Application.DTOs;
using StoreApp.Application.Mapper;
using StoreApp.Application.Repository;
using StoreApp.Application.Results;

namespace StoreApp.Application.UseCases.CategoryUseCase.Query.GetList
{
    public class GetListCategoryHandler(ICategoryRepository categoryRepository) : IRequestHandler<GetListCategoryQuery, ResultWithData<List<CategoryDTO>>>
    {
        public async Task<ResultWithData<List<CategoryDTO>>> Handle(GetListCategoryQuery request, CancellationToken cancellationToken)
        {
            var categories = await categoryRepository.GetAll();

            var categoryDTO = categories
                .Select(category => category.ToDTO())
                .ToList();

            // Trả về kết quả trực tiếp
            return new ResultWithData<List<CategoryDTO>>(
                Success: true,
                Message: "Lấy danh sách danh mục thành công.",
                Data: categoryDTO
            );
        }
    }
}
