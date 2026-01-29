using StoreApp.Application.DTOs;
using StoreApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace StoreApp.Application.Mapper
{
    public static class CustomerMappingExtention
    {
        public static CustomerDTO ToDTO(this Customer cus)
        {
            // Truyền trực tiếp vào constructor theo đúng thứ tự
            return new CustomerDTO(
                Id: cus.Id,
                Name: cus.Name,
                Email: cus.Email,
                Phone: cus.Phone,
                Address: cus.Address,
                CreatedAt: cus.CreatedAt ?? DateTime.Now
            );
        }
    }
}
