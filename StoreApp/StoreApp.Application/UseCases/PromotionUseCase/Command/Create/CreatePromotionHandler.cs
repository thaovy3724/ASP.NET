using MediatR;
using StoreApp.Application.Repository;
using StoreApp.Application.Results;
using StoreApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Application.UseCases.PromotionUseCase.Command.Create
{
    public class CreatePromotionHandler(IPromotionRepository promotionRepository) : IRequestHandler<CreatePromotionCommand, Result>
    {
        public async Task<Result> Handle(CreatePromotionCommand request, CancellationToken cancellationToken)
        {
            var promotion = new Promotion(
                promoCode: request.PromoCode,
                description: request.Description,
                discountType: request.DiscountType,
                discountValue: request.DiscountValue,
                startDate: request.StartDate,
                endDate: request.EndDate
            );
            await promotionRepository.Create(promotion);
            return new Result(Success: true, Message: "Promotion created successfully.");
        }
    }
}
