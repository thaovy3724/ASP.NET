using MediatR;
using StoreApp.Application.Results;
using StoreApp.Core.ValueObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Application.UseCases.PromotionUseCase.Command.Update
{
    public sealed record UpdatePromotionCommand(
        Guid Id,
        string PromoCode,
        string Description,
        DateTime StartDate,
        DateTime EndDate,
        DiscountType DiscountType,
        decimal DiscountValue
    ) : IRequest<Result>;
}
