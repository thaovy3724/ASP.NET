using StoreApp.Application.DTOs;
using StoreApp.Core.Entities;

namespace StoreApp.Application.Mapper
{
    public static class CustomerAddressMappingExtension
    {
        public static CustomerAddressDTO ToDTO(this CustomerAddress address)
        {
            return new CustomerAddressDTO(
                Id: address.Id,
                CustomerId: address.CustomerId,
                ReceiverName: address.ReceiverName,
                Phone: address.Phone,
                AddressLine: address.AddressLine,
                IsDefault: address.IsDefault,
                CreatedAt: address.CreatedAt
            );
        }
    }
}
