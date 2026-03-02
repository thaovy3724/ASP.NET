using MediatR;
using StoreApp.Application.DTOs;
using StoreApp.Application.Exceptions;
using StoreApp.Application.Mapper;
using StoreApp.Application.Repository;

namespace StoreApp.Application.UseCases.UserUseCase.Query.GetOne
{
    public class GetUserHandler(IUserRepository userRepository) : IRequestHandler<GetUserQuery, UserDTO>
    {
        public async Task<UserDTO> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var user = await userRepository.GetById(request.Id);
            if(user == null)
            {
                throw new NotFoundException("Không tìm thấy người dùng.");
            }
            // Trả về kết quả trực tiếp
            return user.ToDTO();
        }
    }
}
