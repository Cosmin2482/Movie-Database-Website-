namespace MovieDb.Api.Domain.DTOs
{
    public record RegisterDto(string Username, string Password);
    public record LoginDto(string Username, string Password);
    public record TokenResponse(string Token);
}
