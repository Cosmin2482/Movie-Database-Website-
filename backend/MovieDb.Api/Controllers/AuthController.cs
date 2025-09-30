using Microsoft.AspNetCore.Mvc;
using MovieDb.Api.Application.Interfaces;
using MovieDb.Api.Domain.DTOs;

namespace MovieDb.Api.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _auth;
        public AuthController(IAuthService auth) { _auth = auth; }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            var ok = await _auth.RegisterAsync(dto);
            return ok ? Created("", new { username = dto.Username }) : BadRequest(new { error = "Username already exists" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var token = await _auth.LoginAsync(dto);
            return token is null ? Unauthorized() : Ok(token);
        }
    }
}
