using MediatR;
using StoreApp.Application.DTOs;
using StoreApp.Application.Mapper;
using StoreApp.Application.Repository;
using StoreApp.Core.Entities;

namespace StoreApp.Application.UseCases.UserUseCase.Query.GetList
{
    public class GetListUserHandler(IUserRepository userRepository) : IRequestHandler<GetListUserQuery, PagedList<UserDTO>>
    {
        public async Task<PagedList<UserDTO>> Handle(GetListUserQuery request, CancellationToken cancellationToken)
        {
            var result = await userRepository.Search(request.PageNumber, request.PageSize, request.Keyword, request.role);
            var userListDTO = result.Items
                .Select(user => user.ToDTO())
                .ToList();
            // Trả về kết quả trực tiếp
            return new PagedList<UserDTO>(userListDTO, result.MetaData);
        }
    }
}
