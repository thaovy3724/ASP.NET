using MediatR;
using StoreApp.Application.DTOs;
using StoreApp.Application.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Application.UseCases.CategoryUseCase.Query.GetOne
{
    public sealed record GetCategoryQuery(Guid Id) : IRequest<ResultWithData<CategoryDTO>>;
}
