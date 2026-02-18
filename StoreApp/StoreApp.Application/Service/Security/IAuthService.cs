using StoreApp.Application.DTOs;
using StoreApp.Core.Entities;

namespace StoreApp.Application.Service.Security
{
    public interface IAuthService
    {
        string CreateToken(User user);
        Task<TokenResponseDTO> CreateTokenResponse(User user);
        Task<User?> ValidateRefreshTokenAsync(Guid userId, string refreshToken);
        string GenerateRefreshToken();
        Task<string> GenerateAndSaveRefreshTokenAsync(User user);

    }
}
