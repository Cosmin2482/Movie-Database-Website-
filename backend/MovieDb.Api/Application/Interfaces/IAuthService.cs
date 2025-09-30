using MovieDb.Api.Domain.DTOs;

namespace MovieDb.Api.Application.Interfaces
{
    public interface IAuthService
    {
        Task<TokenResponse?> LoginAsync(LoginDto dto);
        Task<bool> RegisterAsync(RegisterDto dto);
    }
}
