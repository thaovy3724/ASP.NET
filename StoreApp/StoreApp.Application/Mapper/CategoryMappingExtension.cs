using StoreApp.Application.DTOs;
using StoreApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Application.Mapper
{
    public static class CategoryMappingExtension
    {
        public static CategoryDTO ToDTO(this Category entity)
        {
            return new CategoryDTO
            (
                Id : entity.Id,
                Name : entity.Name
            );
        }
    }
}
