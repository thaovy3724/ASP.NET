using MediatR;
using StoreApp.Application.DTOs;
using StoreApp.Application.Mapper;
using StoreApp.Application.Repository;
using StoreApp.Application.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Application.UseCases.UserUseCase.Query.GetOne
{
    public class GetUserHandler(IUserRepository userRepository) : IRequestHandler<GetUserQuery, ResultWithData<UserDTO>>
    {
        public async Task<ResultWithData<UserDTO>> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var user = await userRepository.GetById(request.Id);
            if(user == null)
            {
                throw new NotFoundException("Không tìm thấy người dùng.");
            }
            // Trả về kết quả trực tiếp
            var dto = user.ToDTO();
            return new ResultWithData<UserDTO>(
                Success: true,
                Message: "Lấy thông tin người dùng thành công.",
                Data: dto
            );
        }
    }
}
