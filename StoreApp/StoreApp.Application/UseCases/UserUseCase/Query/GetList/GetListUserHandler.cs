using MediatR;
using StoreApp.Application.DTOs;
using StoreApp.Application.Mapper;
using StoreApp.Application.Repository;

namespace StoreApp.Application.UseCases.UserUseCase.Query.GetList
{
    public class GetListUserHandler(IUserRepository userRepository) : IRequestHandler<GetListUserQuery, List<UserDTO>>
    {
        public async Task<List<UserDTO>> Handle(GetListUserQuery request, CancellationToken cancellationToken)
        {
            var users = await userRepository.Search(request.Keyword);
            var userDTO = users
                .Select(user => user.ToDTO())
                .ToList();
            // Trả về kết quả trực tiếp
            return userDTO;
        }
    }
}
