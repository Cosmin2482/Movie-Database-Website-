using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using MovieDb.Api.Application.Interfaces;
using MovieDb.Api.Domain.DTOs;
using MovieDb.Api.Domain.Entities;

namespace MovieDb.Api.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _repo;
        private readonly IConfiguration _cfg;
        public AuthService(IUserRepository repo, IConfiguration cfg) { _repo = repo; _cfg = cfg; }

        public async Task<TokenResponse?> LoginAsync(LoginDto dto)
        {
            var user = await _repo.FindByUsernameAsync(dto.Username);
            if (user is null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash)) return null;
            var token = CreateJwt(user);
            return new TokenResponse(token);
        }

        public async Task<bool> RegisterAsync(RegisterDto dto)
        {
            var existing = await _repo.FindByUsernameAsync(dto.Username);
            if (existing is not null) return false;
            var user = new User { Username = dto.Username, PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password) };
            await _repo.AddAsync(user);
            await _repo.SaveChangesAsync();
            return true;
        }

        string CreateJwt(User user)
        {
            var issuer = _cfg["Jwt:Issuer"] ?? "MovieDb";
            var key = _cfg["Jwt:Key"] ?? "fallback-unsafe-key";
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.Username),
                new Claim(ClaimTypes.Name, user.Username)
            };
            var creds = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)), SecurityAlgorithms.HmacSha256);
            var jwt = new JwtSecurityToken(issuer: issuer, audience: null, claims: claims, expires: DateTime.UtcNow.AddHours(8), signingCredentials: creds);
            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}
