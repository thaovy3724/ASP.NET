using StoreApp.Application.DTOs;
using StoreApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Application.Mapper
{
    public static class UserMappingExtension
    {
        public static UserDTO ToDTO(this User user)
        {
            return new UserDTO(
                Id: user.Id,
                Username: user.Username,
                Password: user.Password,
                FullName: user.FullName,
                Role: user.Role,
                CreatedAt: user.CreatedAt
            );
        }
    }
}
