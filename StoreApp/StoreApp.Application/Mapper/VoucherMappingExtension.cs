using StoreApp.Application.DTOs;
using StoreApp.Core.Entities;

namespace StoreApp.Application.Mapper
{
    public static class VoucherMappingExtension
    {
        public static VoucherDTO ToDTO(this Voucher voucher)
        {
            return new VoucherDTO(
                Id: voucher.Id,
                Code: voucher.Code,
                DiscountPercent: voucher.DiscountPercent,
                MaxDiscountAmount: voucher.MaxDiscountAmount,
                StartDate: voucher.StartDate,
                EndDate: voucher.EndDate,
                Quantity: voucher.Quantity,
                Status: voucher.Status
            );
        }
    }
}