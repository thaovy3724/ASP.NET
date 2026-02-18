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
                throw new NotFoundException($"Không tìm thấy khuyến mãi với Id: {request.Id}");
            }

            // 2. Kiểm tra trùng mã PromoCode
            var allPromos = await promotionRepository.GetAll();
            var duplicateCode = allPromos.Any(p => p.PromoCode == request.PromoCode && p.Id != request.Id);

            if (duplicateCode)
            {
                throw new ConflictException($"Mã khuyến mãi '{request.PromoCode}' đã tồn tại.");
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
