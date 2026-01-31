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

namespace StoreApp.Application.UseCases.PromotionUseCase.Query.Search
{
    public class SearchPromotionHandler(IPromotionRepository promotionRepository) : IRequestHandler<SearchPromotionQuery, ResultWithData<List<PromotionDTO>>>
    {
        public async Task<ResultWithData<List<PromotionDTO>>> Handle(SearchPromotionQuery request, CancellationToken cancellationToken)
        {
            var promotions = await promotionRepository.SearchByKeyword(request.Keyword);
            var promotionDTOs = promotions
                .Select(promotion => promotion.ToDTO())
                .ToList();
            return new ResultWithData<List<PromotionDTO>>(
                Success: true,
                Message: "Search promotion successfully.",
                Data: promotionDTOs
            );
        }
    }
}
