using MediatR;
using StoreApp.Application.DTOs;
using StoreApp.Application.Mapper;
using StoreApp.Application.Repository;
using StoreApp.Application.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Application.UseCases.CategoryUseCase.Query.GetOne
{
    public class GetCategoryHandler(ICategoryRepository categoryRepository) : IRequestHandler<GetCategoryQuery, ResultWithData<CategoryDTO>>
    {
        public async Task<ResultWithData<CategoryDTO>> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
        {
            var category = await categoryRepository.GetById(request.Id);
            var categoryDTO = category.ToDTO();
            return new ResultWithData<CategoryDTO>(
                Success: true,
                Message: "Lấy thông tin loại sản phẩm thành công.",
                Data: categoryDTO
                );
        }
    }
}
