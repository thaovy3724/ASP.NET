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

namespace StoreApp.Application.UseCases.PromotionUseCase.Query.GetOne
{
    public class GetPromotionHandler(IPromotionRepository promotionRepository) : IRequestHandler<GetPromotionQuery, ResultWithData<PromotionDTO>>
    {
        public async Task<ResultWithData<PromotionDTO>> Handle(GetPromotionQuery request, CancellationToken cancellationToken)
        {
            var promotion = await promotionRepository.GetById(request.Id);
            if (promotion == null)
            {
                return new ResultWithData<PromotionDTO>(Success: false, Message: "Promotion not found", Data: null);
            }
            var promotionDTO = promotion.ToDTO();
            return new ResultWithData<PromotionDTO>(Success: true, Message: "Get promotion successfully", Data: promotionDTO);
        }
    
    }
}
