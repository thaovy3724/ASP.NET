using StoreApp.Application.DTOs;
using StoreApp.Core.Entities;

namespace StoreApp.Application.Mapper
{
    public static class SupplierMappingExtension
    {
        public static SupplierDTO ToDTO(this Supplier entity)
        {
            return new SupplierDTO
            (
                Id: entity.Id,
                Name: entity.Name,
                Phone: entity.Phone,
                Email: entity.Email,
                Address: entity.Address
            );
        }
    }
}
