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

namespace StoreApp.Application.UseCases.UserUseCase.Query.Search
{
    public class GetListUserByKeywordHandler(IUserRepository userRepository) : IRequestHandler<GetListUserByKeywordQuery, ResultWithData<List<UserDTO>>>
    {
        public async Task<ResultWithData<List<UserDTO>>> Handle(GetListUserByKeywordQuery request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.keyword))
            {
                throw new ArgumentNullException(nameof(request.keyword), "Từ khóa tìm kiếm không được để trống.");
            }
            var users = await userRepository.SearchByKeyword(request.keyword);
            var userDTO = users
                .Select(user => user.ToDTO())
                .ToList();
            return new ResultWithData<List<UserDTO>>(
                Success: true,
                Message: "Lấy danh sách người dùng thành công.",
                Data: userDTO
            );
        }
    }
}
