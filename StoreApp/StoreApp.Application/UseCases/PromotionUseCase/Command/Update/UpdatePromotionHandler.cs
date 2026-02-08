using MediatR;
using StoreApp.Application.Repository;
using StoreApp.Application.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Application.UseCases.PromotionUseCase.Command.Update
{
    public class UpdatePromotionHandler(IPromotionRepository promotionRepository) : IRequestHandler<UpdatePromotionCommand, Result>
    {
        public async Task<Result> Handle(UpdatePromotionCommand request, CancellationToken cancellationToken)
        {
            // 1. Kiểm tra tồn tại
            var promotion = await promotionRepository.GetById(request.Id);
            if (promotion == null)
            {
                return new Result(Success: false, Message: "Promotion not found.");
            }

            // 2. Kiểm tra trùng mã PromoCode
            var allPromos = await promotionRepository.GetAll();
            var duplicateCode = allPromos.Any(p => p.PromoCode == request.PromoCode && p.Id != request.Id);

            if (duplicateCode)
            {
                return new Result(Success: false, Message: $"Mã khuyến mãi '{request.PromoCode}' đã tồn tại hệ thống.");
            }

            // 3. Cập nhật thông tin qua
            promotion.Update(
                promoCode: request.PromoCode,
                description: request.Description,
                discountType: request.DiscountType,
                discountValue: request.DiscountValue,
                startDate: request.StartDate,
                endDate: request.EndDate
            );

            // 4. Lưu vào DB
            await promotionRepository.Update(promotion);

            return new Result(Success: true, Message: "Promotion updated successfully.");
        }
    }
}
