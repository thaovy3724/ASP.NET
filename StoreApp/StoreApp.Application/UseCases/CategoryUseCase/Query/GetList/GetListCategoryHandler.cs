using MediatR;
using StoreApp.Application.DTOs;
using StoreApp.Application.Mapper;
using StoreApp.Application.Repository;
using StoreApp.Core.Entities;

namespace StoreApp.Application.UseCases.CategoryUseCase.Query.GetList
{
    public class GetListCategoryHandler(ICategoryRepository categoryRepository) : IRequestHandler<GetListCategoryQuery, PagedList<CategoryDTO>>
    {
        public async Task<PagedList<CategoryDTO>> Handle(GetListCategoryQuery request, CancellationToken cancellationToken)
        {
            var result = await categoryRepository.Search(request.Keyword, request.PageNumber, request.PageSize);
            var categoryDTO = result.Items
                    .Select(category => category.ToDTO())
                    .ToList();

            return new PagedList<CategoryDTO>(categoryDTO, result.MetaData);
        }
    }
}
