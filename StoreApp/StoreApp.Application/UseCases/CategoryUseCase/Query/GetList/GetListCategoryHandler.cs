using MediatR;
using StoreApp.Application.DTOs;
using StoreApp.Application.Mapper;
using StoreApp.Application.Repository;

namespace StoreApp.Application.UseCases.CategoryUseCase.Query.GetList
{
    public class GetListCategoryHandler(ICategoryRepository categoryRepository) : IRequestHandler<GetListCategoryQuery, List<CategoryDTO>>
    {
        public async Task<List<CategoryDTO>> Handle(GetListCategoryQuery request, CancellationToken cancellationToken)
        {
            var categories = await categoryRepository.Search(request.Keyword);

            var categoryDTO = categories
                    .Select(category => category.ToDTO())
                    .ToList();

            return categoryDTO;
        }
    }
}
