using StoreApp.Application.DTOs;
using StoreApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
