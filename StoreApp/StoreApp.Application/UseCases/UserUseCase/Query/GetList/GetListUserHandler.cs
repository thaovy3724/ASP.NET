using MediatR;
using StoreApp.Application.DTOs;
using StoreApp.Application.Mapper;
using StoreApp.Application.Repository;
using StoreApp.Application.Results;
using StoreApp.Core.Entities;

namespace StoreApp.Application.UseCases.UserUseCase.Query.GetList
{
    public class GetListUserHandler(IUserRepository userRepository) : IRequestHandler<GetListUserQuery, ResultWithData<List<UserDTO>>>
    {
        public async Task<ResultWithData<List<UserDTO>>> Handle(GetListUserQuery request, CancellationToken cancellationToken)
        {
            var users = await userRepository.Search(request.Keyword);
            var userDTO = users
                .Select(user => user.ToDTO())
                .ToList();
            // Trả về kết quả trực tiếp
            return new ResultWithData<List<UserDTO>>(
                Success: true,
                Message: "Lấy danh sách người dùng thành công.",
                Data: userDTO
            );
        }
    }
}
