using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Application.UseCases.CategoryUseCase.Query.GetOne
{
    public class GetCategoryValidator : AbstractValidator<GetCategoryQuery>
    { 
        public GetCategoryValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id không được để trống");
        }
    }
}
