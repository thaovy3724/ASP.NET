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

namespace StoreApp.Application.UseCases.PromotionUseCase.Query.GetList
{
    public class GetListPromotionHandler(IPromotionRepository promotionRepository) : IRequestHandler<GetListPromotionQuery, ResultWithData<List<PromotionDTO>>>
    {
        public async Task<ResultWithData<List<PromotionDTO>>> Handle(GetListPromotionQuery request, CancellationToken cancellationToken)
        {
            var promotions = await promotionRepository.GetAll();
            var promotionDTOs = promotions.Select(p => p.ToDTO()).ToList();
            return new ResultWithData<List<PromotionDTO>>(Success: true, Message: "Get list promotion successfully", Data: promotionDTOs);
        }
    }
}
